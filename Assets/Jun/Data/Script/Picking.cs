using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Picking : MonoBehaviour
{

    public LayerMask pickMask;
    public UnityEvent<Vector2Int> clickToMove = null;
    public UnityEvent<Vector2Int[]> clickToSkill = null;

    private Vector2Int currentHover;
    private List<Vector2Int> curTargets;
    
    // Start is called before the first frame update
    void Start()
    {
        curTargets = new List<Vector2Int>();
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
                        clickToSkill?.Invoke(curTargets.ToArray());
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
                    float dot = Vector3.Dot(transform.forward, dir);
                    float angle= Vector3.Angle(transform.right, dir);


                    if (dot < 0) 
                        is_front = -1;
                    if(angle <= 45.0f)
                    {
                        foreach (Vector2Int v in GetComponent<Player>().currSKill.AttackIndex)
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
                        foreach (Vector2Int v in GetComponent<Player>().currSKill.AttackIndex)
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
                        foreach (Vector2Int v in GetComponent<Player>().currSKill.AttackIndex)
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