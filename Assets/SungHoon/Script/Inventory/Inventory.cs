using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    
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

    public Transform curEquipment;

    public Transform[] myEquipmentParent;

    public List<Slot> slotsIndex;

    public List<ItemList> myItemList;

    public int curIndex;
    private GameManager GetGmInst()
    {
        return GameManager.Inst;
    }
    private void OnValidate()
    {
        
        slots = slotParent[0].GetComponentsInChildren<Slot>();
        myEquipment=myEquipmentParent[0].GetComponentInChildren<Equipment>();
        curEquipment = myEquipmentParent[0];

    }

    void Awake()
    {
        curIndex = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            slotsIndex.Add(slots[i]);
        }
        for(int i=0; i < slotParent.Length; i++)
        {
            myItemList.Add(slotParent[i].GetComponent<ItemList>());
        }
        orgPlayer = null;
        FreshSlot();
    }
   
    private void Update()
    {

        if (GetGmInst().characters.Count != 0)
        {
            //if(myPlayer.Item_stat.Count. GetGmInst().characters.Count)
            StatModifire myEquipmentStat = new StatModifire();
            myPlayer = GetGmInst().characters[GetGmInst().curCharacter].GetComponent<Player>();
            if (myPlayer != null)
            {
                if (myPlayer != orgPlayer)
                {
                    curEquipment.gameObject.SetActive(false);
                    curIndex++;
                    FreshSlot();   
                    if ((orgPlayer == null))
                    {
                        curIndex = 0;
                    }
                    slots = slotParent[curIndex].GetComponentsInChildren<Slot>();
                    myEquipment = myEquipmentParent[curIndex].GetComponentInChildren<Equipment>();
                    curEquipment = myEquipmentParent[curIndex];
                    for(int i = 0; i < slots.Length; i++)
                    {
                        slotsIndex[i] = null;
                        slotsIndex[i] = slots[i];
                    }
                }
                orgPlayer = myPlayer;
                if (myPlayer.Item_stat.Count == 0)
                {
                    myPlayer.Item_stat.Add(myEquipmentStat);
                }
            }
            else
            {
                orgPlayer = null;
                curIndex = 0;
            }
            //myPlayer.Item_stat[0].AttackPower += 10;
            //Debug.Log($"{myPlayer.Item_stat.Count}");
            //Debug.Log($"{myPlayer.Item_stat[0].AttackPower}");
        }
       
    }
    public void openEquipment()
    {
        if (curEquipment.gameObject.activeSelf == false)
        {
            curEquipment.gameObject.SetActive(true);
        }
        else
        {
            curEquipment.gameObject.SetActive(false);
        }
          
    }
    public void FreshSlot()
    {
        int i = 0;
        for (; i < myItemList[curIndex].items.Count && i < slots.Length; i++)
        {
            slots[i].item = myItemList[curIndex].items[i];
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
            if (myItemList[curIndex].items[i] == null)
            { 
                myItemList[curIndex].items[i] = _item;
                slots[i].myItemGrade = itemGrade;
                slots[i].item = myItemList[curIndex].items[i];
                FreshSlot();
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
        int i = slotsIndex.IndexOf(slots[index].GetComponent<Slot>());
        slots[i].item = null;
        myItemList[curIndex].items[index] = _item;
        slots[index].myItemGrade = itemGrade;
        if(slots[index].ChangeChild2 != slots[index].orgChild)
        {
            slots[i].ChangeChild2 = slots[i].orgChild;
            slots[i].item = myItemList[curIndex].items[index];
        }
        else
        {
            slots[i].ChangeChild2 = slots[i].orgChild;
            slots[i].item = myItemList[curIndex].items[index];
        }
        FreshSlot();
    }
    public void DestroyItem(int index) 
    {
        //items.RemoveAt(index);
        myItemList[curIndex].items[index] = null;
        FreshSlot();
    }
    
}