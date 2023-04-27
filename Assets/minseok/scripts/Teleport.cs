using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    public Vector2Int pos;
    public LayerMask pickMask;
    public List<TileState> tiles;
    void Start()
    {
        TeleportSystem.main_teleport.teleporters.Add(this);
        Setting();
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) 
    {
        if ((1 << other.gameObject.layer & pickMask) != 0)
        {
            pos = GameManager.GM.GetTileIndex(other.gameObject);
            
            this.GetComponent<CapsuleCollider>().isTrigger = false;
        }
    }
    public void tp()
    {
        foreach (var tile in tiles)
        {
            if (tile.my_obj == OB_TYPES.PLAYER)
            {
                Player.STATE _curState = tile.my_target.GetComponent<Player>().GetState();
                if (_curState == Player.STATE.ACTION)
                {
                    tile.my_target.transform.position = new Vector3(5.5f, 0, 8.5f);
                }
            }
        }
    }
    public void Setting()
    {
        tiles.Add(GameManager.GM.tiles[pos.x + 1, pos.y + 0].GetComponent<TileState>());
        tiles.Add(GameManager.GM.tiles[pos.x + 0, pos.y + 1].GetComponent<TileState>());
        tiles.Add(GameManager.GM.tiles[pos.x + -1, pos.y + 0].GetComponent<TileState>());
        tiles.Add(GameManager.GM.tiles[pos.x + 0, pos.y + -1].GetComponent<TileState>());
        tiles.Add(GameManager.GM.tiles[pos.x + 1, pos.y + 1].GetComponent<TileState>());
        tiles.Add(GameManager.GM.tiles[pos.x + -1, pos.y + -1].GetComponent<TileState>());
        tiles.Add(GameManager.GM.tiles[pos.x + 1, pos.y + -1].GetComponent<TileState>());
        tiles.Add(GameManager.GM.tiles[pos.x + -1, pos.y + 1].GetComponent<TileState>());
    }
}
