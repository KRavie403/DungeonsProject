using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;




public class GameManager : Singleton<GameManager>
{
    public List<GameObject> characters;
    public FollowCamera Main_Cam;
    public int curCharacter =0;
    public GameObject[,] tiles;
    [SerializeField]
    CharacterDB selectedOrgChars;
    [SerializeField]
    GameObject boss;

    private void Awake()
    {
        GenerateAllTiles(1, columns, rows);
        characters = new List<GameObject>();
    }
    
    public void GameStart()
    {
        GameObject obj;
        foreach (var chosen in selectedOrgChars.characterList)
        {
            obj = Instantiate(chosen.Preb);
            obj.GetComponent<Player>().PlayerSetting(chosen);

            characters.Add(obj);
        }

        obj = Instantiate(boss);
        obj.GetComponent<BossMonster>().PlayerSetting();
        characters.Add(obj);

        UI_Manager.Inst.GameStart();
        Main_Cam.enabled = true;
        Main_Cam.SetCam(0);

        if (characters != null)
        {
            characters[0].GetComponent<Player>().ChangeState(STATE.ACTION);
            CurrentSkill();
        }


    }
    void Update()
    {
    }

    //Player
    public void OnMove()
    {
        characters[curCharacter].GetComponent<Player>()?.OnMove();
    }

    public void ChangeTurn()
    {
        characters[curCharacter].GetComponent<CharactorMovement>().ChangeState(STATE.IDLE);
        curCharacter = (++curCharacter) % (characters.Count);
        UI_Manager.Inst.StateUpdate(curCharacter);
        Main_Cam.SetCam(curCharacter);
        characters[curCharacter].GetComponent<CharactorMovement>().ChangeState(STATE.ACTION);

        if(characters[curCharacter].TryGetComponent(out BossMonster boss))
            boss.StartFSM();

        CurrentSkill();


    }
    private void CurrentSkill()
    {
        if (characters[curCharacter].GetComponent<CharactorMovement>().myType == OB_TYPES.PLAYER)
        {
            if (UI_Manager.Inst.currentSkillSet.List != null)
                UI_Manager.Inst.currentSkillSet.List.Clear();
            UI_Manager.Inst.skill_Count = 0;
            foreach (var skill in characters[curCharacter].GetComponent<Player>().skilList)
                UI_Manager.Inst.AddSkills(skill);
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
        tile.gameObject.isStatic = true;
        return tile;
    }
    public void Init()
    {
        foreach (GameObject obj in tiles)
        {
            
            int step = obj.GetComponent<TileState>().isVisited;
            if(step > -1)
                obj.GetComponent<TileState>().isVisited  = -1;
            obj.layer = 3;
        }
    }
    public void InitLayer()
    {
        foreach (GameObject obj in tiles)
        {
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
