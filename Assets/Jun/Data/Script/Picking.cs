using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Picking : MonoBehaviour
{
    public LayerMask pickMask; //�������ִ� ���̾��߰�
    public LayerMask TP;
    public LayerMask Chest;
    public GameObject TPUI;
    public GameObject ChestUI;
    public UnityEvent<Vector2Int> clickAction = null; //Player��ũ��Ʈ���ִ� OnMoveByPath�ҷ�����

    private Vector2Int currentHover;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //ī�޶�������� ���콺Ŀ���ǿ����϶����� ��ǥ���� ray�� ����

        if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, pickMask)) //Ŀ�����ִ°��� 1000�Ÿ���ŭ�Ƿ������� ��� Ground�� �ν��ϰ���
        {
            Player.STATE _curState = this.GetComponent<Player>().GetState(); //�÷��̾ ���������ִ� STATE����������
            if (_curState == Player.STATE.MOVE) //�÷��̾���°� MOVE���¶�� ���� (EŰ������ Player��ũ��Ʈ�� ���� ����ιٲ�)
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