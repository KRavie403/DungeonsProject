using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OB_TYPES { PLAYER, MONSTER, TELEPORT, CHEST, NONE }

public class TileStatus : MonoBehaviour
{
    public int G, H;
    public int F { get { return G + H; } }

    public bool is_blocked = false;

    public TileStatus prevTile;

    public int isVisited;
    public Vector2Int pos;
    public OB_TYPES my_obj = OB_TYPES.NONE;
    public GameObject my_target = null;

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"Enter : {other.gameObject.layer}");
        if (other.gameObject.layer == LayerMask.NameToLayer("Structures"))
        {
            is_blocked = true;
            isVisited = -100;
        }

        else if (other.gameObject.layer == LayerMask.NameToLayer("Teleport"))
        {
            my_obj = OB_TYPES.TELEPORT;
            my_target = other.gameObject;
            isVisited = -3;
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Chest"))
        {
            my_obj = OB_TYPES.CHEST;
            my_target = other.gameObject;
            isVisited = -4;
        }
    }


    public void SetTarget(GameObject target)
    {
        my_target = target;
    }
    public GameObject OnMyTarget()
    {
        return my_target;
    }
    public void reSetTile()
    {
        my_obj = OB_TYPES.NONE;
        my_target = null;
        isVisited = 0;
        gameObject.layer = 3;
    }
}