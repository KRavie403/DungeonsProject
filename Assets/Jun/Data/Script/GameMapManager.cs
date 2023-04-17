using System.Collections.Generic;
using UnityEngine;

public class GameMapManager : MonoBehaviour
{
    
    public int rows = 10; //98
    public int columns = 10; //98
    public float scale = 1.0f; //1

    public Material tileMat; //Ground���׸���־�������


    public GameObject[,] tiles;
    public Vector3 LBLocation = new Vector3(0, 0, 0);



    //public Transform[] _MapTiles = null;
    private void Awake()
    {

        GenerateAllTiles(1, columns, rows); //1, 98, 98

    }
    

    void GenerateAllTiles(float tileSize, int tileCount_X, int tileCount_Y) //1, 98, 98
    {
        tiles = new GameObject[tileCount_X, tileCount_Y]; //tiles�迭�� 98,98

        Debug.Log("Generating All Tiles");
        for (int x = 0; x < tileCount_X; x++)
        {
            for (int y = 0; y < tileCount_Y; y++) // y0~97�� 98���ݺ�
            {
                tiles[x, y] = GenerateSingleTile(tileSize, x, y); //������1�� xy 0���� 97������
                //obj.SetActive(false);
            }
        }
    }
    private GameObject GenerateSingleTile(float tileSize, int x, int y)
    {
        //Debug.Log("Generating Si Tiles");

        GameObject tile = new GameObject(string.Format("X : {0}, Y:{1}", x, y)); //x�� 0���� y�� 1���� ��
        tile.transform.parent = gameObject.transform; //tile�� �θ�� GameMapManager
        tile.layer = 3; //tile�� ���̾�� 3���� Ground�κ���
        Mesh mesh = new Mesh();
        tile.AddComponent<MeshFilter>().mesh = mesh; //�Ž����� ������Ʈ �߰�
        tile.AddComponent<MeshRenderer>().material = tileMat; //�Ž������� ������Ʈ �߰�

        Vector3[] vertices = new Vector3[4]; //����4���ǹ迭����
        vertices[0] = new Vector3(x * tileSize, 0.1f, y * tileSize); //??????????
        vertices[1] = new Vector3(x * tileSize, 0.1f, (y+1) * tileSize); //??????????
        vertices[2] = new Vector3((x+1) * tileSize, 0.1f, y * tileSize); //??????????
        vertices[3] = new Vector3((x+1) * tileSize, 0.1f, (y+1) * tileSize); //??????????
        int[] tris = new int[] { 0, 1, 2, 1, 3, 2 }; //012132 ����ִ� �迭����
        mesh.vertices = vertices; //??????????
        mesh.triangles = tris; //??????????

        tile.AddComponent<BoxCollider>(); //�ڽ��ݶ��̴� ������Ʈ �߰�
        tile.GetComponent<BoxCollider>().isTrigger = true; //�ڽ��ݶ��̴� Ʈ���� üũ

        tile.AddComponent<TileState>(); //Ÿ�Ͻ�����Ʈ ��ũ��Ʈ �߰�
        tile.GetComponent<TileState>().isVisited = -1; //��ũ��Ʈ�� isVisited�� -1���߰�
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
