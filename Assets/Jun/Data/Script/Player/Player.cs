using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharactorMovement
{
    public enum STATE
    {
        CREATE, IDLE, ATTACK_CAST, GUARD_UP 
    }

    STATE _curState = STATE.CREATE;


    // Start is called before the first frame update
    void Start()
    {
        GameMapManger = GameObject.FindGameObjectWithTag("GameMapManager").GetComponent<GameMapManager>();
        GameManager = GameObject.FindGameObjectWithTag("GameMapManager").GetComponent<GameManager>();
        GameManager.Players.Add(this.gameObject);
        pos_x = Random.Range(0, GameMapManger.rows);
        pos_y = Random.Range(0, GameMapManger.columns);

        float half = GameMapManger.scale * 0.5f;
        transform.position = new Vector3((float)pos_x + half, 0, (float)pos_y + half);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(STATE state)
    {
        _curState = STATE.IDLE;
    }
    public void OnMove(Vector2Int tile)
    {
        InitMoveStart();
        MoveToTile(tile);
    }
    public void OnMoveByPath(Vector2Int tile)
    {
        InitMoveStart();
        Debug.Log($"Pos : {pos_x},{pos_y}");
        Debug.Log($"Target : {tile}");
        Debug.Log($"Start : {Start_X},{Start_Y}");
        pos_x = tile.x;
        pos_y = tile.y;

        MoveByPath(tile);
    }

    void InitMoveStart()
    {
        Start_X = pos_x;
        Start_Y = pos_y;
        GameMapManger.tiles[Start_X, Start_Y].GetComponent<TileState>().isVisited = 0;
    }

}
