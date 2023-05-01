using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;
public class BossMonster : Player
{
    // Start is called before the first frame update
    public Slider _bossHPUI;
    private float Hp;
    private void Start()
    {
        
        SetPlayer();
        StartCoroutine(SetPos());
    }
    void SetPlayer()
    {
        myType = OB_TYPES.MONSTER;
        if (skilList == null)
            skilList = new List<SkillSet>();
        GameManager.GM.Players.Add(this.gameObject);
    }
    IEnumerator SetPos()
    {
        //GameManager.GM.Players.Add(this.gameObject);
        
        int x, y;

        do
        {
            x = Random.Range(0, GameManager.GM.columns);
            y = Random.Range(0, GameManager.GM.rows);
        } while (GameManager.GM.tiles[x, y].GetComponent<TileState>().isVisited == -5);


        my_Pos = new Vector2Int(y, x);

        float half = GameManager.GM.scale;
        transform.position = new Vector3((float)my_Pos.x + half, 0, (float)my_Pos.y + half);

        for (int i = 0; i <= 1; i++)
        {
            for (int j = 0; j <= 1; j++)
            {
                GameManager.GM.tiles[my_Pos.x + i, my_Pos.y + j].GetComponent<TileState>().my_obj = myType;
                GameManager.GM.tiles[my_Pos.x + i, my_Pos.y + j].GetComponent<TileState>().isVisited = -2;
                GameManager.GM.tiles[my_Pos.x + i, my_Pos.y + j].GetComponent<TileState>().SettingTarget(this.gameObject);
            }
        }
        GameManager.UM.AddPlayer(my_Sprite);

        yield return null;
    }
    
    

}
