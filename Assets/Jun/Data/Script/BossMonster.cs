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
    private void Start()
    {
        
        SetPlayer();
        StartCoroutine(SetPos());
    }
    public override void SetPlayer()
    {
        myType = OB_TYPES.MONSTER;
        if (skilList == null)
            skilList = new List<SkillSet>();
        GameManager.GM.characters.Add(this.gameObject);
    }
    IEnumerator SetPos()
    {
        //GameManager.GM.Players.Add(this.gameObject);
        
        int x, y;

        do
        {
            x = Random.Range(0, GameManager.GM.columns);
            y = Random.Range(0, GameManager.GM.rows);
        } while (GameManager.GM.tiles[x, y].GetComponent<TileState>().isVisited == -5);


        my_Pos = new Vector2Int(y, x);

        float half = GameManager.GM.scale * 0.5f;
        transform.position = new Vector3((float)my_Pos.x + half, 0, (float)my_Pos.y + half);

        for (int i = 0; i <= 1; i++)
        {
            for (int j = 0; j <= 1; j++)
            {
                GameManager.GM.tiles[my_Pos.x + i, my_Pos.y + j].GetComponent<TileState>().my_obj = myType;
                GameManager.GM.tiles[my_Pos.x + i, my_Pos.y + j].GetComponent<TileState>().isVisited = -2;
                GameManager.GM.tiles[my_Pos.x + i, my_Pos.y + j].GetComponent<TileState>().SettingTarget(this.gameObject);
            }
        }
        GameManager.UM.AddPlayer(my_Sprite);

        yield return null;
    }

    public override void OnMove()
    {
        ChangeState(STATE.MOVE);
        InitTileDistance();
        SetDistance();
        gameObject.GetComponent<Picking>().enabled = true;
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
        //�ִϸ��̼� ��� (casting end)
        //��ǥ ȸ��
        Vector3 dir = new Vector3((target.x + GameManager.GM.scale / 2.0f) * _mySize, transform.position.y, (target.y + GameManager.GM.scale / 2.0f) * _mySize) - transform.position;
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
            GameObject target = GameManager.GM.tiles[index.x, index.y].GetComponent<TileState>().OnMyTarget();

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
        //�ִϸ��̼� ��� (casting)
    }


    public void OnMoveByPath(Vector2Int tile)
    {
        Debug.Log($"Target : {tile}");
        Debug.Log($"Start : {Start_X},{Start_Y}");

        MoveByPath(tile);
    }

}
