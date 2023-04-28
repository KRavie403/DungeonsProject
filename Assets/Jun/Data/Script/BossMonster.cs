using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : BossMovement
{
    // Start is called before the first frame update
    private void Start()
    {
        myType = OB_TYPES.MONSTER;
        //GameManager.GM.Players.Add(this.gameObject);
        my_Pos.x = GameManager.GM.rows - 5;
        my_Pos.y = GameManager.GM.columns - 5;
        float half = GameManager.GM.scale * 0.5f;
        transform.position = new Vector3((float)my_Pos.x + half, 0, (float)my_Pos.y + half);

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                GameManager.GM.tiles[my_Pos.x + i, my_Pos.y + j].GetComponent<TileState>().my_obj = myType;
                GameManager.GM.tiles[my_Pos.x + i, my_Pos.y + j].GetComponent<TileState>().isVisited = -2;
                GameManager.GM.tiles[my_Pos.x + i, my_Pos.y + j].GetComponent<TileState>().SettingTarget(this.gameObject);
            }
        }

    }


    public void OnDamage(float dmg)
    {
        curHP -= dmg;
        Debug.Log($"Get Damage, Current HP : {curHP}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
