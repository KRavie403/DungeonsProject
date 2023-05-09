using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;
public class BossMonster : CharactorMovement
{
    // Start is called before the first frame update
    public Slider _bossHPUI;
    private float Hp;

    public override void SetPos()
    {
        myType = OB_TYPES.MONSTER;
        if (skilList == null)
            skilList = new List<SkillSet>();
        StartCoroutine(SettingPos());

    }
    IEnumerator SettingPos()
    {
        
        int x, y;

        do
        {
            x = Random.Range(0, GameManager.Inst.rows);
            y = Random.Range(0, GameManager.Inst.columns);
        } while (GameManager.Inst.tiles[x, y].GetComponent<TileState>().isVisited == -5);


        my_Pos = new Vector2Int(x, y);

        float half = GameManager.Inst.scale * 0.5f;
        transform.position = new Vector3((float)my_Pos.x + half, 0, (float)my_Pos.y + half);

        for (int i = 0; i <= 1; i++)
        {
            for (int j = 0; j <= 1; j++)
            {
                GameManager.Inst.tiles[my_Pos.x + i, my_Pos.y + j].GetComponent<TileState>().my_obj = myType;
                GameManager.Inst.tiles[my_Pos.x + i, my_Pos.y + j].GetComponent<TileState>().isVisited = -2;
                GameManager.Inst.tiles[my_Pos.x + i, my_Pos.y + j].GetComponent<TileState>().SetTarget(this.gameObject);
            }
        }
        UI_Manager.Inst.AddPlayer(my_Sprite);

        yield return null;
    }
    override public void SetDistance()
    {
        List<GameObject> searchTileArea = new List<GameObject>();

        
        for (int i = my_Pos.x - curAP; i <= my_Pos.x + curAP; i++)
        {
            for (int j = my_Pos.y - curAP; i <= my_Pos.y + curAP; j++)
            {
                Vector2Int pos = new Vector2Int(i, j);
                if (!GameManager.Inst.CheckIncludedIndex(pos))
                    break;
                searchTileArea.Add((GameObject)GameManager.Inst.tiles[i, j]);
            }
        }
        
        for (int step = 1; step <= curAP; step++)
        {
            foreach (GameObject obj in searchTileArea)
            {
                if (obj.GetComponent<TileState>().isVisited == step - 1)
                {
                    TestAllDirection(obj.GetComponent<TileState>().pos.x, obj.GetComponent<TileState>().pos.y, step);
                    //obj 주변 x+1 / y + 1방향도 step값 변경, 예외처리 필요
                    //if 인접타일이 못가는 곳인가 ? step 날림
                    obj.layer = 9;
                }
                if (obj.GetComponent<TileState>().isVisited == step)
                    obj.layer = 9;
            }
        }

    }
    public override void OnMove()
    {
        ChangeState(STATE.MOVE);
        InitTileDistance();
        SetDistance();

        //Vector2Int target;
        //FindingPlayer()?.MoveByPath(target);
        

    }
    void StateProcess()
    {
        switch (_curState)
        {
            case STATE.CREATE:
                break;

            case STATE.IDLE:

                break;

            case STATE.ACTION:


                break;
            case STATE.MOVE:
                break;

            case STATE.SKILL_CAST:
                break;
        }
    }

    public override void ChangeState(STATE s)
    {
        if (_curState == s) return;
        _bfState = _curState;
        _curState = s;
        switch (_curState)
        {
            case STATE.CREATE:
                break;

            case STATE.IDLE:

                break;

            case STATE.ACTION:


                break;
            case STATE.MOVE:

                break;

        }
    }
    public void OnCastingSkill(Vector2Int target, Vector2Int[] targets)
    {
        //애니메이션 재생 (casting end)
        //목표 회전
        Vector3 dir = new Vector3((target.x + GameManager.Inst.scale / 2.0f) * _mySize, transform.position.y, (target.y + GameManager.Inst.scale / 2.0f) * _mySize) - transform.position;
        StartCoroutine(CastingSkill(dir, targets));
    }
    IEnumerator CastingSkill(Vector3 dir, Vector2Int[] targets)
    {
        bool rote = false;
        Roatate(dir, () => rote = true);
        while (!rote)
        {
            yield return null;
        }

        //애니메이션 재생 (action start)

        //애니메이션이 끝나고 실행
        foreach (var index in targets)
        {
            GameObject target = GameManager.Inst.tiles[index.x, index.y].GetComponent<TileState>().OnMyTarget();

            if (target != null && target.GetComponent<BossMonster>() != null)
            {
                target.GetComponent<BossMonster>().TakeDamage(10.0f);
            }
        }
    }
    public void OnAttack(Vector2Int tile)
    {
        ChangeState(STATE.ATTACK);
        InitTileDistance();
    }
    public void OnSkilCastStart(SkillSet skill)
    {
        //애니메이션 재생 (casting)
    }
    public void OnMoveByPath(Vector2Int tile)
    {
        Debug.Log($"Target : {tile}");
        Debug.Log($"Start : {Start_X},{Start_Y}");

        MoveByPath(tile);
    }

    IEnumerator GenerateFSM()
    {
        bool turnEnd = false;
        while (!turnEnd)
        {
            StateProcess();

            yield return null;
        }

    }
  }
