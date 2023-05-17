using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class Player : CharactorMovement
{

    public SkillSet currSkill = null;
    public void PlayerSetting(Character data)
    {
        myType = OB_TYPES.PLAYER;
        name = data.name;
        my_Sprite = data.Sprite;
        AttackPower = data.AttackPower;
        DeffencePower = data.DeffencePower;
        speed = data.Speed;
        ActionPoint = data.ActionPoint;
        skilList.Clear();
        if(data.Skill != null)
            foreach (var skill in data.Skill.List)
                skilList.Add(skill);
        SettingPos();
    }
    public void SettingPos()
    {
        int x, y, step;
        do
        {
            x = Random.Range(0, GetMMInst().rows);
            y = Random.Range(0, GetMMInst().columns);
            
            step = GetMMInst().tiles[new Vector2Int(x,y)].isVisited;
        } while (step == -5 || step == 0 );


        my_Pos = new Vector2Int(x, y);

        float half = MapManager.Inst.scale * 0.5f;
        transform.position = new Vector3((float)my_Pos.x + half, 0, (float)my_Pos.y + half);

        GetMMInst().tiles[my_Pos].my_obj = myType;
        GetMMInst().tiles[my_Pos].isVisited = 1;
        GetMMInst().tiles[my_Pos].SetTarget(this.gameObject);
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
                //���� UI ���۰� ����

                //�ӽ�
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
                    GetMMInst().InitLayer();
                    ChangeState(_bfState);
                }
                break;

            case STATE.SKILL_CAST:
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    InitTileDistance();
                    GetMMInst().InitLayer();
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
                //gameObject.GetComponent<Picking>().enabled = true;
                break;
            case STATE.MOVE:
                gameObject.GetComponent<Picking>().enabled = true;

                break;

        }
    }
  
    public void Picked(Vector2Int tile)
    {
        OB_TYPES tmp = GetMMInst().tiles[tile].gameObject.GetComponent<TileStatus>().my_obj;
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
        //�ִϸ��̼� ��� (casting end)
        //��ǥ ȸ��
        Transform model = transform.Find("Model").GetComponent<Transform>();
        Vector3 dir = new Vector3((target.x + GetMMInst().scale / 2.0f) * _mySize, transform.position.y, (target.y + GetMMInst().scale / 2.0f) * _mySize) - model.position;
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

        //�ִϸ��̼� ��� (action start)

        //�ִϸ��̼��� ������ ����
        foreach (var index in targets)
        {
            GameObject target = GetMMInst().tiles[index].gameObject.GetComponent<TileStatus>().OnMyTarget();

            if (target != null && target.GetComponent<BossMonster>() != null)
            {
                target.GetComponent<BossMonster>().TakeDamage(10.0f);
            }
        }
        GetMMInst().InitLayer();
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
        //�ִϸ��̼� ��� (casting)
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
