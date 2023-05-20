using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataBase", menuName = "ScriptableObjects/ItemDB", order = 6)]

public class ItemSetDB : ScriptableObject
{
    [SerializeField]
    public List<ItemSet> ItemList = new List<ItemSet>();
}
