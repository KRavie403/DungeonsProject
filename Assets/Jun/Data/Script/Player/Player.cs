using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharactorMovement
{
    public enum STATE
    {
        CREATE, ACTION, MOVE, ATTACK_CAST, GUARD_UP, IDLE
    }
    STATE _curState = STATE.CREATE;


    // Start is called before the first frame update
    void Start()
    {

        GM.Players.Add(this.gameObject);
        pos_x = Random.Range(0, GMMap.rows);
        pos_y = Random.Range(0, GMMap.columns);

        float half = GMMap.scale * 0.5f;
        transform.position = new Vector3((float)pos_x + half, 0, (float)pos_y + half);

    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }

    void StateProcess()
    {
        switch (_curState)
        {
            case STATE.CREATE:
                break;

            case STATE.IDLE:

                break;

            case STATE.ACTION:
                //추후 UI 조작과 연결

                //임시
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Guard();
                    ChangeState(STATE.GUARD_UP);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    InitMoveStart();
                    SetDistance();
                    ChangeState(STATE.MOVE);
                }


                break;

        }
    }

    public void ChangeState(STATE s)
    {
        if (_curState == s) return;
        _curState = s;
        switch (_curState)
        {
            case STATE.CREATE:
                break;

            case STATE.IDLE:
                gameObject.GetComponent<Picking>().enabled = false;

                break;

            case STATE.ACTION:


                break;
            case STATE.MOVE:
                gameObject.GetComponent<Picking>().enabled = true;

                break;

        }
    }
    public STATE GetState()
    {
        return _curState;
    }

    public void OnMove(Vector2Int tile)
    {
        InitMoveStart();
        MoveToTile(tile);
    }
    public void OnMoveByPath(Vector2Int tile)
    {
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
        GMMap.tiles[Start_X, Start_Y].GetComponent<TileState>().isVisited = 0;
    }

}
