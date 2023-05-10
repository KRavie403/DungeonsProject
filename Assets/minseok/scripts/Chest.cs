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
            pos = GameManager.Inst.GetTileIndex(other.gameObject);

            this.GetComponent<SphereCollider>().isTrigger = false;
        }
    }
    public void chest()
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
        Destroy(gameObject);
    }
    public void Setting()
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
