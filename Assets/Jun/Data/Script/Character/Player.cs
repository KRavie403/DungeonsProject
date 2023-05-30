using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Battle
{

    public SkillSet currSkill = null;
    public void PlayerSetting(Character data)
    {
        myType = OB_TYPES.PLAYER;
        name = data.name;
        my_Sprite = data.Sprite;
        OrgAttackPower = data.AttackPower;
        OrgDeffencePower = data.DeffencePower;
        OrgSpeed = data.Speed;
        curAP = ActionPoint = data.ActionPoint;
        skilList.Clear();

        //선택된 스킬 리스트 입력 필요 
        foreach (var skill in data.Skill.List)
            skilList.Add(skill);
        SettingPos();
    }
    public void SettingPos()
    {
        int x, y;
        bool blocked = true;
        do
        {
            x = Random.Range(30, 50);
            y = Random.Range(30, 50);
            if(GetMMInst().tiles.ContainsKey(new Vector2Int(x,y)))
                blocked = GetMMInst().tiles[new Vector2Int(x,y)].is_blocked;
        } while (blocked);


        my_Pos = new Vector2Int(x, y);

        float half = MapManager.Inst.scale * 0.5f;
        transform.position = new Vector3((float)my_Pos.x + half, 0, (float)my_Pos.y + half);

        GetMMInst().tiles[my_Pos].my_obj = myType;
        GetMMInst().tiles[my_Pos].is_blocked = true;
        GetMMInst().tiles[my_Pos].SetTarget(this.gameObject);
        UI_Manager.Inst.AddPlayer(my_Sprite);

    }
    void Start()
    {
    }

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
                //임시
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Guard();
                    ChangeState(STATE.GUARD_UP);
                }
                break;
            case STATE.INTERACT:
            case STATE.MOVE:
            case STATE.SKILL_CAST:
                GetMMInst().tiles[my_Pos].isVisited = 1;

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
            case STATE.ACTION:
            case STATE.IDLE:
                gameObject.GetComponent<Picking>().enabled = false;
                break;
            case STATE.INTERACT:
            case STATE.MOVE:
            case STATE.SKILL_CAST:
                gameObject.GetComponent<Picking>().enabled = true;
                break;

        }
    }
  
    public void Picked(Vector2Int tile)
    {
        OB_TYPES tmp = GetMMInst().tiles[tile].gameObject.GetComponent<TileStatus>().my_obj;
        switch (tmp)
        {
            case OB_TYPES.CHEST: 
                UI_Manager.Inst.ChestUI.SetActive(true);
                break;
            case OB_TYPES.TELEPORT:
                UI_Manager.Inst.TPUI.SetActive(true);
                break;
            case OB_TYPES.PLAYER:
                break;
        }
    }

    public void OnCastingSkill(Vector2Int target, Vector2Int[] targets)
    {
        //애니메이션 재생 (casting end)
        //목표 회전
        curAP -= currSkill.Cost;

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

        //애니메이션 재생 (action start)

        bool done = false;

        switch (currSkill.myEType)
        {
            case SkillSet.EffectType.DamageEffcet:
                StartCoroutine(Damaging(currSkill, currSkill.Damage, targets, () => done = true));
                break;
            case SkillSet.EffectType.SpecialEffect:
                //SpecialEffect(currSkill, currSkill.Damage, targets, () => done = true)
            case SkillSet.EffectType.StatEffect:
                StartCoroutine(StatModifiring(currSkill, targets, () => done = true));
                break;
        }
        while (!done)
        {
            yield return null;
        }

        //애니메이션이 끝나고 실행
        UI_Manager.Inst.StateUpdate((int)GetGMInst().curCharacter);

        if (curAP == 10)
            GetGMInst().ChangeTurn();


        GetMMInst().InitLayer();
        ChangeState(STATE.IDLE);
    }
    
    
    public void OnSkilCastStart(SkillSet skill)
    {
        //애니메이션 재생 (casting)
        ChangeState(STATE.SKILL_CAST);
        currSkill = skill;
        InitTileDistance();
    }
    override public void SetDistance()
    {
        for (int step = 1; step <= curAP; step++)
        {
            foreach (TileStatus tile in GetMMInst().tiles.Values)
            {
                if (tile.isVisited == step - 1)
                    TestAllDirection(tile.gridPos.x, tile.gridPos.y, step);
            }
        }

        RefreshArea();
    }
    public override void OnMove()
    {
        ChangeState(STATE.MOVE);
        InitTileDistance();
        this.SetDistance();
    }
    public override void OnInteract()
    {
        ChangeState(STATE.INTERACT);
    }
    public void OnMoveByPath(List<TileStatus> path)
    {
        Debug.Log($"Target : {path}");
        Debug.Log($"Start : {Start_X},{Start_Y}");

        MoveByPath(path);
    }

    protected override void TakeDamage(float dmg)
    {
        myAnim.SetTrigger("Damaged");
        Mathf.Lerp(curHP, curHP - dmg, Time.deltaTime);
        Debug.Log($"Get Damage, Current HP : {curHP}");
    }

    
}
