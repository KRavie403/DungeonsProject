using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Events;

public class CharactorMovement : CharactorProperty
{
    enum Direction { Front, Left, Right, Back}
    public bool findPath = false;

    protected int Start_X, Start_Y;
    public List<GameObject> path = new List<GameObject>();
    
    Coroutine coMove = null;

    //이동
    protected void MoveToTile(Vector2Int target, UnityAction done = null)
    {
        if (coMove != null)
        {
            StopCoroutine(coMove);
            coMove = null;
        }
        coMove = StartCoroutine(MovingToTile(target, done));
    }
    protected void MoveByPath(Vector2Int tile)
    {
        StopAllCoroutines();


        SetPath(tile);
        if (findPath)
            StartCoroutine(MovingByPath());
    }
    bool CastDirectionTile(int x, int y, int step, Direction dir)
    {
        switch (dir)
        {
            case Direction.Left:
                if (x - 1 > -1 && GameManager.GM.tiles[x - 1, y] &&
                    GameManager.GM.tiles[x - 1, y].GetComponent<TileState>().isVisited == step) return true;
                break;
            case Direction.Right:
                if (x + 1 < GameManager.GM.columns && GameManager.GM.tiles[x + 1, y] &&
                    GameManager.GM.tiles[x + 1, y].GetComponent<TileState>().isVisited == step) return true;
                break;
            case Direction.Back:
                if (y - 1 > -1 && GameManager.GM.tiles[x, y - 1] &&
                    GameManager.GM.tiles[x, y-1].GetComponent<TileState>().isVisited == step) return true;
                break;
            case Direction.Front:
                if (y + 1 < GameManager.GM.rows && GameManager.GM.tiles[x, y+1] &&
                    GameManager.GM.tiles[x, y+1].GetComponent<TileState>().isVisited == step) return true;
                break;
        }
        return false;
    }
    void SetVisited (int x, int y, int step)
    {
        if(GameManager.GM.tiles[x, y])
            GameManager.GM.tiles[x, y].GetComponent<TileState>().isVisited = step;
    }
    protected void SetDistance()
    {
        for (int step = 1; step <= curActionPoint; step++)
        {
            foreach (GameObject obj in GameManager.GM.tiles)
            {
                if (obj.GetComponent<TileState>().isVisited == step - 1)
                {
                    TestAllDirection(obj.GetComponent<TileState>().pos.x, obj.GetComponent<TileState>().pos.y, step);
                    obj.layer = 9;
                }
                if (obj.GetComponent<TileState>().isVisited == step )
                    obj.layer = 9;
            }
        }
        
    }
    void TestAllDirection(int x, int y, int step)
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
        if(GameManager.GM.tiles[target.x, target.y] && GameManager.GM.tiles[target.x, target.y].GetComponent<TileState>().isVisited > 0)
        {
            path.Add(GameManager.GM.tiles[x, y]);
            step = GameManager.GM.tiles[x, y].GetComponent<TileState>().isVisited - 1;
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
                tempList.Add(GameManager.GM.tiles[x, y + 1]);
            if (CastDirectionTile(x, y, step, Direction.Back))
                tempList.Add(GameManager.GM.tiles[x, y - 1]);
            if (CastDirectionTile(x, y, step, Direction.Left))
                tempList.Add(GameManager.GM.tiles[x - 1, y]);
            if (CastDirectionTile(x, y, step, Direction.Right))
                tempList.Add(GameManager.GM.tiles[x + 1, y]);

            GameObject tmp = FindClosest(GameManager.GM.tiles[target.x, target.y].transform, tempList);
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
        float currentDinstance = GameManager.GM.scale * GameManager.GM.rows * GameManager.GM.columns;
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
        Vector3 dir = new Vector3((target.x + GameManager.GM.scale / 2.0f) * _mySize, transform.position.y, (target.y + GameManager.GM.scale / 2.0f) * _mySize) - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        StartCoroutine(Rotating(dir));

        //myAnim.SetBool("isMoving", true);

        while (dist > 0.0f)
        {
            //if (!myAnim.GetBool("isAttacking"))
            //{
                float delta = MoveSpeed * Time.deltaTime;
                if (dist - delta < 0.0f)
                {
                    delta = dist;
                }
                dist -= delta;
                transform.Translate(dir * delta, Space.World);
            //}
            yield return null;
        }

        //myAnim.SetBool("isMoving", false);
        done?.Invoke();

    }
    IEnumerator MovingByPath()
    {
        //myAnim.SetFloat("Speed", MoveSpeed);
        for (int i = path.Count - 1; i >= 0;)
        {
            bool done = false;
            Vector2Int tilePos = new Vector2Int(path[i].GetComponent<TileState>().pos.x, path[i].GetComponent<TileState>().pos.y);
            MoveToTile(tilePos, () => done = true);
            while (!done)
            {
                yield return null;
            }
            curActionPoint--;
            i--;
        }



        if (curActionPoint <= 0)
        {
            GameManager.GM.ChangeTurn();
            curActionPoint = ActionPoint;
        }
        else
        {
            this.GetComponent<Player>().ChangeState(Player.STATE.ACTION);
        }
    
        GameManager.GM.Init();
        GameManager.GM.tiles[path[0].GetComponent<TileState>().pos.x, path[0].GetComponent<TileState>().pos.y].GetComponent<TileState>().my_obj = OB_TYPES.PLAYER;
        GameManager.GM.tiles[path[0].GetComponent<TileState>().pos.x, path[0].GetComponent<TileState>().pos.y].GetComponent<TileState>().my_target = this.gameObject;

        //myAnim.SetFloat("Speed", 0);
    }
    IEnumerator Rotating(Vector3 dir)
    {
        float angle = Vector3.Angle(transform.forward, dir);
        float rotDir = 1.0f;
        if (Vector3.Dot(transform.right, dir) < 0.0f)
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
            transform.Rotate(Vector3.up * rotDir * delta);
            yield return null;
        }
    }

    //행동
    void GettingItem()
    {

    }
    void Attack()
    {

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
        //애니메이션 실행
        yield return new WaitForSeconds(2.0f);
        GameManager.GM.ChangeTurn();
    }

}

