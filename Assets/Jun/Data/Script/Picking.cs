using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Picking : MonoBehaviour
{
    public LayerMask pickMask; //누를수있는 레이어추가
    public LayerMask TP;
    public LayerMask Chest;
    public GameObject TPUI;
    public GameObject ChestUI;
    public UnityEvent<Vector2Int> clickAction = null; //Player스크립트에있는 OnMoveByPath불러오기

    private Vector2Int currentHover;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //카메라기준으로 마우스커서의움직일때마다 좌표값을 ray에 기입

        if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, pickMask)) //커서가있는곳에 1000거리만큼의레이저를 쏘고 Ground만 인식하게함
        {
            Player.STATE _curState = this.GetComponent<Player>().GetState(); //플레이어에 열거형에있는 STATE값을가져옴
            if (_curState == Player.STATE.MOVE) //플레이어상태가 MOVE상태라면 실행 (E키누르면 Player스크립트로 인해 무브로바뀜)
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
                        if (GameManager.GM.CheckTileVisited(currentHover.x, currentHover.y) == -1)
                            GameManager.GM.tiles[currentHover.x, currentHover.y].layer = 3;
                        else
                            GameManager.GM.tiles[currentHover.x, currentHover.y].layer = 9;
                        currentHover = hitPos;
                        GameManager.GM.tiles[hitPos.x, hitPos.y].layer = 8;
                    }
                    
                }
            }
            if (_curState == Player.STATE.ACTION)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector2Int hitPos = GameManager.GM.GetTileIndex(hit.transform.gameObject);

                    if (hitPos.x - GetComponent<Player>().my_Pos.x <= 1.5f && hitPos.x - GetComponent<Player>().my_Pos.x >= -1.5f
                        && hitPos.y - GetComponent<Player>().my_Pos.y <= 1.5f && hitPos.y - GetComponent<Player>().my_Pos.y >= -1.5f)
                    {
                        if (GameManager.GM.tiles[hitPos.x, hitPos.y].GetComponent<TileState>().my_obj == OB_TYPES.TELEPORT)
                        {
                            TPUI.SetActive(true);
                            Create_obj_System.main_teleport.TPtarget(hit.transform);
                        }
                        if (GameManager.GM.tiles[hitPos.x, hitPos.y].GetComponent<TileState>().my_obj == OB_TYPES.Chest)
                        {
                            ChestUI.SetActive(true);
                            Create_obj_System.main_teleport.Chesttarget(hit.transform);
                        }
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
                    Vector3 pPos = this.transform.position;

                    Vector3 dir = hit.point - pPos;
                    dir.Normalize();
                    
                    bool is_front = false;
                    float dot = Vector3.Dot(transform.forward, dir);
                    float angle= Vector3.Angle(transform.right, dir);


                    if (dot > 0) 
                        is_front = true;
                    if(angle <= 45)
                    {
                        //foreach (vector2int v in getcomponent<player>().currskill.attackindex)
                        //{
                        //    vector2int tmp = ppos + v;
                        //}
                    }


                    Debug.Log($"Front : {is_front}, Direction {angle}");






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
                GameManager.GM.tiles[currentHover.x, currentHover.y].layer = 3;
                currentHover = -Vector2Int.one;
            }
        }

    }
}