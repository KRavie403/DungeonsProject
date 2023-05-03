using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Events;

public abstract class CharactorMovement : CharactorProperty
{
    enum Direction { Front, Left, Right, Back }
    public enum STATE
    {
        CREATE, ACTION, MOVE, ATTACK, SKILL_CAST, GUARD_UP, IDLE, SEARCH
    }
    public STATE _bfState;
    public STATE _curState = STATE.CREATE;

    public bool findPath = false;

    protected int Start_X, Start_Y;
    public List<GameObject> path = new List<GameObject>();

    Coroutine coMove = null;

    virtual public void ChangeState(STATE s) { }
    public STATE GetState()
    {
        return _curState;
    }
    virtual public void OnMove() { }
    protected void InitTileDistance()
    {
        Start_X = my_Pos.x;
        Start_Y = my_Pos.y;
        GetGMInst().tiles[Start_X, Start_Y].GetComponent<TileState>().reSetTile();
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
    protected void MoveByPath(Vector2Int tile, UnityAction done = null)
    {
        StopAllCoroutines();


        SetPath(tile);
        if (findPath)
            StartCoroutine(MovingByPath(done));
        
        my_Pos = tile;

    }
    bool CastDirectionTile(int x, int y, int step, Direction dir)
    {
        switch (dir)
        {
            case Direction.Left:
                if (x - 1 > -1 && GetGMInst().tiles[x - 1, y] &&
                    GetGMInst().tiles[x - 1, y].GetComponent<TileState>().isVisited == step) return true;
                break;
            case Direction.Right:
                if (x + 1 < GetGMInst().columns && GetGMInst().tiles[x + 1, y] &&
                    GetGMInst().tiles[x + 1, y].GetComponent<TileState>().isVisited == step) return true;
                break;
            case Direction.Back:
                if (y - 1 > -1 && GetGMInst().tiles[x, y - 1] &&
                    GetGMInst().tiles[x, y-1].GetComponent<TileState>().isVisited == step) return true;
                break;
            case Direction.Front:
                if (y + 1 < GetGMInst().rows && GetGMInst().tiles[x, y+1] &&
                    GetGMInst().tiles[x, y+1].GetComponent<TileState>().isVisited == step) return true;
                break;
        }
        return false;
    }
    void SetVisited (int x, int y, int step)
    {
        if(GetGMInst().tiles[x, y])
            GetGMInst().tiles[x, y].GetComponent<TileState>().isVisited = step;
    }
    virtual public void SetDistance()
    {
        List<GameObject> searchTileArea = new List<GameObject>();

        for (int i = my_Pos.x - curAP; i <= my_Pos.x + curAP; i++)
        {
            for (int j = my_Pos.y - curAP; i <= my_Pos.y + curAP; j++)
            {
                Vector2Int pos = new Vector2Int(i, j);
                if (!GetGMInst().CheckIncludedIndex(pos))
                    break;
                searchTileArea.Add(GetGMInst().tiles[i, j]);
            }
        }
        Debug.Log(searchTileArea.Count);
        for (int step = 1; step <= curAP; step++)
        {
            foreach (GameObject obj in searchTileArea)
            {
                if (obj.GetComponent<TileState>().isVisited == step - 1)
                    TestAllDirection(obj.GetComponent<TileState>().pos.x, obj.GetComponent<TileState>().pos.y, step);
            }
        }

        Refresh(searchTileArea);

    }

    private static void Refresh(List<GameObject> searchTileArea)
    {
        foreach (GameObject obj in searchTileArea)
        {
            if (obj.GetComponent<TileState>().isVisited > -1)
                obj.layer = 9;
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
    void SetPath(Vector2Int target)
    {
        int step;
        int x = target.x;
        int y = target.y;
        List<GameObject> tempList = new List<GameObject>();
        path.Clear();
        if(GetGMInst().tiles[target.x, target.y] && GetGMInst().tiles[target.x, target.y].GetComponent<TileState>().isVisited > 0)
        {
            path.Add(GetGMInst().tiles[x, y]);
            step = GetGMInst().tiles[x, y].GetComponent<TileState>().isVisited - 1;
        }
        else
        {
            Debug.Log("Nope");
            findPath = false;
            return;
        }
        for(int i = step; step > -1; step--)
        {
            if (CastDirectionTile(x, y, step, Direction.Front))
                tempList.Add(GetGMInst().tiles[x, y + 1]);
            if (CastDirectionTile(x, y, step, Direction.Back))
                tempList.Add(GetGMInst().tiles[x, y - 1]);
            if (CastDirectionTile(x, y, step, Direction.Left))
                tempList.Add(GetGMInst().tiles[x - 1, y]);
            if (CastDirectionTile(x, y, step, Direction.Right))
                tempList.Add(GetGMInst().tiles[x + 1, y]);

            GameObject tmp = FindClosest(GetGMInst().tiles[target.x, target.y].transform, tempList);
            path.Add(tmp);
            x = tmp.GetComponent<TileState>().pos.x;
            y = tmp.GetComponent<TileState>().pos.y;
            tempList.Clear();
        }
        findPath = true;
        return;
    }
     GameObject FindClosest(Transform targetLoctaion, List<GameObject> list)
     {
        float currentDinstance = GetGMInst().scale * GetGMInst().rows * GetGMInst().columns;
        int indexNum = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (Vector3.Distance(targetLoctaion.position, list[i].transform.position) < currentDinstance)
            {
                currentDinstance = Vector3.Distance(targetLoctaion.position, list[i].transform.position);
                indexNum = i;
            }
        }
        return list[indexNum];
    }
    IEnumerator MovingToTile(Vector2Int target, UnityAction done)
    {
        Vector3 dir = new Vector3((target.x + GetGMInst().scale / 2.0f) * _mySize, transform.position.y, (target.y + GameManager.Inst.scale / 2.0f) * _mySize) - transform.position;
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
    IEnumerator MovingByPath(UnityAction arrive = null)
    {
        //myAnim.SetFloat("Speed", MoveSpeed);
        Vector2Int dest_pos = Vector2Int.zero;
        for (int i = path.Count - 2; i >= 0;)
        {
            bool done = false;
            Vector2Int tilePos = new Vector2Int(path[i].GetComponent<TileState>().pos.x, path[i].GetComponent<TileState>().pos.y);
            MoveToTile(tilePos, () => done = true);
            while (!done)
            {
                yield return null;
            }
            dest_pos = tilePos;

            curAP--;
            if (curAP <= 0)
            {
                GetGMInst().ChangeTurn();
                curAP = 10;
                GetComponent<Player>().ChangeState(STATE.IDLE);
                break;
            }
            UI_Manager.Inst.StateUpdate((int)GetGMInst().curCharacter);
            i--;
        }
        GetComponent<Player>().ChangeState(STATE.ACTION);

        GetGMInst().Init();
        GetGMInst().tiles[dest_pos.x, dest_pos.y].GetComponent<TileState>().my_obj = OB_TYPES.PLAYER;
        GetGMInst().tiles[dest_pos.x, dest_pos.y].GetComponent<TileState>().my_target = this.gameObject;

        //myAnim.SetFloat("Speed", 0);
        arrive?.Invoke();

    }

    protected static GameManager GetGMInst()
    {
        return GameManager.Inst;
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
        while (angle > 0.0f)
        {
            float delta = RotSpeed * Time.deltaTime;
            if (angle - delta < 0.0f)
            {
                delta = angle;
            }
            angle -= delta;
            target.Rotate(Vector3.up * rotDir * delta);
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
    public void TakeDamage(float dmg)
    {
        //
        curHP -= dmg;
        Debug.Log($"Get Damage, Current HP : {curHP}");
    }
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
        //�ִϸ��̼� ����
        yield return new WaitForSeconds(2.0f);
        GetGMInst().ChangeTurn();
    }

}

