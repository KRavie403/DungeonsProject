using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Vector2Int pos;
    public LayerMask pickMask;
    public List<TileState> tiles;
    void Start()
    {
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

            this.GetComponent<BoxCollider>().isTrigger = false;
        }
    }
    public void chest()
    {
        foreach (var tile in tiles)
        {
            if (tile.my_obj == OB_TYPES.PLAYER)
            {
                Player.STATE _curState = tile.my_target.GetComponent<Player>().GetState();
                if (_curState == Player.STATE.ACTION)
                {

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
