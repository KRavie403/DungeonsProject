using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Picking : MonoBehaviour
{

    public LayerMask pickMask;
    public UnityEvent<Vector2Int> clickAction = null;

    private Vector2Int currentHover;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        Camera.main.ViewportPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, pickMask))
        {
            Player.STATE _curState = this.GetComponent<Player>().GetState();
            if (_curState == Player.STATE.MOVE)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if ((1 << hit.transform.gameObject.layer & pickMask) != 0)
                    {
                        Debug.Log($"Hit Layer : {hit.transform.gameObject.layer}");
                        clickAction?.Invoke(GameManager.GM.GetTileIndex(hit.transform.gameObject));
                    }

                }
                else
                {
                    //Debug.Log(GB.GetTileIndex(hit.transform.gameObject));
                    Vector2Int hitPos = GameManager.GM.GetTileIndex(hit.transform.gameObject);
                    if (currentHover == -Vector2Int.one)
                    {
                        currentHover = hitPos;
                        GameManager.GM.tiles[hitPos.x, hitPos.y].layer = 8;
                    }
                    if (currentHover != hitPos)
                    {
                        if (GameManager.GM.CheckTileVisited(currentHover.x, currentHover.y) <= -1)
                            GameManager.GM.tiles[currentHover.x, currentHover.y].layer = 3;
                        else
                            GameManager.GM.tiles[currentHover.x, currentHover.y].layer = 9;
                        currentHover = hitPos;
                        GameManager.GM.tiles[hitPos.x, hitPos.y].layer = 8;
                    }
                }
            }
            if (_curState == Player.STATE.SKILL_CAST)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if ((1 << hit.transform.gameObject.layer & pickMask) != 0)
                    {
                        //clickAction?.Invoke(GameManager.GM.GetTileIndex(hit.transform.gameObject));
                    }

                }
                else
                {
                    //Debug.Log(GB.GetTileIndex(hit.transform.gameObject));
                    Vector2Int pPos = GetComponent<Player>().my_Pos;
                    Vector2Int hitPos = GameManager.GM.GetTileIndex(hit.transform.gameObject);

                    Vector2 dir = hitPos - pPos;

                    foreach (Vector2Int v in GetComponent<Player>().currSKill.AttackIndex)
                    {
                        Vector2Int tmp = pPos + v;
                    }


                    //if (currentHover == -Vector2Int.one)
                    //{
                    //    currentHover = hitPos;
                    //    GameManager.GM.tiles[hitPos.x, hitPos.y].layer = 8;
                    //}
                    //if (currentHover != hitPos)
                    //{
                    //    if (GameManager.GM.CheckTileVisited(currentHover.x, currentHover.y) <= -1)
                    //        GameManager.GM.tiles[currentHover.x, currentHover.y].layer = 3;
                    //    else
                    //        GameManager.GM.tiles[currentHover.x, currentHover.y].layer = 9;
                    //    currentHover = hitPos;
                    //    GameManager.GM.tiles[hitPos.x, hitPos.y].layer = 8;
                    //}
                }
            }
        }
        else
        {
            if (currentHover != -Vector2Int.one)
            {
                if(GameManager.GM.tiles[currentHover.x, currentHover.y] != null)
                    GameManager.GM.tiles[currentHover.x, currentHover.y].layer = 3;
                currentHover = -Vector2Int.one;
            }
        }

    }
}