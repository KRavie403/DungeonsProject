using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Picking : MonoBehaviour
{
    public LayerMask pickMask; //�������ִ� ���̾��߰�
    public LayerMask TP;
    public GameObject TPUI;
    public UnityEvent<Vector2Int> clickToMove = null;   //Player��ũ��Ʈ���ִ� OnMoveByPath�ҷ�����
    public UnityEvent<Vector2Int,Vector2Int[]> clickToSkill = null;



    private Vector2Int currentHover;
    private List<Vector2Int> curTargets;
    private Vector2Int targetDir;

    // Start is called before the first frame update
    void Start()
    {
        targetDir = Vector2Int.zero; 
        curTargets = new List<Vector2Int>();
    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //ī�޶�������� ���콺Ŀ���ǿ����϶����� ��ǥ���� ray�� ����

        if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, pickMask)) //Ŀ�����ִ°��� 1000�Ÿ���ŭ�Ƿ������� ��� Ground�� �ν��ϰ���
        {
            
            CharactorMovement.STATE _curState = this.GetComponent<CharactorMovement>().GetState(); //�÷��̾ ���������ִ� STATE����������
            if (_curState == CharactorMovement.STATE.MOVE) //�÷��̾���°� MOVE���¶�� ���� (EŰ������ Player��ũ��Ʈ�� ���� ����ιٲ�)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if ((1 << hit.transform.gameObject.layer & pickMask) != 0)
                    {
                        Debug.Log($"Hit Layer : {hit.transform.gameObject.layer}");
                        clickToMove?.Invoke(GameManager.GM.GetTileIndex(hit.transform.gameObject));
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
                    if ((1 << hit.transform.gameObject.layer & TP) != 0)
                    {
                        if (hit.transform.position.x - this.transform.position.x <= 1.5f && hit.transform.position.x - this.transform.position.x >= -1.5f
                            && hit.transform.position.z - this.transform.position.z <= 1.5f && hit.transform.position.z - this.transform.position.z >= -1.5f)
                        {
                            TPUI.SetActive(true);
                            TeleportSystem.main_teleport.testtarget(hit.transform);
                        }
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
            if (_curState == Player.STATE.SKILL_CAST)
            {

                if (Input.GetMouseButtonDown(0))
                {
                    if ((1 << hit.transform.gameObject.layer & pickMask) != 0)
                    {
                        clickToSkill?.Invoke(targetDir, curTargets.ToArray());
                    }

                }
                else
                {
                    if(curTargets != null)
                    {
                        foreach(var init in curTargets)
                        {
                            GameManager.GM.InitTarget(init);
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
                        targetDir = GetComponent<Player>().my_Pos + new Vector2Int(1, 0);
                        foreach (Vector2Int v in GetComponent<Player>().currSkill.AttackIndex)
                        {
                            Vector2Int tmp = GetComponent<Player>().my_Pos + new Vector2Int(v.y, v.x);

                            if (GameManager.GM.CheckIncludedIndex(tmp))
                            {
                                curTargets.Add(tmp);
                                GameManager.GM.tiles[tmp.x, tmp.y].layer = 8;
                            }
                        }
                    }
                    else if(angle <= 135.0f)
                    {
                        //foward, back;
                        targetDir = GetComponent<Player>().my_Pos + new Vector2Int(0, is_front);

                        foreach (Vector2Int v in GetComponent<Player>().currSkill.AttackIndex)
                        {
                            Vector2Int tmp = GetComponent<Player>().my_Pos + v * is_front;

                            if (GameManager.GM.CheckIncludedIndex(tmp))
                            {
                                curTargets.Add(tmp);
                                GameManager.GM.tiles[tmp.x, tmp.y].layer = 8;
                            }

                        }
                    }
                    else
                    {
                        //left
                        targetDir = GetComponent<Player>().my_Pos + new Vector2Int(-1, 0);

                        foreach (Vector2Int v in GetComponent<Player>().currSkill.AttackIndex)
                        {
                            Vector2Int tmp = GetComponent<Player>().my_Pos - new Vector2Int(v.y, v.x);

                            if (GameManager.GM.CheckIncludedIndex(tmp))
                            {
                                curTargets.Add(tmp);
                                GameManager.GM.tiles[tmp.x, tmp.y].layer = 8;
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
                GameManager.GM.tiles[currentHover.x, currentHover.y].layer = 3;
                currentHover = -Vector2Int.one;
            }
        }

    }
}