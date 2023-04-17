using System.Collections.Generic;
using UnityEngine;

public class GameMapManager : MonoBehaviour
{
    
    public int rows = 10; //98
    public int columns = 10; //98
    public float scale = 1.0f; //1

    public Material tileMat; //Ground마테리얼넣어져있음


    public GameObject[,] tiles;
    public Vector3 LBLocation = new Vector3(0, 0, 0);



    //public Transform[] _MapTiles = null;
    private void Awake()
    {

        GenerateAllTiles(1, columns, rows); //1, 98, 98

    }
    

    void GenerateAllTiles(float tileSize, int tileCount_X, int tileCount_Y) //1, 98, 98
    {
        tiles = new GameObject[tileCount_X, tileCount_Y]; //tiles배열에 98,98

        Debug.Log("Generating All Tiles");
        for (int x = 0; x < tileCount_X; x++)
        {
            for (int y = 0; y < tileCount_Y; y++) // y0~97을 98번반복
            {
                tiles[x, y] = GenerateSingleTile(tileSize, x, y); //사이즈1과 xy 0부터 97값으로
                //obj.SetActive(false);
            }
        }
    }
    private GameObject GenerateSingleTile(float tileSize, int x, int y)
    {
        //Debug.Log("Generating Si Tiles");

        GameObject tile = new GameObject(string.Format("X : {0}, Y:{1}", x, y)); //x는 0번에 y는 1번에 들어감
        tile.transform.parent = gameObject.transform; //tile의 부모는 GameMapManager
        tile.layer = 3; //tile의 레이어는 3번인 Ground로변경
        Mesh mesh = new Mesh();
        tile.AddComponent<MeshFilter>().mesh = mesh; //매쉬필터 컴포넌트 추가
        tile.AddComponent<MeshRenderer>().material = tileMat; //매쉬렌더러 컴포넌트 추가

        Vector3[] vertices = new Vector3[4]; //백터4개의배열생성
        vertices[0] = new Vector3(x * tileSize, 0.1f, y * tileSize); //??????????
        vertices[1] = new Vector3(x * tileSize, 0.1f, (y+1) * tileSize); //??????????
        vertices[2] = new Vector3((x+1) * tileSize, 0.1f, y * tileSize); //??????????
        vertices[3] = new Vector3((x+1) * tileSize, 0.1f, (y+1) * tileSize); //??????????
        int[] tris = new int[] { 0, 1, 2, 1, 3, 2 }; //012132 들어있는 배열생성
        mesh.vertices = vertices; //??????????
        mesh.triangles = tris; //??????????

        tile.AddComponent<BoxCollider>(); //박스콜라이더 컴포넌트 추가
        tile.GetComponent<BoxCollider>().isTrigger = true; //박스콜라이더 트리거 체크

        tile.AddComponent<TileState>(); //타일스테이트 스크립트 추가
        tile.GetComponent<TileState>().isVisited = -1; //스크립트에 isVisited에 -1값추가
        tile.GetComponent<TileState>().x_pos = x;
        tile.GetComponent<TileState>().y_pos = y;

        return tile;
    }
    public void Init() //??????????//??????????//??????????//??????????//??????????//??????????//??????????//??????????//??????????//??????????
    {
        foreach (GameObject obj in tiles)
        {
            if(obj != null)
                obj.GetComponent<TileState>().isVisited = -1;
        }
    }
    void VisitedTile(int X, int Y)
    {
        tiles[X, Y].GetComponent<TileState>().isVisited = 0;
    }
    public int CheckTileVisited(int X, int Y)
    {
        return tiles[X, Y].GetComponent<TileState>().isVisited;
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
