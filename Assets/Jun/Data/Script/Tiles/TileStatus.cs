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
    public Vector2Int gridPos;
    public OB_TYPES my_obj = OB_TYPES.NONE;
    public GameObject my_target = null;

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