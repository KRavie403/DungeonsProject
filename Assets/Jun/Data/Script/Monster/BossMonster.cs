using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;

using Random = UnityEngine.Random;

public class BossMonster : Battle
{

    // Start is called before the first frame update
    public Vector2Int[] expendedPos;
    public Slider _bossHPUI;
    public int searchLength = 15;
    public int attackLength = 5;
    public Dictionary<Player, Vector2> close_targets = new();
    public Dictionary<Player, Vector2> searched_targets = new();
    public List<CharactorProperty> targetList = new();

    public SkillSet currSkill = null;
    public List<TileStatus> movealbeTiles;



    private Senario idleScenario;
    private Senario searchScenario;
    private Senario attackScenario;
    private Senario moveScenario;
    private Senario wanderScenario;
    void Start()
    {
        idleScenario = new Senario(STATE.IDLE, null, null);
        searchScenario = new Senario(STATE.SEARCH, null, null);    //search 결과에 따라 변경

        attackScenario = new Senario(STATE.ATTACK, null, null);    //search 발견 시 거리내에 있으면 타겟 공격
        moveScenario = new Senario(STATE.MOVE, null, null);    //search 발견 했으나 거리가 멀면 이동
        wanderScenario = new Senario(STATE.WANDER, null, null); //search 미발견 시 아무 위치로 n칸 이동


    }
    public void PlayerSetting()
    {
        movealbeTiles = new List<TileStatus>();
        myType = OB_TYPES.MONSTER;
        _bossHPUI = UI_Manager.Inst.MonsterUI.GetComponentInChildren<Slider>();
        if (skilList == null)
            skilList = new List<SkillSet>();
        SettingPos();
        _curState = STATE.CREATE;
    }
    public void SettingPos()
    {

        bool[] blocked = new bool[4];
        expendedPos = new Vector2Int[4];

        blocked = Enumerable.Repeat(true, 4).ToArray();

        do
        {
            my_Pos.x = Random.Range(0, MapManager.Inst.rows - 1);
            my_Pos.y = Random.Range(0, MapManager.Inst.columns - 1);
            if (MapManager.Inst.tiles.ContainsKey(my_Pos)
                && MapManager.Inst.tiles.ContainsKey(my_Pos + new Vector2Int(0, 1))
                && MapManager.Inst.tiles.ContainsKey(my_Pos + new Vector2Int(1, 0))
                && MapManager.Inst.tiles.ContainsKey(my_Pos + new Vector2Int(1, 1)))
            {
                blocked[0] = MapManager.Inst.tiles[my_Pos].is_blocked;
                blocked[1] = MapManager.Inst.tiles[my_Pos + new Vector2Int(0, 1)].is_blocked;
                blocked[2] = MapManager.Inst.tiles[my_Pos + new Vector2Int(1, 0)].is_blocked;
                blocked[3] = MapManager.Inst.tiles[my_Pos + new Vector2Int(1, 1)].is_blocked;
            }
        } while (blocked[0] || blocked[1] || blocked[2] || blocked[3]);



        float half = MapManager.Inst.scale * 0.5f;
        transform.position = new Vector3((float)my_Pos.x + half, 0, (float)my_Pos.y + half);
        int count = 0;
        for (int i = 0; i <= 1; i++)
        {
            for (int j = 0; j <= 1; j++)
            {
                if (MapManager.Inst.tiles.ContainsKey(my_Pos + new Vector2Int(i, j)))
                {
                    MapManager.Inst.tiles[my_Pos + new Vector2Int(i, j)].GetComponent<TileStatus>().my_obj = myType;
                    MapManager.Inst.tiles[my_Pos + new Vector2Int(i, j)].GetComponent<TileStatus>().isVisited = 1;
                    MapManager.Inst.tiles[my_Pos + new Vector2Int(i, j)].GetComponent<TileStatus>().is_blocked = true;
                    MapManager.Inst.tiles[my_Pos + new Vector2Int(i, j)].GetComponent<TileStatus>().SetTarget(this.gameObject);
                    expendedPos[count] = my_Pos + new Vector2Int(i, j);
                    count++;

                }
            }
        }
        UI_Manager.Inst.AddPlayer(my_Sprite);

    }
    public override void SetDistance()
    {
        movealbeTiles.Clear();
        GetMMInst().Init();
        foreach (var pos in expendedPos)
        {
            MapManager.Inst.tiles[pos].GetComponent<TileStatus>().isVisited = 1;
        }

        for (int step = 1; step <= searchLength; step++)
        {
            foreach (var tile in GetMMInst().tiles)
            {
                if (step <= attackLength)
                {
                    if (tile.Value.isVisited == step - 1)
                    {
                        TestAllDirection(tile.Value.gridPos.x, tile.Value.gridPos.y, step);
                        tile.Value.gameObject.layer = 9;
                    }

                    if (tile.Value.isVisited == step)
                        tile.Value.gameObject.layer = 9;
                    if (!movealbeTiles.Contains(tile.Value))
                        movealbeTiles.Add(tile.Value);

                    if (tile.Value.OnMyTarget() != null && tile.Value.my_obj == OB_TYPES.PLAYER)
                    {
                        if (!close_targets.ContainsKey(tile.Value.OnMyTarget().GetComponent<Player>()))
                            close_targets.Add(tile.Value.OnMyTarget().GetComponent<Player>(), tile.Key);

                    }
                    else
                    {
                        if (tile.Value.isVisited == step - 1)
                        {
                            TestAllDirection(tile.Value.gridPos.x, tile.Value.gridPos.y, step);
                            tile.Value.gameObject.layer = 8;
                        }

                        if (tile.Value.isVisited == step)
                            tile.Value.gameObject.layer = 8;
                        if (tile.Value.OnMyTarget() != null && tile.Value.my_obj == OB_TYPES.PLAYER)
                        {
                            if (!close_targets.ContainsKey(tile.Value.OnMyTarget().GetComponent<Player>()))
                                searched_targets.Add(tile.Value.OnMyTarget().GetComponent<Player>(), tile.Key);

                        }
                    }

                }
            }

        }
    }
    public override void OnMove()
    {
        ChangeState(STATE.MOVE);
        InitTileDistance();

        //Vector2Int target;
        //FindingPlayer()?.MoveByPath(target);
    }


