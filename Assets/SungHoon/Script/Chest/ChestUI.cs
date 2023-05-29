using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChestUI : MonoBehaviour
{
    [SerializeField]
    public List<ItemSet> items;

    [SerializeField]
    private Transform ChestSlotParent;
    [SerializeField]
    public List<ChestUISlot> slots;

    [SerializeField]
    public Inventory myInventory;

    public int[] Index;

    ChestUISlot[] slotsIndex;
    [field:SerializeField]

    public ItemSetDB myItemDB;


  

    // Start is called before the first frame update
    void Start()
    {  
        FreshSlot();
        slotsIndex = ChestSlotParent.GetComponentsInChildren<ChestUISlot>();
        for (int i = 0; i < slotsIndex.Length; i++)
        {
            slots.Add(slotsIndex[i]);
        }
        Additem();  
        for (int i = 0; i < items.Count; i++)
        {
            slots[i].image.sprite = items[i].MySprite;
        }

    }
  
    public void Additem()
    {
        int randItem;
        for (int i=0; i < slots.Count;i++)
        {
            randItem = UnityEngine.Random.Range(0,myItemDB.ItemList.Count);
            items.Add(myItemDB.ItemList[randItem]);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void FreshSlot()
    {
        int i = 0;
        for (; i < items.Count && i < slots.Count; i++)
        {
            slots[i].item = items[i];
        }
        for (; i < slots.Count; i++)
        {
            slots[i].item = null;
        }
    }
    public void DestroyItem(int index)
    {
        //items.RemoveAt(index);
        items[index] = null;
    }
   
}
