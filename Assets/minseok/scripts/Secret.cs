using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secret : MonoBehaviour
{
    public LayerMask pickMask;
    public GameObject SecretItemUI;
    public Vector2Int pos;
    public TileState tiles;
 

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & pickMask) != 0)
        {
            pos = GameManager.Inst.GetTileIndex(other.gameObject);
            tiles = (GameManager.Inst.tiles[pos.x, pos.y].GetComponent<TileState>());
            this.GetComponent<SphereCollider>().isTrigger = false;
        }
    }
    void Start()
    {
        SecretItemUI = GameObject.Find("Canvas").transform.Find("InGameUIs").transform.Find("SecretItemUI").gameObject;
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        if (tiles.my_obj == OB_TYPES.PLAYER)
        {
            STATE _curState = tiles.my_target.GetComponent<Player>().GetState();
            if (_curState == STATE.ACTION)
            {
                SecretItemUI.SetActive(true);
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