    public override void ChangeState(STATE s)
    {

        if (_curState == s) return;
        _bfState = _curState;
        _curState = s;
        Debug.Log($"BOSS.STATE.{_curState}");

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
    public void OnCastingSkill(Vector2Int target, Vector2Int[] targets, UnityAction done = null)
    {
        if(GetMMInst().tiles.ContainsKey(target))
            StartCoroutine(CastingSkill(GetMMInst().tiles[target].transform.position, targets, done));
    }
    IEnumerator CastingSkill(Vector3 dir, Vector2Int[] targets, UnityAction done = null)
    {
        bool rote = false;
        Roatate(dir, () => rote = true);
        while (!rote)
        {
            yield return null;
        }

        foreach (var index in targets)
        {
            GameObject target = MapManager.Inst.tiles[index].GetComponent<TileStatus>().OnMyTarget();

            if (target != null && target.GetComponent<Player>() != null)
            {
                Damaging(currSkill, currSkill.Damage, currSkill.AttackIndex.ToArray());
            }
        }
        done?.Invoke();
    }
    public void OnAttack(Vector2Int tile)
    {
        ChangeState(STATE.ATTACK);
        InitTileDistance();
    }
    public void OnSkillCastStart(SkillSet skill)
    {

    }
    public void OnMoveByPath(List<TileStatus> path)
    {
        Debug.Log($"Target : {path}");
        Debug.Log($"Start : {Start_X}, {Start_Y}");

        MoveByPath(path);
    }

    int click(int a)
    {
        return 1;
    }
    public void StartFSM()
    {
        Debug.Log("FSM Start");
        StartCoroutine(GenerateFSM());
    }
    
    IEnumerator GenerateFSM()
    {
        bool turnEnd = false;
        bool is_done = false;
        Senario scenario = new Senario();
        ChangeState(STATE.SEARCH);
        close_targets = new Dictionary<Player, Vector2>();
        while (!turnEnd)
        {
            is_done = false;
            yield return new WaitForSeconds(0.5f);

            switch (_curState)
            {

                case STATE.CREATE:
                    break;

                case STATE.IDLE:
                    if (scenario.senarioValue > idleScenario.senarioValue)
                        _curState = STATE.SKILL_CAST;
                    break;

                case STATE.ACTION:
                    ChangeState(STATE.SEARCH);
                    is_done = true;
                    break;
                case STATE.SEARCH:
                    this.SetDistance();
                    yield return new WaitForSeconds(0.5f);

                    close_targets = close_targets.OrderBy(o => -o.Value.x + o.Value.y).ToDictionary(pair => pair.Key, pair => pair.Value);
                    if (close_targets.Count == 0)
                    {
                        scenario = searchScenario;
                        ChangeState(STATE.WANDER);
                    }
                    else
                    {
                        int around_target_count = 0;
                        foreach (var target in close_targets)
                        {
                            if (target.Value.x < attackLength)
                            {
                                around_target_count++;
                            }
                        }
                        if (around_target_count == 0)
                        {
                            ChangeState(STATE.MOVE);
                        }
                        else
                        {
                            ChangeState(STATE.SKILL_CAST);
                        }
                    }
                    is_done = true;

                    break;
                case STATE.MOVE:
                    {
                        Debug.Log("Boss Move Start");

                        var target = close_targets.Aggregate((x, y) => x.Value.x < y.Value.x ? x : y).Key;
                        var start = GetMMInst().tiles[my_Pos];
                        var end = GetMMInst().tiles[target.my_Pos];
                        List<TileStatus> path = new List<TileStatus>();

                        path = PathFinder.Inst.FindPath(start, end);
                        foreach(var tile in path)
                        {
                            tile.gameObject.layer = 8;
                        }
                        if (path.Count == 0)
                        {
                            Debug.Log("Boss Can't find Path");
                            ChangeState(STATE.WANDER);
                        }

                        MoveByPath(path, () => is_done = true);
                        while (!is_done)
                            yield return null;
                        Debug.Log("Boss Move Done");

                    }

                    break;

                case STATE.SKILL_CAST:
                    {
                        yield return new WaitForSeconds(0.5f);
                        if (targetList.Count == 1)
                        {
                            currSkill = skilList[2];

                            GetMMInst().InitLayer();
                            List<Vector2Int> attackIndex = new List<Vector2Int>();
                            Vector2Int target = targetList.First().my_Pos;
                            foreach (var tile in currSkill.AttackIndex)
                            {
                                if (GetMMInst().tiles.ContainsKey(target + tile))
                                {
                                    GetMMInst().tiles[target + tile].gameObject.layer = 8;
                                    attackIndex.Add(target + tile);
                                }
                            }
                            Vector2Int dir = target - my_Pos;
                            
                            OnCastingSkill(dir, attackIndex.ToArray(), () => is_done = true);

                            while (!is_done)
                                yield return null;
                        }
                        else
                        {
                            currSkill = skilList[2];

                            List<Vector2Int> targets = new List<Vector2Int>();
                            foreach (var target in targetList)
                                targets.Add(target.my_Pos);

                            GetMMInst().InitLayer();
                            List<Vector2Int> attackIndex = new List<Vector2Int>();
                            foreach (var target in targets)
                            {
                                foreach (var tile in currSkill.AttackIndex)
                                {
                                    if (GetMMInst().tiles.ContainsKey(target + tile))
                                    {
                                        GetMMInst().tiles[target + tile].gameObject.layer = 8;
                                        attackIndex.Add(target + tile);
                                    }
                                }

                            }
                            int rnd = Random.Range(0, targets.Count);
                            OnCastingSkill(targets[rnd] - my_Pos, attackIndex.ToArray(), () => is_done = true);

                            while (!is_done)
                                yield return null;
                        }
                        Debug.Log("Boss Skill Cast is Done");
                    }
                    ChangeState(STATE.ACTION);
                    break;
                case STATE.WANDER:
                    {
                        // Move to RandomPos
                        List<TileStatus> path = new List<TileStatus> { };
                        TileStatus start = GetMMInst().tiles[my_Pos];
                        int rnd = 0;
                        TileStatus end;
                        do
                        {
                            rnd = Random.Range(0, movealbeTiles.Count - 1);
                            end = movealbeTiles[rnd];
                            path = PathFinder.Inst.FindPath(start, end);

                        } while (Vector2Int.Distance(my_Pos, end.gridPos) < 3.0f && path.Count == 0);

                        MoveByPath(path, () => is_done = true);
                        while (!is_done)
                            yield return null;
                    }
                    is_done = true;

                    break;
            }

            
            curAP--;

            if (curAP == 10)
            {
                GetMMInst().InitLayer();
                GameManager.Inst.ChangeTurn();
                StopAllCoroutines();
                turnEnd = true;

            }
            yield return null;
        }

    }
    protected override void TakeDamage(float dmg)
    {
        Debug.Log($"Get Damage : {dmg}");
        StartCoroutine(TakingDamge(dmg));
    }

    IEnumerator TakingDamge(float dmg)
    {
        float target = curHP - dmg;

        while (!Mathf.Approximately(curHP, target))
        {
            curHP = Mathf.Lerp(curHP, target, Time.deltaTime);
            _bossHPUI.value = curHP / MaxHP;
            yield return null;
        }
    }

   
}
