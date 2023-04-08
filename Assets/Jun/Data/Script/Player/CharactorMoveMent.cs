using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorMovement : CharactorProperty
{
    enum Direction { Front, Left, Right, Back}
    public bool findPath = false;

    protected int Start_X, Start_Y;
    int End_X, End_Y;
    public List<GameObject> path = new List<GameObject>();

    protected void MoveToTile(Vector2Int target)
    {
        StopAllCoroutines();
        StartCoroutine(MovingToTile(target));
    }
    
    void GettingItem()
    {

    }

    void Attack()
    {

    }

    void Guard()
    {

    }

    bool CastDirectionTile(int x, int y, int step, Direction dir)
    {
        switch (dir)
        {
            case Direction.Left:
                if (x - 1 > -1 && GameMapManger.tiles[x - 1, y] &&
                    GameMapManger.tiles[x - 1, y].GetComponent<TileState>().isVisited == step) return true;
                break;
            case Direction.Right:
                if (x + 1 < GameMapManger.columns && GameMapManger.tiles[x + 1, y] &&
                    GameMapManger.tiles[x + 1, y].GetComponent<TileState>().isVisited == step) return true;
                break;
            case Direction.Back:
                if (y - 1 > -1 && GameMapManger.tiles[x, y - 1] &&
                    GameMapManger.tiles[x, y-1].GetComponent<TileState>().isVisited == step) return true;
                break;
            case Direction.Front:
                if (y + 1 < GameMapManger.rows && GameMapManger.tiles[x, y+1] &&
                    GameMapManger.tiles[x, y+1].GetComponent<TileState>().isVisited == step) return true;
                break;
        }
        return false;
    }
    void SetVisited (int x, int y, int step)
    {
        if(GameMapManger.tiles[x, y])
            GameMapManger.tiles[x, y].GetComponent<TileState>().isVisited = step;
    }

    void SetDistance()
    {
        for(int step = 1; step < ActionPoint; step++)
        {
            foreach(GameObject obj in GameMapManger.tiles)
            {
                if (obj.GetComponent<TileState>().isVisited == step - 1)
                    TestAllDirection(obj.GetComponent<TileState>().x_pos, obj.GetComponent<TileState>().y_pos, step);
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
    void SetPath()
    {
        int step;
        int x = End_X;
        int y = End_Y;
        List<GameObject> tempList = new List<GameObject>();
        path.Clear();
        if(GameMapManger.tiles[End_X, End_Y] && GameMapManger.tiles[End_X, End_Y].GetComponent<TileState>().isVisited > 0)
        {
            path.Add(GameMapManger.tiles[x, y]);
            step = GameMapManger.tiles[x, y].GetComponent<TileState>().isVisited - 1;
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
                tempList.Add(GameMapManger.tiles[x, y + 1]);
            if (CastDirectionTile(x, y, step, Direction.Back))
                tempList.Add(GameMapManger.tiles[x, y - 1]);
            if (CastDirectionTile(x, y, step, Direction.Left))
                tempList.Add(GameMapManger.tiles[x - 1, y]);
            if (CastDirectionTile(x, y, step, Direction.Right))
                tempList.Add(GameMapManger.tiles[x + 1, y]);

            GameObject tmp = FindClosest(GameMapManger.tiles[End_X, End_Y].transform, tempList);
            path.Add(tmp);
            x = tmp.GetComponent<TileState>().x_pos;
            y = tmp.GetComponent<TileState>().y_pos;
            tempList.Clear();
        }
    }



    IEnumerator MovingToTile(Vector2Int target)
    {
        findPath = true;
        End_X = target.x;
        End_Y = target.y;

        while (findPath)
        {
            SetDistance();
            //SetPath();
            yield return null;
        }


    }
    GameObject FindClosest(Transform targetLoctaion, List<GameObject> list)
    {
        float currentDinstance = GameMapManger.scale * GameMapManger.rows * GameMapManger.columns;
        int indexNum = 0;
        for(int i = 0; i < list.Count; i++)
        {
            if(Vector3.Distance(targetLoctaion.position, list[i].transform.position) < currentDinstance)
            {
                currentDinstance = Vector3.Distance(targetLoctaion.position, list[i].transform.position);
                indexNum = i;
            }
        }
        return list[indexNum];
    }
}
