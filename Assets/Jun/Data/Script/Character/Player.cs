using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class Player : CharactorMovement
{

    public SkillSet currSkill = null;
    public override void SetPos()
    {
        myType = OB_TYPES.PLAYER;
        if(skilList == null)
            skilList = new List<SkillSet>();
        SettingPos();
    }
    public void SettingPos()
    {
        int x, y, step;
        do
        {
            x = Random.Range(0, GetGMInst().rows);
            y = Random.Range(0, GetGMInst().columns);
            step = GetGMInst().tiles[x, y].GetComponent<TileState>().isVisited;
        } while (step == -5 || step == 0 );


        my_Pos = new Vector2Int(x, y);

        float half = GameManager.Inst.scale * 0.5f;
        transform.position = new Vector3((float)my_Pos.x + half, 0, (float)my_Pos.y + half);

        GetGMInst().tiles[my_Pos.x, my_Pos.y].GetComponent<TileState>().my_obj = myType;
        GetGMInst().tiles[my_Pos.x, my_Pos.y].GetComponent<TileState>().isVisited = 1;
        GetGMInst().tiles[my_Pos.x, my_Pos.y].GetComponent<TileState>().SetTarget(this.gameObject);
        UI_Manager.Inst.AddPlayer(my_Sprite);

    }
    // Update is called once per frame
    void Update()
    {
        StateProcess();
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
                //추후 UI 조작과 연결

                //임시
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Guard();
                    ChangeState(STATE.GUARD_UP);
                }
                

                break;
            case STATE.MOVE:
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    InitTileDistance();
                    GetGMInst().InitLayer();
                    ChangeState(_bfState);
                }
                break;

            case STATE.SKILL_CAST:
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    InitTileDistance();
                    GetGMInst().InitLayer();
                    ChangeState(_bfState);
                }
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
                gameObject.GetComponent<Picking>().enabled = false;
                break;

            case STATE.ACTION:
                gameObject.GetComponent<Picking>().enabled = true;
                break;
            case STATE.MOVE:
                gameObject.GetComponent<Picking>().enabled = true;

                break;

        }
    }
  
    public void Picked(Vector2Int tile)
    {
        OB_TYPES tmp = GetGMInst().tiles[tile.x, tile.y].GetComponent<TileState>().my_obj;
        switch (tmp)
        {
            case OB_TYPES.NONE:
                OnMoveByPath(tile);
                break;
            case OB_TYPES.MONSTER:
                OnAttack(tile);
                break;
            case OB_TYPES.PLAYER:
                break;
        }
    }

    public void OnCastingSkill(Vector2Int target, Vector2Int[] targets)
    {
        //애니메이션 재생 (casting end)
        //목표 회전
        Transform model = transform.Find("Model").GetComponent<Transform>();
        Vector3 dir = new Vector3((target.x + GetGMInst().scale / 2.0f) * _mySize, transform.position.y, (target.y + GetGMInst().scale / 2.0f) * _mySize) - model.position;
        dir.Normalize();
        StopAllCoroutines();
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
            GameObject target = GetGMInst().tiles[index.x, index.y].GetComponent<TileState>().OnMyTarget();

            if (target != null && target.GetComponent<BossMonster>() != null)
            {
                target.GetComponent<BossMonster>().TakeDamage(10.0f);
            }
        }
        GetGMInst().InitLayer();
        ChangeState(STATE.IDLE);
    }
    public void OnAttack(Vector2Int tile)
    {
        ChangeState(STATE.ATTACK);
        InitTileDistance();
        gameObject.GetComponent<Picking>().enabled = true;
    }
    public void OnSkilCastStart(SkillSet skill)
    {
        //애니메이션 재생 (casting)
        ChangeState(STATE.SKILL_CAST);
        currSkill = skill;
        InitTileDistance();
        gameObject.GetComponent<Picking>().enabled = true;
    }

    public override void OnMove()
    {
        ChangeState(STATE.MOVE);
        InitTileDistance();
        SetDistance();
        gameObject.GetComponent<Picking>().enabled = true;
    }
    public void OnMoveByPath(Vector2Int tile)
    {
        Debug.Log($"Target : {tile}");
        Debug.Log($"Start : {Start_X},{Start_Y}");

        MoveByPath(tile);
    }

    
}
