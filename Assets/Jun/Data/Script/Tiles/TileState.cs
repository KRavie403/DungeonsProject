using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileState : MonoBehaviour //실행시 나오는 칸블럭들에게 넣어져있음
{
    public int isVisited; //??????????
    public int x_pos; //??????????
    public int y_pos; //??????????


    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (other.gameObject.layer == LayerMask.NameToLayer("Structures")) //오브젝트와 닿으면
        {
            Destroy(this.gameObject); //파괴
        }
    }

}