using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    [SerializeField]
    public List<ItemSet> items;

    public Inventory myInventory;


    private void Start()
    {
        myInventory = GetComponentInParent<Inventory>();
        for (int i = 0; i < myInventory.slots.Length; i++)
        {
            items.Add(null);
        }
    }
}
