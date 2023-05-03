using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class Player : CharactorMovement
{

    public SkillSet currSkill = null;
    // Start is called before the first frame update
    void Start()
    {
        SetPlayer();
        StartCoroutine(SetPos());
    }

    public override void SetPlayer()
    {
        myType = OB_TYPES.PLAYER;
        if(skilList == null)
            skilList = new List<SkillSet>();
        GameManager.Inst.characters.Add(this.gameObject);
    }
    IEnumerator SetPos()
    {
        int x, y;

        do
        {
            x = Random.Range(0, GameManager.Inst.columns);
            y = Random.Range(0, GameManager.Inst.rows);
        } while (GameManager.Inst.tiles[x, y].GetComponent<TileState>().isVisited == -5);


        my_Pos = new Vector2Int(y, x);

        float half = GameManager.Inst.scale * 0.5f;
        transform.position = new Vector3((float)my_Pos.x + half, 0, (float)my_Pos.y + half);

        GameManager.Inst.tiles[my_Pos.x, my_Pos.y].GetComponent<TileState>().my_obj = myType;
        GameManager.Inst.tiles[my_Pos.x, my_Pos.y].GetComponent<TileState>().SettingTarget(this.gameObject);
        UI_Manager.Inst.AddPlayer(my_Sprite);

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

                break;

        }
    }
  
    public void Picked(Vector2Int tile)
    {
        OB_TYPES tmp = GameManager.Inst.tiles[tile.x, tile.y].GetComponent<TileState>().my_obj;
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

        //�ִϸ��̼� ��� (action start)

        //�ִϸ��̼��� ������ ����
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
