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
    public void tp()
    {
        foreach (var tile in tiles)
        {
            if (tile.my_obj == OB_TYPES.PLAYER)
            {
                STATE _curState = tile.my_target.GetComponent<Player>().GetState();
                if (_curState == STATE.ACTION)
                {
                    GameObject obj1 = Instantiate(TPEffect, tile.my_target.transform.position, Quaternion.identity);
                    tile.my_target.transform.position = new Vector3(5.5f, 0, 8.5f);
                    GameObject obj2 = Instantiate(TPEffect, tile.my_target.transform.position, Quaternion.identity);
                    tile.my_target.GetComponent<Player>().my_Pos = new Vector2Int(5, 8);
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
