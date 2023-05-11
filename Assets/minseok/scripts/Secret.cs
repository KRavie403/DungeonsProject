using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secret : MonoBehaviour
{
    public LayerMask pickMask;
    public GameObject SecretItemUI;
    public Vector2Int pos;
    public TileState tiles;
    public GameObject SecretItemObj;


    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & pickMask) != 0)
        {
            pos = GameManager.Inst.GetTileIndex(other.gameObject);
            tiles = (GameManager.Inst.tiles[pos.x, pos.y].GetComponent<TileState>());
            this.GetComponent<SphereCollider>().isTrigger = false;
        }
    }
    void Start() //UI바인딩 후 셋액티브 펄스
    {
        SecretItemUI = GameObject.Find("Canvas").transform.Find("InGameUIs").transform.Find("SecretItemUI").gameObject;
        this.gameObject.SetActive(false);
    }

    void Update() //자기타일에 플레이어가 들어오면 실행
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
