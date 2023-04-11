using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> Players;
    public FollowCamera Main_Cam;
    public int currentPlayer =0;

    // Start is called before the first frame update
    void Awake()
    {
        Players = new List<GameObject>();
    }

    private void Start()
    {
        if (Players != null)
        {
            Players[currentPlayer].GetComponent<Player>().ChangeState(Player.STATE.ACTION);
        }
    }
    public void ChangeTurn()
    {
        Players[currentPlayer].GetComponent<Player>().ChangeState(Player.STATE.IDLE);
        currentPlayer = (++currentPlayer) % (Players.Count);
        Main_Cam.myTarget = Players[currentPlayer].transform.Find("ViewPoint").transform;
        Players[currentPlayer].GetComponent<Player>().ChangeState(Player.STATE.ACTION);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
