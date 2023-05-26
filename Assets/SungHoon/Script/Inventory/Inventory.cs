using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    public List<ItemSet> items;
    [SerializeField]
    private Transform[] slotParent;
    [SerializeField]
    public Slot[] slots;
    [SerializeField]
    public GameObject myObj;

    [SerializeField]
    public Player myPlayer;

    [SerializeField]
    public Player orgPlayer;

    public ItemSetDB myItemDB;
    
    public Equipment myEquipment;

    public List<Slot> slotsIndex;

    private GameManager GetGmInst()
    {
        return GameManager.Inst;
    }
    private void OnValidate()
    {
        slots = slotParent[0].GetComponentsInChildren<Slot>();
    }

    void Awake()
    {
        FreshSlot();
        for (int i = 0; i < slots.Length; i++)
        {
            items.Add(null);
        }
        for (int i = 0; i < slots.Length; i++)
        {
            slotsIndex.Add(slots[i]);
        }
        orgPlayer = null;
    }
    int i = 7;
    private void Update()
    {
        if (GetGmInst().characters.Count != 0)
        {
            //if(myPlayer.Item_stat.Count. GetGmInst().characters.Count)
            StatModifire myEquipmentStat = new StatModifire();
            myPlayer = GetGmInst().characters[GetGmInst().curCharacter].GetComponent<Player>();
            if (myPlayer != orgPlayer)
            {
                i++;
                if ((orgPlayer != null))
                {
                    i = 0;
                }
                slots = slotParent[i].GetComponentsInChildren<Slot>();
            }
            orgPlayer = myPlayer;
            if (myPlayer.Item_stat.Count == 0)
            {
                myPlayer.Item_stat.Add(myEquipmentStat);
            }
            myPlayer.Item_stat[0].AttackPower += 10;
            Debug.Log($"{myPlayer.Item_stat.Count}");
            Debug.Log($"{myPlayer.Item_stat[0].AttackPower}");
        }
       
    }
    public void FreshSlot()
    {
        int i = 0;
        for (; i < items.Count && i < slots.Length; i++)
        {
            slots[i].item = items[i];
        }
        for (; i < slots.Length; i++)
        {
            slots[i].item = null;
        }
    }   
    int count = 0;
    public void AddItem(ItemSet _item,ItemSet.ItemGrade itemGrade)
    {
        count++;
        for (int i = 0; i < slots.Length;)
        {
            if (items[i] == null)
            {
                items[i] = _item;
                slots[i].myItemGrade = itemGrade;
                slots[i].item = items[i];
                break;
            }
            else
            {
                i++;
            }
        }
        //if (items.Count < slots.Length)
        //{
        //    items.Add(_item);
        //    Debug.Log($"{count}¹ø ÀÎµ¦½º Ãß°¡");
        //    FreshSlot();
        //}
        //else
        //{
        //    print("½½·ÔÀÌ °¡µæ Â÷ ÀÖ½À´Ï´Ù.");
        //}
    }
    public void ChangeItem(ItemSet _item, ItemSet.ItemGrade itemGrade,int index)
    {
        int i = slotsIndex.IndexOf(slots[index].ChangeChild2.transform.GetComponentInParent<Slot>());
        slots[i].item = null;
        items[index] = _item;
        slots[index].myItemGrade = itemGrade;
        if(slots[index].ChangeChild2 != slots[index].orgChild)
        {
            slots[i].ChangeChild2 = slots[i].orgChild;
            slots[i].item = items[index];
            
        }
        else
        {
            slots[i].ChangeChild2 = slots[i].orgChild;
            slots[i].item = items[index];
         
        }
        

    }
    public void DestroyItem(int index) 
    {
        //items.RemoveAt(index);
        items[index] = null;
        slots[index].item = null;
    }
    
}