using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    public Vector2Int pos;
    public LayerMask pickMask;
    public List<TileState> tiles;
    public GameObject TPEffect;
    void Start()
    {
        //Create_obj_System.main_teleport.teleporters.Add(this);
        Setting();
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) 
    {
        if ((1 << other.gameObject.layer & pickMask) != 0)
        {
            pos = GameManager.Inst.GetTileIndex(other.gameObject);
            
            this.GetComponent<CapsuleCollider>().isTrigger = false;
        }
    }
    Vector3 mypos = Vector3.zero;
    int x = 0, y = 0;
    
    public void tp() //������ǥ�� �̵���Ű��
    {
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

        pos = new Vector2Int(x, y);
        Vector2Int pos2 = new Vector2Int();
        foreach (var tile in tiles) //�̵�
        {
            if (tile.my_obj == OB_TYPES.PLAYER)
            {
                STATE _curState = tile.my_target.GetComponent<Player>().GetState();
                if (_curState == STATE.ACTION)
                {
                    pos2.x = tile.my_target.GetComponent<Player>().my_Pos.x;
                    pos2.y = tile.my_target.GetComponent<Player>().my_Pos.y;

                    GameObject obj1 = Instantiate(TPEffect, tile.my_target.transform.position, Quaternion.identity);

                    tile.my_target.transform.position = mypos;
                    tile.my_target.GetComponent<Player>().my_Pos = pos;

                    GameObject obj2 = Instantiate(TPEffect, tile.my_target.transform.position, Quaternion.identity);

                    GameManager.Inst.tiles[pos.x, pos.y].GetComponent<TileState>().my_target = tile.my_target.gameObject;
                    GameManager.Inst.tiles[pos.x, pos.y].GetComponent<TileState>().my_obj = tile.my_target.GetComponent<Player>().myType;
                    GameManager.Inst.tiles[pos.x, pos.y].GetComponent<TileState>().isVisited = 0;
                    GameManager.Inst.tiles[pos2.x, pos2.y].GetComponent<TileState>().my_target = null;
                    GameManager.Inst.tiles[pos2.x, pos2.y].GetComponent<TileState>().my_obj = OB_TYPES.NONE;
                    GameManager.Inst.tiles[pos2.x, pos2.y].GetComponent<TileState>().isVisited = -1;

                    
                }
            }
        }
    }
    public void Setting()
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) // && GameManager.GM.CheckIncludedIndex(new Vector2Int(pos.x+i,pos.y+j))
                {
                    continue;
                }
                tiles.Add(GameManager.Inst.tiles[pos.x + i, pos.y + j].GetComponent<TileState>());
            }
        }
    }
}
