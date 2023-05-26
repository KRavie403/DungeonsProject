using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public class BossMonster : Battle
{
    // Start is called before the first frame update
    public Vector2Int[] expendedPos;
    public Slider _bossHPUI;
    public int searchLenght = 10;
    public int attackLenght = 5;
    public Dictionary<Player, Vector2> close_targets = new();
    public List<CharactorProperty> targetList = new();
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
        myType = OB_TYPES.MONSTER;
        _bossHPUI = UI_Manager.Inst.MonsterUI.GetComponentInChildren<Slider>();
        if (skilList == null)
            skilList = new List<SkillSet>();
        StartCoroutine(SettingPos());

    }
    IEnumerator SettingPos()
    {

        Vector2Int my_Pos = new Vector2Int();
        bool[] blocked = new bool[4];
        expendedPos = new Vector2Int[4];

        blocked = Enumerable.Repeat(true, 4).ToArray();
        
        do
        {
            my_Pos.x = Random.Range(0, MapManager.Inst.rows-1);
            my_Pos.y = Random.Range(0, MapManager.Inst.columns-1);
            if (MapManager.Inst.tiles.ContainsKey(my_Pos) 
                && MapManager.Inst.tiles.ContainsKey(my_Pos+ new Vector2Int(0,1))
                && MapManager.Inst.tiles.ContainsKey(my_Pos+ new Vector2Int(1,0))
                && MapManager.Inst.tiles.ContainsKey(my_Pos+ new Vector2Int(1,1)))
            {
                blocked[0] = MapManager.Inst.tiles[my_Pos].is_blocked;
                blocked[1] = MapManager.Inst.tiles[my_Pos + new Vector2Int(0,1)].is_blocked;
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

        yield return null;
    }
    public override void SetDistance()
    {
        GetMMInst().Init();
        foreach (var pos in expendedPos)
        {
            Debug.Log(pos);
            MapManager.Inst.tiles[pos].GetComponent<TileStatus>().isVisited = 1;
        }

        for (int step = 1; step <= searchLenght; step++)
        {
            foreach (var tile in GetMMInst().tiles)
            {
                if (step <= attackLenght)
                {
                    if (tile.Value.isVisited == step - 1)
                    {
                        TestAllDirection(tile.Value.gridPos.x, tile.Value.gridPos.y, step);
                        tile.Value.gameObject.layer = 9;
                    }

                    if (tile.Value.isVisited == step)
                        tile.Value.gameObject.layer = 9;
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
                }
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
        StartCoroutine(CastingSkill(GetMMInst().tiles[target].transform.position, targets));
    }
    IEnumerator CastingSkill(Vector3 dir, Vector2Int[] targets)
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
                    SearchingPlayer((close_targets));
                    close_targets.OrderBy(o => -o.Value.x + o.Value.y);
                    if (close_targets.Count != 0)
                    {
                        scenario = searchScenario;
                        ChangeState(STATE.WANDER);
                    }
                    else
                    {
                        int around_target_count = 0;
                        foreach (var target in close_targets)
                        {
                            if (target.Value.x <= attackLenght)
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
                    break;
                case STATE.MOVE:
                    if (_curState == STATE.SEARCH || _curState == STATE.MOVE)
                    {
                        var target = close_targets.Aggregate((x, y) => x.Value.x < y.Value.x ? x : y).Key;
                        bool moved= false;
                        var start = GetMMInst().tiles[my_Pos];
                        var end = GetMMInst().tiles[target.my_Pos];
                        List<TileStatus> path = PathFinder.Inst.FindPath(start, end);

                        MoveByPath(path, () => moved = true);
                        while (!moved)
                            yield return null;
                    }
                    else if (_curState == STATE.WANDER)
                    {
                        // Move to RandomPos
                        List<TileStatus> movealbeTiles = new List<TileStatus>();
                        for (int i = my_Pos.x - curAP; i <= my_Pos.x + curAP; i++)
                        {
                            for (int j = my_Pos.y - curAP; i <= my_Pos.y + curAP; j++)
                            {
                                Vector2Int pos = new Vector2Int(i, j);
                                if (!MapManager.Inst.CheckIncludedIndex(pos))
                                    break;
                                movealbeTiles.Add(MapManager.Inst.tiles[pos]);
                            }
                        }

                        int rnd = Random.Range(0, movealbeTiles.Count-1);
                        bool moved= false;
                        TileStatus start = GetMMInst().tiles[my_Pos];
                        TileStatus end = movealbeTiles[rnd];
                        List<TileStatus> path = PathFinder.Inst.FindPath(start, end);

                        MoveByPath(path, () => moved = true);
                        while (!moved)
                            yield return null;
                    }
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

    private void SearchingPlayer( Dictionary <Player,Vector2> close_targets)
    {
        foreach (var player in GameManager.Inst.characters)
        {
            CharactorProperty info = player.GetComponent<CharactorProperty>();
            float dist = Vector2Int.Distance(my_Pos, info.my_Pos);
            float agro = info.Agro;
            
            if (info.myType == OB_TYPES.PLAYER &&  dist < searchLenght)
            {
                close_targets.Add(player.GetComponent<Player>(), new Vector2(dist, agro));
                targetList.Add(info);
            }
        }
    }
    
}
