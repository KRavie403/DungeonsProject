using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Vector2Int pos;
    public LayerMask pickMask;
    public List<TileStatus> tiles;
    public GameObject ChestObj;
    void Start()
    {
        Setting();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) //자기타일이 무엇인지 저장시키기
    {
        if ((1 << other.gameObject.layer & pickMask) != 0)
        {
            pos = MapManager.Inst.GetTileIndex(other.gameObject);

            this.GetComponent<CapsuleCollider>().isTrigger = false;
        }
    }
    Vector3 mypos = Vector3.zero;
    int x = 0, y = 0;
    public void chest() //체스트 확인누르면 실행
    {
        foreach (var tile in tiles)
        {
            if (tile.my_obj == OB_TYPES.PLAYER)
            {
                STATE _curState = tile.my_target.GetComponent<Player>().GetState();
                if (_curState == STATE.ACTION)
                {

                }
            }
        }
        MapManager.Inst.tiles[pos].GetComponent<TileStatus>().my_target = null;
        MapManager.Inst.tiles[pos].GetComponent<TileStatus>().my_obj = OB_TYPES.NONE;
        MapManager.Inst.tiles[pos].GetComponent<TileStatus>().isVisited = -1;



        x = Random.Range(1, 20);
        y = Random.Range(1, 20);

        Vector2Int my_Pos = new Vector2Int(x, y);

        float half = MapManager.Inst.scale * 0.5f;
        mypos = new Vector3((float)my_Pos.x + half, 0, (float)my_Pos.y + half);

        while (MapManager.Inst.tiles[my_Pos].GetComponent<TileStatus>().isVisited < -1 ||
            MapManager.Inst.tiles[my_Pos].GetComponent<TileStatus>().isVisited == 0)
        {
            x = Random.Range(1, 20);
            y = Random.Range(1, 20);

            my_Pos.x = x;
            my_Pos.y = y;

            half = MapManager.Inst.scale * 0.5f;
            mypos = new Vector3((float)my_Pos.x + half, 0, (float)my_Pos.y + half);
        }

        //pos = new Vector2Int(x, y);
        GameObject obj = Instantiate(ChestObj, mypos, Quaternion.identity);
        //obj.GetComponent<Chest>().pos = pos;

        this.GetComponent<Animator>().SetBool("OPEN", false);
        Destroy(gameObject);
    }
    public void Setting() //자기기준 3x3타일인식
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }

                Vector2Int newPos = pos + new Vector2Int(i, j);
                if (MapManager.Inst.tiles.ContainsKey(newPos))
                {
                    MapManager.Inst.tiles[newPos].my_obj = OB_TYPES.TELEPORT;
                    MapManager.Inst.tiles[newPos].my_target = this.gameObject;
                    MapManager.Inst.tiles[newPos].is_blocked = true;
                    tiles.Add(MapManager.Inst.tiles[newPos]);
                }
            }
        }
    }
}
