using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Vector2Int pos;
    public LayerMask pickMask;
    public List<TileState> tiles;
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
            pos = GameManager.Inst.GetTileIndex(other.gameObject);

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
        GameManager.Inst.tiles[pos.x, pos.y].GetComponent<TileState>().my_target = null;
        GameManager.Inst.tiles[pos.x, pos.y].GetComponent<TileState>().my_obj = OB_TYPES.NONE;
        GameManager.Inst.tiles[pos.x, pos.y].GetComponent<TileState>().isVisited = -1;



        x = Random.Range(1, 20);
        y = Random.Range(1, 20);

        Vector2Int my_Pos = new Vector2Int(x, y);

        float half = GameManager.Inst.scale * 0.5f;
        mypos = new Vector3((float)my_Pos.x + half, 0, (float)my_Pos.y + half);

        while (GameManager.Inst.tiles[my_Pos.x, my_Pos.y].GetComponent<TileState>().isVisited < -1 ||
            GameManager.Inst.tiles[my_Pos.x, my_Pos.y].GetComponent<TileState>().isVisited == 0)
        {
            x = Random.Range(1, 20);
            y = Random.Range(1, 20);

            my_Pos.x = x;
            my_Pos.y = y;

            half = GameManager.Inst.scale * 0.5f;
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
                tiles.Add(GameManager.Inst.tiles[pos.x + i, pos.y + j].GetComponent<TileState>());
            }
        }
    }
}
