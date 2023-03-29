using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharactorMoveMent
{
    public enum STATE
    {
        CREATE, IDLE, ATTACK_CAST, GUARD_UP 
    }

    STATE _curState = STATE.CREATE;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeState(STATE state)
    {
        _curState = STATE.IDLE;
    }
    public void OnMove(Transform tile)
    {
        MoveToTile(tile);
    }

}
