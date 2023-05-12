using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Events;

public abstract class CharactorMovement : CharactorProperty
{
    enum Direction { Front, Left, Right, Back }
    
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
        GetMMInst().tiles[Start_X, Start_Y].GetComponent<TileStatus>().reSetTile();
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

    }
    bool CastDirectionTile(int x, int y, int step, Direction dir)
    {
        switch (dir)
        {
            case Direction.Left:
                if (x - 1 > -1 && GetMMInst().tiles[x - 1, y] &&
                    GetMMInst().tiles[x - 1, y].GetComponent<TileStatus>().isVisited == step) return true;
                break;
            case Direction.Right:
                if (x + 1 < GetMMInst().columns && GetMMInst().tiles[x + 1, y] &&
                    GetMMInst().tiles[x + 1, y].GetComponent<TileStatus>().isVisited == step) return true;
                break;
            case Direction.Back:
                if (y - 1 > -1 && GetMMInst().tiles[x, y - 1] &&
                    GetMMInst().tiles[x, y-1].GetComponent<TileStatus>().isVisited == step) return true;
                break;
            case Direction.Front:
                if (y + 1 < GetMMInst().rows && GetMMInst().tiles[x, y+1] &&
                    GetMMInst().tiles[x, y+1].GetComponent<TileStatus>().isVisited == step) return true;
                break;
        }
        return false;
    }
    void SetVisited (int x, int y, int step)
    {
        if(GetMMInst().tiles[x, y])
            GetMMInst().tiles[x, y].GetComponent<TileStatus>().isVisited = step;
    }
    virtual public void SetDistance()
    {
        for (int step = 1; step <= curAP; step++)
        {
            foreach (GameObject obj in GetMMInst().tiles)
            {
                if (obj.GetComponent<TileStatus>().isVisited == step - 1)
                    TestAllDirection(obj.GetComponent<TileStatus>().pos.x, obj.GetComponent<TileStatus>().pos.y, step);
            }
        }

        RefreshArea();

    }

    private void RefreshArea()
    {
        foreach (GameObject obj in GetMMInst().tiles)
        {
            if (obj.GetComponent<TileStatus>().isVisited > -1)
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
        if(GetMMInst().tiles[target.x, target.y] && GetMMInst().tiles[target.x, target.y].GetComponent<TileStatus>().isVisited > 0)
        {
            path.Add(GetMMInst().tiles[x, y]);
            step = GetMMInst().tiles[x, y].GetComponent<TileStatus>().isVisited - 1;
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
                tempList.Add(GetMMInst().tiles[x, y + 1]);
            if (CastDirectionTile(x, y, step, Direction.Back))
                tempList.Add(GetMMInst().tiles[x, y - 1]);
            if (CastDirectionTile(x, y, step, Direction.Left))
                tempList.Add(GetMMInst().tiles[x - 1, y]);
            if (CastDirectionTile(x, y, step, Direction.Right))
                tempList.Add(GetMMInst().tiles[x + 1, y]);

            GameObject tmp = FindClosest(GetMMInst().tiles[target.x, target.y].transform, tempList);
            path.Add(tmp);
            x = tmp.GetComponent<TileStatus>().pos.x;
            y = tmp.GetComponent<TileStatus>().pos.y;
            tempList.Clear();
        }
        findPath = true;
        return;
    }
     protected GameObject FindClosest(Transform targetLoctaion, List<GameObject> list)
     {
        float currentDinstance = GetMMInst().scale * GetMMInst().rows * GetMMInst().columns;
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
    IEnumerator MovingByPath(UnityAction arrive = null)
    {
        //myAnim.SetFloat("Speed", MoveSpeed);
        Vector2Int dest_pos = my_Pos;
        for (int i = path.Count - 2; i >= 0;)
        {
            bool done = false;
            Vector2Int tilePos = new Vector2Int(path[i].GetComponent<TileStatus>().pos.x, path[i].GetComponent<TileStatus>().pos.y);
            MoveToTile(tilePos, () => done = true);
            while (!done)
            {
                yield return null;
            }
            dest_pos = tilePos;

            curAP--;
            UI_Manager.Inst.StateUpdate((int)GetGMInst().curCharacter);
            if (curAP == 10)
                break;
            i--;
        }

        if (curAP == 10)
            GetGMInst().ChangeTurn();
        
        else
            GetComponent<Player>().ChangeState(STATE.ACTION);

        GetMMInst().Init();
        my_Pos = dest_pos;
        GetMMInst().tiles[dest_pos.x, dest_pos.y].GetComponent<TileStatus>().my_obj = OB_TYPES.PLAYER;
        GetMMInst().tiles[dest_pos.x, dest_pos.y].GetComponent<TileStatus>().my_target = this.gameObject;

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

