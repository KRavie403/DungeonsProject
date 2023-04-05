using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public List<GameObject> Players;
    public int rows = 10;
    public int columns = 10;
    public int scale = 1;

    public Material tileMat;
    public GameObject[,] tiles;
    public Vector3 LBLocation = new Vector3(0, 0, 0);

    public int Start_X = 0;
    public int Start_Y = 0;

    //public Transform[] _MapTiles = null;
    private void Awake()
    {

        GenerateAllTiles(1, columns, rows);

        Players = new List<GameObject>();
    }


    void GenerateAllTiles(float tileSize, int tileCount_X, int tileCount_Y)
    {
        tiles = new GameObject[tileCount_X, tileCount_Y];

        Debug.Log("Generating All Tiles");
        for (int x = 0; x < tileCount_X; x++)
        {
            for (int y = 0; y < tileCount_Y; y++)
            {
                tiles[x, y] = GenerateSingleTile(tileSize, x, y);
                //obj.SetActive(false);
            }
        }
    }
    private GameObject GenerateSingleTile(float tileSize, int x, int y)
    {
        Debug.Log("Generating Si Tiles");

        GameObject tile = new GameObject(string.Format("X : {0}, Y:{1}", x, y));
        tile.transform.parent = gameObject.transform;
        tile.layer = 3;
        Mesh mesh = new Mesh();
        tile.AddComponent<MeshFilter>().mesh = mesh;
        tile.AddComponent<MeshRenderer>().material = tileMat;

        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(x * tileSize, 0.1f, y * tileSize);
        vertices[1] = new Vector3(x * tileSize, 0.1f, (y+1) * tileSize);
        vertices[2] = new Vector3((x+1) * tileSize, 0.1f, y * tileSize);
        vertices[3] = new Vector3((x+1) * tileSize, 0.1f, (y+1) * tileSize);
        int[] tris = new int[] { 0, 1, 2, 1, 3, 2 };
        mesh.vertices = vertices;
        mesh.triangles = tris;

        tile.AddComponent<BoxCollider>();

        return tile;
        //obj.SetActive(false);
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

    public Vector2Int GetTileIndex(GameObject hitInfo)
    {
        for(int x=0;x< columns; x++)
        {
            for(int y = 0; y < rows; y ++) {
                if (tiles[x, y] == hitInfo)
                    return new Vector2Int(x, y);
            }
        }
        return -Vector2Int.one;
    }
   

}
