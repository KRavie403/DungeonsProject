using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BossMonster : CharactorMovement
{
    // Start is called before the first frame update
    public Slider _bossHPUI;
    private float Hp;

    public void PlayerSetting()
    {
        myType = OB_TYPES.MONSTER;
        if (skilList == null)
            skilList = new List<SkillSet>();
        StartCoroutine(SettingPos());

    }
    IEnumerator SettingPos()
    {

        Vector2Int my_Pos = new Vector2Int();

        do
        {
            my_Pos.x = Random.Range(0, MapManager.Inst.rows);
            my_Pos.y = Random.Range(0, MapManager.Inst.columns);
        } while (MapManager.Inst.tiles.ContainsKey(my_Pos) && MapManager.Inst.tiles[my_Pos].isVisited == -5);



        float half = MapManager.Inst.scale * 0.5f;
        transform.position = new Vector3((float)my_Pos.x + half, 0, (float)my_Pos.y + half);

        for (int i = 0; i <= 1; i++)
        {
            for (int j = 0; j <= 1; j++)
            {
                if (MapManager.Inst.tiles.ContainsKey(my_Pos + new Vector2Int(i, j)))
                {
                    MapManager.Inst.tiles[my_Pos + new Vector2Int(i, j)].GetComponent<TileStatus>().my_obj = myType;
                    MapManager.Inst.tiles[my_Pos + new Vector2Int(i, j)].GetComponent<TileStatus>().isVisited = -2;
                    MapManager.Inst.tiles[my_Pos + new Vector2Int(i, j)].GetComponent<TileStatus>().is_blocked = true;
                    MapManager.Inst.tiles[my_Pos + new Vector2Int(i, j)].GetComponent<TileStatus>().SetTarget(this.gameObject);

                }
            }
        }
        UI_Manager.Inst.AddPlayer(my_Sprite);

        yield return null;
    }
    override public void SetDistance()
    {
        List<TileStatus> searchTileArea = new List<TileStatus>();

        
        for (int i = my_Pos.x - curAP; i <= my_Pos.x + curAP; i++)
        {
            for (int j = my_Pos.y - curAP; i <= my_Pos.y + curAP; j++)
            {
                Vector2Int pos = new Vector2Int(i, j);
                if (!MapManager.Inst.CheckIncludedIndex(pos))
                    break;
                searchTileArea.Add(MapManager.Inst.tiles[pos]);
            }
        }
        
        for (int step = 1; step <= curAP; step++)
        {
            foreach (TileStatus tile in searchTileArea)
            {
                if (tile.isVisited == step - 1)
                {
                    TestAllDirection(tile.gridPos.x, tile.gridPos.y, step);
                    //obj 주변 x+1 / y + 1방향도 step값 변경, 예외처리 필요
                    //if 인접타일이 못가는 곳인가 ? step 날림
                    tile.gameObject.layer = 9;
                }
                if (tile.GetComponent<TileStatus>().isVisited == step)
                    tile.gameObject.layer = 9;
            }
        }

    }
    public override void OnMove()
    {
        for (int i = 0; i <= 1; i++)
        {
            for (int j = 0; j <= 1; j++)
            {
                if (MapManager.Inst.tiles.ContainsKey(my_Pos + new Vector2Int(i, j)))
                    MapManager.Inst.tiles[my_Pos + new Vector2Int(i, j)].GetComponent<TileStatus>().is_blocked = true;
            }
        }
        ChangeState(STATE.MOVE);
        InitTileDistance();
        SetDistance();

        //Vector2Int target;
        //FindingPlayer()?.MoveByPath(target);
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
        Vector3 dir = new Vector3((target.x + MapManager.Inst.scale / 2.0f) * _mySize, transform.position.y, (target.y + MapManager.Inst.scale / 2.0f) * _mySize) - transform.position;
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
            GameObject target = MapManager.Inst.tiles[index].GetComponent<TileStatus>().OnMyTarget();

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
    public void OnMoveByPath(List<TileStatus> path)
    {
        Debug.Log($"Target : {path}");
        Debug.Log($"Start : {Start_X},{Start_Y}");

        MoveByPath(path);
    }

    public void StartFSM()
    {

        StartCoroutine(GenerateFSM());
    }
    IEnumerator GenerateFSM()
    {
        bool turnEnd = false;
        bool is_done = false;
        while (!turnEnd)
        {
            
            switch (_curState)
            {
                case STATE.CREATE:
                    break;

                case STATE.IDLE:
                    break;

                case STATE.ACTION:
                    ChangeState(STATE.SEARCH);
                    is_done = true;
                    break;
                case STATE.SEARCH:
                    //SearchingPlayer(() => is_done = true);
                    
                    break;
                case STATE.MOVE:
                    break;

                case STATE.SKILL_CAST:
                    break;
            }

            while (!is_done)
                yield return null;

            curAP--;

            if (curAP == 10)
            {
                GameManager.Inst.ChangeTurn();
                StopAllCoroutines();
                turnEnd = true;
            }
            yield return null;
        }

    }

    
  }
