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
        GameMapManger = GameObject.FindGameObjectWithTag("GameMapManager").GetComponent<GameBoard>();
        GameMapManger.Players.Add(this.gameObject);
        pos_x = Random.Range(0, GameMapManger.rows);
        pos_y = Random.Range(0, GameMapManger.columns);
        float half_scale = GameMapManger.scale / 2.0f;
        transform.position = new Vector3((float)pos_x - half_scale, 0, (float)pos_y - half_scale);
        Debug.Log($"{half_scale}, {(float)pos_x - half_scale}");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(STATE state)
    {
        _curState = STATE.IDLE;
    }
    public void OnMove(Vector3 tile)
    {
        InitMoveStart();
        MoveToTile(tile);
    }
    
    void InitMoveStart()
    {
        Start_X = pos_x;
        Start_Y = pos_y;
        GameMapManger.tiles[Start_X, Start_Y].GetComponent<TileState>().isVisited = 0;
    }

}
