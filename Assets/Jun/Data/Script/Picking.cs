using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Picking : MonoBehaviour
{
    [Header("picking Inspector")]
    [SerializeField]
    public LayerMask pickMask; //�������ִ� ���̾��߰�
    public LayerMask TP;
    public GameObject TPUI;
    public LayerMask Chest;
    public GameObject ChestUI;
    public UnityEvent<Vector2Int> clickToMove = null;   //Player��ũ��Ʈ���ִ� OnMoveByPath�ҷ�����
    public UnityEvent<Vector2Int,Vector2Int[]> clickToSkill = null;



    private Vector2Int currentHover;
    private List<Vector2Int> curTargets;
    private Vector2Int targetPos;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = Vector2Int.zero; 
        curTargets = new List<Vector2Int>();
        TPUI = GameObject.Find("Canvas").transform.Find("InGameUIs").transform.Find("TPUI").gameObject;
        ChestUI = GameObject.Find("Canvas").transform.Find("InGameUIs").transform.Find("ChestUI").gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //ī�޶�������� ���콺Ŀ���ǿ����϶����� ��ǥ���� ray�� ����

        if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, pickMask)) //Ŀ�����ִ°��� 1000�Ÿ���ŭ�Ƿ������� ��� Ground�� �ν��ϰ���
        {
            
            STATE _curState = this.GetComponent<CharactorMovement>().GetState(); //�÷��̾ ���������ִ� STATE����������
            if (_curState == STATE.MOVE) //�÷��̾���°� MOVE���¶�� ���� (EŰ������ Player��ũ��Ʈ�� ���� ����ιٲ�)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if ((1 << hit.transform.gameObject.layer & pickMask) != 0)
                    {
                        Debug.Log($"Hit Layer : {hit.transform.gameObject.layer}");
                        clickToMove?.Invoke(GameManager.Inst.GetTileIndex(hit.transform.gameObject));
                    }
                    
                }
                else
                {
                    //Debug.Log(GB.GetTileIndex(hit.transform.gameObject));
                    Vector2Int hitPos = GameManager.Inst.GetTileIndex(hit.transform.gameObject);
                    if (currentHover == -Vector2Int.one)
                    {
                        currentHover = hitPos;
                        GameManager.Inst.tiles[hitPos.x, hitPos.y].layer = 8;
                    }
                    if (currentHover != hitPos)
                    {
                        if (GameManager.Inst.CheckTileVisited(currentHover.x, currentHover.y) <= -1)
                            GameManager.Inst.tiles[currentHover.x, currentHover.y].layer = 3;
                        else
                            GameManager.Inst.tiles[currentHover.x, currentHover.y].layer = 9;
                        currentHover = hitPos;
                        GameManager.Inst.tiles[hitPos.x, hitPos.y].layer = 8;
                    }
                    
                }
            }
            if (_curState == STATE.ACTION)
            {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Vector2Int hitPos = GameManager.Inst.GetTileIndex(hit.transform.gameObject);

                        if (hitPos.x - GetComponent<Player>().my_Pos.x <= 1.5f && hitPos.x - GetComponent<Player>().my_Pos.x >= -1.5f
                            && hitPos.y - GetComponent<Player>().my_Pos.y <= 1.5f && hitPos.y - GetComponent<Player>().my_Pos.y >= -1.5f)
                        {
                            if (GameManager.Inst.tiles[hitPos.x, hitPos.y].GetComponent<TileState>().my_obj == OB_TYPES.TELEPORT)
                            {
                                TPUI.SetActive(true);
                                Create_obj_System.main_obj_create.TPtarget(hit.transform);
                            }
                            if (GameManager.Inst.tiles[hitPos.x, hitPos.y].GetComponent<TileState>().my_obj == OB_TYPES.CHEST)
                            {
                                ChestUI.SetActive(true);
                                Create_obj_System.main_obj_create.Chesttarget(hit.transform);
                            }
                        }
                    }
                else
                {
                    //Debug.Log(GB.GetTileIndex(hit.transform.gameObject));
                    Vector2Int hitPos = GameManager.Inst.GetTileIndex(hit.transform.gameObject);
                    if (currentHover == -Vector2Int.one)
                    {
                        currentHover = hitPos;
                        GameManager.Inst.tiles[hitPos.x, hitPos.y].layer = 8;
                    }
                    if (currentHover != hitPos)
                    {
                        if (GameManager.Inst.CheckTileVisited(currentHover.x, currentHover.y) == -1)
                            GameManager.Inst.tiles[currentHover.x, currentHover.y].layer = 3;
                        else
                            GameManager.Inst.tiles[currentHover.x, currentHover.y].layer = 9;
                        currentHover = hitPos;
                        GameManager.Inst.tiles[hitPos.x, hitPos.y].layer = 8;
                    }

                }
            }
            if (_curState == STATE.SKILL_CAST)
            {

                if (Input.GetMouseButtonDown(0))
                {
                    if ((1 << hit.transform.gameObject.layer & pickMask) != 0)
                    {
                        clickToSkill?.Invoke(targetPos, curTargets.ToArray());
                    }

                }
                else
                {
                    if(curTargets != null)
                    {
                        foreach(var init in curTargets)
                        {
                            GameManager.Inst.InitTarget(init);
                        }
                    }
                    //Debug.Log(GB.GetTileIndex(hit.transform.gameObject));
                    Vector3 pPos = this.transform.position;
                    Vector3 dir = hit.point - pPos;
                    dir.Normalize();
                    int is_front = 1;
                    float dot = Vector3.Dot(Vector3.forward, dir);
                    float angle= Vector3.Angle(Vector3.right, dir);


                    if (dot < 0) 
                        is_front = -1;
                    if(angle <= 45.0f)
                    {
                        //right
                        targetPos = GetComponent<Player>().my_Pos + new Vector2Int(1, 0);
                        foreach (Vector2Int v in GetComponent<Player>().currSkill.AttackIndex)
                        {
                            Vector2Int tmp = GetComponent<Player>().my_Pos + new Vector2Int(v.y, v.x);

                            if (GameManager.Inst.CheckIncludedIndex(tmp))
                            {
                                curTargets.Add(tmp);
                                GameManager.Inst.tiles[tmp.x, tmp.y].layer = 8;
                            }
                        }
                    }
                    else if(angle <= 135.0f)
                    {
                        //foward, back;
                        targetPos = GetComponent<Player>().my_Pos + new Vector2Int(0, is_front);

                        foreach (Vector2Int v in GetComponent<Player>().currSkill.AttackIndex)
                        {
                            Vector2Int tmp = GetComponent<Player>().my_Pos + v * is_front;

                            if (GameManager.Inst.CheckIncludedIndex(tmp))
                            {
                                curTargets.Add(tmp);
                                GameManager.Inst.tiles[tmp.x, tmp.y].layer = 8;
                            }

                        }
                    }
                    else
                    {
                        //left
                        targetPos = GetComponent<Player>().my_Pos + new Vector2Int(-1, 0);

                        foreach (Vector2Int v in GetComponent<Player>().currSkill.AttackIndex)
                        {
                            Vector2Int tmp = GetComponent<Player>().my_Pos - new Vector2Int(v.y, v.x);

                            if (GameManager.Inst.CheckIncludedIndex(tmp))
                            {
                                curTargets.Add(tmp);
                                GameManager.Inst.tiles[tmp.x, tmp.y].layer = 8;
                            }

                        }
                    }


                    Debug.Log($"Front : {is_front}, Direction {angle}");



                }
            }
        }
        else
        {
            if (currentHover != -Vector2Int.one)
            {
                GameManager.Inst.tiles[currentHover.x, currentHover.y].layer = 3;
                currentHover = -Vector2Int.one;
            }
        }

    }
}