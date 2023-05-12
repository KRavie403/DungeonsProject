using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    //GameMap
    public Dictionary<Vector2Int, TileStatus> tiles;
    public int rows = 10;
    public int columns = 10;
    public float scale = 1.0f;

    public Material tileMat;
    public Vector3 LBLocation = new Vector3(0, 0, 0);

    private void Awake()
    {
        GenerateAllTiles(1, columns, rows);
    }


    //public Transform[] _MapTiles = null;



    void GenerateAllTiles(float tileSize, int tileCount_X, int tileCount_Y)
    {
        tiles = new Dictionary<Vector2Int, TileStatus>();

        Debug.Log("Generating All Tiles");
        for (int x = 0; x < tileCount_X; x++)
        {
            for (int y = 0; y < tileCount_Y; y++)
            {
                tiles.Add(new Vector2Int(x, y), GenerateSingleTile(tileSize, x, y).GetComponent<TileStatus>());

                //obj.SetActive(false);
            }
        }
    }
    private GameObject GenerateSingleTile(float tileSize, int x, int y)
    {
        //Debug.Log("Generating Si Tiles");

        GameObject tile = new GameObject(string.Format("X : {0}, Y:{1}", x, y));
        tile.transform.parent = gameObject.transform;
        tile.layer = 3;
        Mesh mesh = new Mesh();
        tile.AddComponent<MeshFilter>().mesh = mesh;
        tile.AddComponent<MeshRenderer>().material = tileMat;

        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(x * tileSize, 0.1f, y * tileSize);
        vertices[1] = new Vector3(x * tileSize, 0.1f, (y + 1) * tileSize);
        vertices[2] = new Vector3((x + 1) * tileSize, 0.1f, y * tileSize);
        vertices[3] = new Vector3((x + 1) * tileSize, 0.1f, (y + 1) * tileSize);
        int[] tris = new int[] { 0, 1, 2, 1, 3, 2 };
        mesh.vertices = vertices;
        mesh.triangles = tris;

        tile.AddComponent<BoxCollider>();
        tile.GetComponent<BoxCollider>().isTrigger = true;

        tile.AddComponent<TileStatus>();
        tile.GetComponent<TileStatus>().isVisited = -1;
        tile.GetComponent<TileStatus>().pos = new Vector2Int(x, y);
        tile.gameObject.isStatic = true;
        return tile;
    }
    public void Init()
    {
        foreach (TileStatus tile in tiles.Values)
        {
            int step = tile.isVisited;
            if (step > -1)
                tile.isVisited = -1;
            tile.gameObject.layer = 3;
        }
    }
    public void InitLayer()
    {
        foreach (TileStatus tile in tiles.Values)
        {
            tile.gameObject.layer = 3;
        }
    }
    public void InitTarget(Vector2Int tile)
    {

        tiles[tile].isVisited = -1;
        tiles[tile].gameObject.layer = 3;
    }
    void VisitedTile(int X, int Y)
    {
        Vector2Int pos = new Vector2Int(X, Y);
        tiles[pos].isVisited = 0;
    }
    public int CheckTileVisited(int X, int Y)
    {
        Vector2Int pos = new Vector2Int(X, Y);
        return tiles[pos].isVisited;
    }

    public bool CheckIncludedIndex(Vector2Int pos)
    {
        if (pos.x >= columns || pos.x < 0)
            return false;
        if (pos.y >= rows || pos.y < 0)
            return false;

        return true;
    }
    public Vector2Int GetTileIndex(GameObject hitInfo)
    {
        Vector2Int tile = hitInfo.GetComponent<TileStatus>().pos;
        if (tiles.ContainsKey(tile) == hitInfo)
            return tile;
        return -Vector2Int.one;
    }

}
