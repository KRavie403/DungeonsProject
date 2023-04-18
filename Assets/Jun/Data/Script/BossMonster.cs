using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : BossMovement
{
    // Start is called before the first frame update
    private void Start()
    {
        myType = OB_TYPES.MONSTER;
        GameManager.GM.Players.Add(this.gameObject);
        my_Pos.x = GameManager.GM.rows - 5;
        my_Pos.y = GameManager.GM.columns - 5;
        float half = GameManager.GM.scale * 0.5f;
        transform.position = new Vector3((float)my_Pos.x + half, 0, (float)my_Pos.y + half);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
