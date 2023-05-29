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


    Queue<Senario> senarios;
    private Senario attackScenario;
    private Senario moveScenario;

    void Start()
    {
        senarios = new Queue<Senario>();

        moveScenario = new Senario(STATE.MOVE, GetMMInst().tiles[new Vector2Int(35, 45)], null);
        senarios.Enqueue(moveScenario);
        moveScenario = new Senario(STATE.MOVE, GetMMInst().tiles[new Vector2Int(55, 38)], null);
        senarios.Enqueue(moveScenario);
        moveScenario = new Senario(STATE.MOVE, GetMMInst().tiles[new Vector2Int(35, 18)], null);
        senarios.Enqueue(moveScenario);

        attackScenario = new Senario(STATE.ATTACK, null, null);
        senarios.Enqueue(attackScenario);


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

        //do
        //{
        //    my_Pos.x = Random.Range(0, MapManager.Inst.rows - 1);
        //    my_Pos.y = Random.Range(0, MapManager.Inst.columns - 1);
        //    if (MapManager.Inst.tiles.ContainsKey(my_Pos)
        //        && MapManager.Inst.tiles.ContainsKey(my_Pos + new Vector2Int(0, 1))
        //        && MapManager.Inst.tiles.ContainsKey(my_Pos + new Vector2Int(1, 0))
        //        && MapManager.Inst.tiles.ContainsKey(my_Pos + new Vector2Int(1, 1)))
        //    {
        //        blocked[0] = MapManager.Inst.tiles[my_Pos].is_blocked;
        //        blocked[1] = MapManager.Inst.tiles[my_Pos + new Vector2Int(0, 1)].is_blocked;
        //        blocked[2] = MapManager.Inst.tiles[my_Pos + new Vector2Int(1, 0)].is_blocked;
        //        blocked[3] = MapManager.Inst.tiles[my_Pos + new Vector2Int(1, 1)].is_blocked;
        //    }
        //} while (blocked[0] || blocked[1] || blocked[2] || blocked[3]);
        my_Pos = new Vector2Int(35, 66);


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
    public void StartScnarioMachine()
    {
        Debug.Log("FSM Start");
        StartCoroutine(GenerateSMachine());
    }

    IEnumerator GenerateSMachine()
    {
        bool turnEnd = false;
        bool is_done = false;
        close_targets = new Dictionary<Player, Vector2>();

        Senario cur_senario = senarios.Peek();
        ChangeState(cur_senario.senarioValue);

        while (!turnEnd)
        {
            is_done = false;
            yield return new WaitForSeconds(0.5f);

            switch (_curState)
            {

                case STATE.CREATE:
                    break;

                case STATE.IDLE:
                    break;

                case STATE.ATTACK:
                    close_targets.Clear();
                    SetDistance();
                    break;
                case STATE.SEARCH:
                    

                    break;
                case STATE.MOVE:
                    var path = PathFinder.Inst.FindPath(GetMMInst().tiles[my_Pos], cur_senario.targettile);
                    MoveByPath(path, ()=>is_done = true);
                    Debug.Log($"MoveByPath{path}");
                    while (!is_done)
                    {
                        if(curAP == ActionPoint|| cur_senario.targettile.gridPos == my_Pos)
                        {
                            senarios.Dequeue();
                            cur_senario = senarios.Peek();
                            Debug.Log($"senario Count : {senarios.Count}");
                        }
                        
                        yield return null;
                    }
                    if (cur_senario.targettile.gridPos == my_Pos)
                    {
                        senarios.Dequeue();
                        cur_senario = senarios.Peek();
                        Debug.Log($"senario Count : {senarios.Count}");
                    }


                    break;

                case STATE.SKILL_CAST:
                     
                    break;
                case STATE.WANDER:
                    break;
                case STATE.ACTION:
                    ChangeState(cur_senario.senarioValue);
                    break;
            }



            if (curAP == ActionPoint)
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
        Debug.Log($"{this.gameObject.name} => Get Damage : {dmg}");
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
