using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public List<GameObject> Players;
    public int rows = 10;
    public int columns = 10;
    public int scale = 1;
    public GameObject tilePrefab;
    public GameObject[,] tiles;
    public Vector3 LBLocation = new Vector3(0, 0, 0);

    public int Start_X = 0;
    public int Start_Y = 0;

    //public Transform[] _MapTiles = null;
    private void Awake()
    {
        tiles = new GameObject[columns, rows];

        if (tilePrefab)
            GenerateTiles();

        Players = new List<GameObject>();
    }


    void GenerateTiles()
    {
        for(int i = 0; i < columns; i++)
        {
            for(int j=0;j<rows; j++)
            {
                GameObject obj = Instantiate(tilePrefab, new Vector3 (LBLocation.x + scale*i, LBLocation.y, LBLocation.z + scale *j), Quaternion.identity);
                obj.transform.SetParent(gameObject.transform);
                obj.GetComponent<TileProperty>().x_pos = i;
                obj.GetComponent<TileProperty>().y_pos = j;
                tiles[i, j] = obj;
                //obj.SetActive(false);
            }
        }
    }
    void Init()
    {
        foreach (GameObject obj in tiles)
            obj.GetComponent<TileProperty>().isVisited = -1;

    }

    void VisitedTile(int X, int Y)
    {
        tiles[X, Y].GetComponent<TileProperty>().isVisited = 0;
    }

    void Show()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                tiles[i, j].SetActive(true);
            }
        }
    }
}
