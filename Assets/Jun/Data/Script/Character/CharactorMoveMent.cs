using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public abstract class CharactorMovement : CharactorProperty
{
    enum Direction { Front, Left, Right, Back }
    
    public STATE _bfState;
    public STATE _curState = STATE.CREATE;

    public bool findPath = false;

    protected int Start_X, Start_Y;

    Coroutine coMove = null;
    virtual public void ChangeState(STATE s) { }
    public STATE GetState()
    {
        return _curState;
    }
    virtual public void OnMove() { }
    virtual public void OnInteract() { }
    protected void InitTileDistance()
    {
        Vector2Int Start = my_Pos;
        GetMMInst().tiles[Start].GetComponent<TileStatus>().reSetTile();
    }
    public float CheckAP()
    {
        return curAP;
    }
    protected void MoveToTile(Vector2Int target, UnityAction done = null)
    {
        if (coMove != null)
        {
            StopCoroutine(coMove);
            coMove = null;
        }
        coMove = StartCoroutine(MovingToTile(target, done));
    }
    protected void Roatate(Vector3 dir, UnityAction done = null)
    {
        StartCoroutine(Rotating(dir, done));
    }
    protected void MoveByPath(List<TileStatus> path = null, UnityAction done = null)
    {
        StopAllCoroutines();
        
        if (path != null)
            findPath = true;

        if (findPath)
            StartCoroutine(MovingByPath(path, done));

    }
    bool CastDirectionTile(int x, int y, int step, Direction dir)
    {
        switch (dir)
        {
            case Direction.Left:
                if (x - 1 > -1 && GetMMInst().tiles.ContainsKey(new Vector2Int(x - 1, y)) &&
                    GetMMInst().tiles[new Vector2Int(x - 1, y)].isVisited == step) return true;
                break;
            case Direction.Right:
                if (x + 1 < GetMMInst().columns && GetMMInst().tiles.ContainsKey(new Vector2Int(x + 1, y)) &&
                    GetMMInst().tiles[new Vector2Int(x + 1, y)].isVisited == step) return true;
                break;
            case Direction.Back:
                if (y - 1 > -1 && GetMMInst().tiles.ContainsKey(new Vector2Int(x, y - 1)) &&
                    GetMMInst().tiles[new Vector2Int(x, y - 1)].isVisited == step) return true;
                break;
            case Direction.Front:
                if (y + 1 < GetMMInst().rows && GetMMInst().tiles.ContainsKey(new Vector2Int(x, y + 1)) &&
                    GetMMInst().tiles[new Vector2Int(x, y + 1)].isVisited == step) return true;
                break;
        }
        return false;
    }
    void SetVisited (int x, int y, int step)
    {
        if(GetMMInst().tiles[new Vector2Int (x, y)])
            GetMMInst().tiles[new Vector2Int(x, y)].isVisited = step;
    }
    virtual public void SetDistance()
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

    private void RefreshArea()
    {
        foreach (TileStatus tile in GetMMInst().tiles.Values)
        {
            if (tile.isVisited > -1)
                tile.gameObject.layer = 9;
        }
    }
    public void RefreshStatus()
    {
        AttackPower = OrgAttackPower;
        DeffencePower = OrgDeffencePower;
        Speed = OrgSpeed;

        foreach (var item in AD_stat)
        {
            AttackPower += item.AttackPower;
            DeffencePower += item.DeffencePower;
            Speed += item.Speed;
        }
    }
    protected void TestAllDirection(int x, int y, int step)
    {
        if (CastDirectionTile(x, y, -1, Direction.Front))
            SetVisited(x, y + 1, step);
        if (CastDirectionTile(x, y, -1, Direction.Left))
            SetVisited(x - 1, y, step);
        if (CastDirectionTile(x, y, -1, Direction.Right))
            SetVisited(x + 1, y, step);
        if (CastDirectionTile(x, y, -1, Direction.Back))
            SetVisited(x, y - 1, step);
    }

    protected IEnumerator MovingToTile(Vector2Int target, UnityAction done = null)
    {
        Vector3 dir = new Vector3((target.x + GetMMInst().scale / 2.0f) * _mySize, transform.position.y, (target.y + MapManager.Inst.scale / 2.0f) * _mySize) - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        bool rote = false;
        Roatate(dir, () => rote = true);
        while (!rote)
        {
            yield return null;
        }

        //myAnim.SetBool("isMoving", true);

        while (dist > 0.0f)
        {
            float delta = MoveSpeed * Time.deltaTime;
            if (dist - delta < 0.0f)
            {
                delta = dist;
            }
            dist -= delta;
            transform.Translate(dir * delta, Space.World);
            yield return null;
        }

        //myAnim.SetBool("isMoving", false);
        done?.Invoke();

    }
    IEnumerator MovingByPath(List<TileStatus> path, UnityAction arrive = null)
    {
        //myAnim.SetFloat("Speed", MoveSpeed);
        GetMMInst().tiles[my_Pos].is_blocked = false;

        Vector2Int dest_pos = my_Pos;
        for (int i = 0; i < path.Count;)
        {
            bool done = false;
            MoveToTile(path[i].GetComponent<TileStatus>().gridPos, () => done = true);
            while (!done)
            {
                yield return null;
            }
            dest_pos = path[i].GetComponent<TileStatus>().gridPos;

            curAP--;
            UI_Manager.Inst.StateUpdate((int)GetGMInst().curCharacter);
            if (curAP == 10)
                break;
            i++;
        }

        if (curAP == 10)
            GetGMInst().ChangeTurn();

        else
        {
            if (myType == OB_TYPES.PLAYER)
                GetComponent<Player>().ChangeState(STATE.ACTION);
            else
                GetComponent<BossMonster>().ChangeState(STATE.ACTION);
        }
        GetMMInst().Init();
        my_Pos = dest_pos;
        GetMMInst().tiles[dest_pos].my_obj = OB_TYPES.PLAYER;
        GetMMInst().tiles[dest_pos].my_target = this.gameObject;
        GetMMInst().tiles[dest_pos].is_blocked = true;

        //myAnim.SetFloat("Speed", 0);
        arrive?.Invoke();

    }

    

    IEnumerator Rotating(Vector3 dir, UnityAction roatate)
    {
        Transform target = transform.Find("Model").GetComponent<Transform>(); 
        float angle = Vector3.Angle(target.forward, dir);
        float rotDir = 1.0f;
        if (Vector3.Dot(target.right, dir) < 0.0f)
        {
            rotDir = -1.0f;
        }
        Debug.Log($"Angle : { angle }");
        while (angle > 0.0f)
        {
            float delta = RotSpeed * Time.deltaTime;
            if (angle - delta < 0.0f)
            {
                delta = angle;
            }
            angle -= delta;
            target.Rotate(Vector3.up * rotDir * delta, Space.World);
            yield return null;
        }
        roatate?.Invoke();
    }
    
    //�ൿ
    void GettingItem()
    {

    }
    void Attack()
    {

    }
    protected virtual void TakeDamage(float dmg) { }
    protected void Guard()
    {
        if (coMove != null)
        {
            StopCoroutine(coMove);
            coMove = null;
        }
        coMove = StartCoroutine(GuardUpCast());
    }
    IEnumerator GuardUpCast()
    {
        yield return new WaitForSeconds(2.0f);
        GetGMInst().ChangeTurn();
    }
}

