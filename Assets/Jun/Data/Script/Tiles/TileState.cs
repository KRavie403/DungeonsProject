using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OB_TYPES { PLAYER, MONSTER, TELEPORT, NONE }

public class TileState : MonoBehaviour
{
    public int isVisited;
    public Vector2Int pos;
    public OB_TYPES my_obj = OB_TYPES.NONE;
    public GameObject my_target = null;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (other.gameObject.layer == LayerMask.NameToLayer("Structures"))
        {
            isVisited = -5;
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            my_obj = OB_TYPES.MONSTER;
            my_target = other.gameObject;
            isVisited = -2;
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Teleport"))
        {
            my_obj = OB_TYPES.TELEPORT;
            my_target = other.gameObject;
            isVisited = -3;
        }
    }

}