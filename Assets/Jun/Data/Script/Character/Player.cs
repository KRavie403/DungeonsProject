using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharactorMovement
{
    public enum STATE
    {
        CREATE, ACTION, MOVE, ATTACK, SKILL_CAST, GUARD_UP, IDLE
    }
    public STATE _bfState;
    public STATE _curState = STATE.CREATE;
    public SkillSet currSKill = null;
    // Start is called before the first frame update
    void Start()
    {
        SetPlayer();
        StartCoroutine(SetPos());
    }

    void SetPlayer()
    {
        myType = OB_TYPES.PLAYER;
        if(skilList == null)
            skilList = new List<SkillSet>();
        GameManager.GM.Players.Add(this.gameObject);
    }
    IEnumerator SetPos()
    {
        int x, y;

        do
        {
            x = Random.Range(0, GameManager.GM.columns);
            y = Random.Range(0, GameManager.GM.rows);
        } while (GameManager.GM.tiles[x, y].GetComponent<TileState>().isVisited == -5);


        my_Pos = new Vector2Int(y, x);

        float half = GameManager.GM.scale * 0.5f;
        transform.position = new Vector3((float)my_Pos.x + half, 0, (float)my_Pos.y + half);

        GameManager.GM.tiles[my_Pos.x, my_Pos.y].GetComponent<TileState>().my_obj = myType;
        GameManager.GM.tiles[my_Pos.x, my_Pos.y].GetComponent<TileState>().SettingTarget(this.gameObject);
        GameManager.UM.AddPlayer(my_Sprite);

        yield return null;
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
                    ChangeState(_bfState);
                }
                break;

            case STATE.SKILL_CAST:
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    InitTileDistance();
                    ChangeState(_bfState);
                }
                break;
        }
    }

    public void ChangeState(STATE s)
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


                break;
            case STATE.MOVE:

                break;

        }
    }
    public STATE GetState()
    {
        return _curState;
    }
    public void Picked(Vector2Int tile)
    {
        OB_TYPES tmp = GameManager.GM.tiles[tile.x, tile.y].GetComponent<TileState>().my_obj;
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
    public void CastingSkill(Vector2Int[] targets)
    {
        //애니메이션 재생
        foreach (var index in targets)
        {
            GameObject target = GameManager.GM.tiles[index.x, index.y].GetComponent<TileState>().OnMyTarget();

            if(target != null && target.GetComponent<BossMonster>()!= null)
            {
                target.GetComponent<BossMonster>().OnDamage(10.0f);
            }
        }
    }
    public void OnAttack(Vector2Int tile)
    {
        ChangeState(STATE.ATTACK);
        InitTileDistance();
        gameObject.GetComponent<Picking>().enabled = true;
    }
    public void OnSkillCast(SkillSet skill)
    {
        ChangeState(STATE.SKILL_CAST);
        currSKill = skill;
        InitTileDistance();
        gameObject.GetComponent<Picking>().enabled = true;
    }

    public void OnMove()
    {
        //InitMoveStart();
        //MoveToTile(tile);
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

    void InitTileDistance()
    {
        Start_X = my_Pos.x;
        Start_Y = my_Pos.y;
        GameManager.GM.tiles[Start_X, Start_Y].GetComponent<TileState>().reSetTile();

    }

}
