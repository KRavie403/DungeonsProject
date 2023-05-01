using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GameManager : MonoBehaviour
{
    public static UI_Manager UM = null;
    public static GameManager GM = null;
    public List<GameObject> characters;
    public FollowCamera Main_Cam;
    public int curCharacter =0;
    public GameObject[,] tiles;


    private void Awake()
    {
        GM = this;
        UM = GetComponent<UI_Manager>();
        GenerateAllTiles(1, columns, rows);
        characters = new List<GameObject>();
    }
    
    public void GameStart()
    {
        UM.InGameUI.gameObject.SetActive(true);
        UM.start_button.gameObject.SetActive(false);
        Main_Cam.enabled = true;
        Main_Cam.SetCam(0);

        if (characters != null)
        {
            characters[0].GetComponent<Player>().ChangeState(Player.STATE.ACTION);
            CurrentSkill();
        }

    }
    void Update()
    {
    }

    //Player
    public void OnMove()
    {
        characters[curCharacter].GetComponent<CharactorMovement>().OnMove();
    }
    public void ChangeTurn()
    {
        characters[curCharacter].GetComponent<CharactorMovement>().ChangeState(CharactorMovement.STATE.IDLE);
        
        curCharacter = (++curCharacter) % (characters.Count);
        Main_Cam.SetCam(curCharacter);
        characters[curCharacter].GetComponent<CharactorMovement>().ChangeState(CharactorMovement.STATE.ACTION);
        CurrentSkill();


    }
    private void CurrentSkill()
    {
        if (characters[curCharacter].GetComponent<CharactorMovement>().myType == OB_TYPES.PLAYER)
        {
            if (UM.currentSkillSet.SkillList != null)
                UM.currentSkillSet.SkillList.Clear();
            UM.skill_Count = 0;
            foreach (var skill in characters[curCharacter].GetComponent<Player>().skilList)
                UM.AddSkills(skill);
        }
    }
    // Update is called once per frame
    

    //GameMap
    public int rows = 10;
    public int columns = 10;
    public float scale = 1.0f;

    public Material tileMat;


    public Vector3 LBLocation = new Vector3(0, 0, 0);



    //public Transform[] _MapTiles = null;
    


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

        tile.AddComponent<TileState>();
        tile.GetComponent<TileState>().isVisited = -1;
        tile.GetComponent<TileState>().pos = new Vector2Int(x, y);

        return tile;
    }
    public void Init()
    {
        foreach (GameObject obj in tiles)
        {
            obj.GetComponent<TileState>().isVisited = -1;
            obj.layer = 3;
        }
    }
    public void InitTarget(Vector2Int tile)
    {
        tiles[tile.x, tile.y].GetComponent<TileState>().isVisited = -1;
        tiles[tile.x, tile.y].layer = 3;
    }
    void VisitedTile(int X, int Y)
    {
        tiles[X, Y].GetComponent<TileState>().isVisited = 0;
    }
    public int CheckTileVisited(int X, int Y)
    {
        return tiles[X, Y].GetComponent<TileState>().isVisited;
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
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                if (tiles[x, y] == hitInfo)
                    return new Vector2Int(x, y);
            }
        }
        return -Vector2Int.one;
    }

}
