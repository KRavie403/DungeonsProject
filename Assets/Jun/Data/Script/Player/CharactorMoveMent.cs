using System.Collections;
using System.Collections.Generic;
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
                if (x - 1 > -1 && GMMap.tiles[x - 1, y] &&
                    GMMap.tiles[x - 1, y].GetComponent<TileState>().isVisited == step) return true;
                break;
            case Direction.Right:
                if (x + 1 < GMMap.columns && GMMap.tiles[x + 1, y] &&
                    GMMap.tiles[x + 1, y].GetComponent<TileState>().isVisited == step) return true;
                break;
            case Direction.Back:
                if (y - 1 > -1 && GMMap.tiles[x, y - 1] &&
                    GMMap.tiles[x, y-1].GetComponent<TileState>().isVisited == step) return true;
                break;
            case Direction.Front:
                if (y + 1 < GMMap.rows && GMMap.tiles[x, y+1] &&
                    GMMap.tiles[x, y+1].GetComponent<TileState>().isVisited == step) return true;
                break;
        }
        return false;
    }
    void SetVisited (int x, int y, int step)
    {
        if(GMMap.tiles[x, y])
            GMMap.tiles[x, y].GetComponent<TileState>().isVisited = step;
    }
    protected void SetDistance()
    {
        for (int step = 1; step <= ActionPoint; step++)
        {
            foreach (GameObject obj in GMMap.tiles)
            {
                if (obj != null && obj.GetComponent<TileState>().isVisited == step - 1)
                {
                    TestAllDirection(obj.GetComponent<TileState>().x_pos, obj.GetComponent<TileState>().y_pos, step);
                    obj.layer = 9;
                }
            }
        }
        foreach (GameObject obj in GMMap.tiles)
        {
            if (obj != null && obj.GetComponent<TileState>().isVisited == -1)
            {
                obj.layer = 3;
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
        if(GMMap.tiles[target.x, target.y] && GMMap.tiles[target.x, target.y].GetComponent<TileState>().isVisited > 0)
        {
            path.Add(GMMap.tiles[x, y]);
            step = GMMap.tiles[x, y].GetComponent<TileState>().isVisited - 1;
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
                tempList.Add(GMMap.tiles[x, y + 1]);
            if (CastDirectionTile(x, y, step, Direction.Back))
                tempList.Add(GMMap.tiles[x, y - 1]);
            if (CastDirectionTile(x, y, step, Direction.Left))
                tempList.Add(GMMap.tiles[x - 1, y]);
            if (CastDirectionTile(x, y, step, Direction.Right))
                tempList.Add(GMMap.tiles[x + 1, y]);

            GameObject tmp = FindClosest(GMMap.tiles[target.x, target.y].transform, tempList);
            path.Add(tmp);
            x = tmp.GetComponent<TileState>().x_pos;
            y = tmp.GetComponent<TileState>().y_pos;
            tempList.Clear();
        }
        findPath = true;
        return;
    }
    GameObject FindClosest(Transform targetLoctaion, List<GameObject> list)
    {
        float currentDinstance = GMMap.scale * GMMap.rows * GMMap.columns;
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
        Vector3 dir = new Vector3(target.x + GMMap.scale / 2.0f, transform.position.y, target.y + GMMap.scale / 2.0f) - transform.position;
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
        for (int i = path.Count-1; i >= 0;)
        {
            bool done = false;
            Vector2Int tilePos = new Vector2Int(path[i].GetComponent<TileState>().x_pos, path[i].GetComponent<TileState>().y_pos);
            MoveToTile(tilePos, () => done = true);
            while (!done)
            {
                yield return null;
            }
            i--;
        }

        GM.ChangeTurn();
        GMMap.Init();
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
        GM.ChangeTurn();
    }

}

