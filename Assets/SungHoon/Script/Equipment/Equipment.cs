using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using static ItemSet;

public class Equipment : MonoBehaviour
{
    [SerializeField]
    public List<ItemSet> items;

    [SerializeField]
    private Transform EquipmentSlotParent;
    [SerializeField]
    public List<EquipmentSlot> slots;

    [SerializeField]
    public Inventory myInventory;

    EquipmentSlot[] slotsIndex;

    public ItemSet.ItemGrade myGrade;
    public ItemSet.EquipmentType myEquipmentType;
    public int Pindex;
    // Start is called before the first frame update

    public List<float> CharPower;

    private void Awake()
    {
        slotsIndex = EquipmentSlotParent.GetComponentsInChildren<EquipmentSlot>();
        for (int i = 0; i < slotsIndex.Length; i++)
        {
            slots.Add(slotsIndex[i]);
        }
        for (int i = 0; i < slots.Count; i++)
        {
            items.Add(null);
            CharPower.Add(0.0f);
        }
        FreshSlot();
    }
    private void Start()
    {
        this.transform.parent.gameObject.SetActive(false);
        this.transform.parent.parent.gameObject.SetActive(false);
    }
    public void FreshSlot()
    {
        int i = 0;
        for (; i <myInventory.myEquipment.items.Count && i < slots.Count; i++)
        {
            slots[i].item = myInventory.myEquipment.items[i];
        }
        for (; i < slots.Count; i++)
        {
            slots[i].item = null;
        }
    }
    public void Additem(ItemSet myItem,ItemSet.ItemGrade myGrade,ItemSet.ItemType myType,ItemSet.EquipmentType myEquipmentType,float Power,int Pindex)
    {
        this.Pindex = Pindex;
        this.myEquipmentType = myEquipmentType;
            switch (myType)
            {
                case ItemSet.ItemType.Power:
                    if (myInventory.myEquipment.items[0] == null)
                    {
                        myInventory.myEquipment.items[0] = myItem;
                        this.myGrade = myGrade;
                        slots[0].myItemGrade = myGrade;
                    myInventory.DestroyItem(Pindex);
                    CharPower[0] = slots[0].CheckPower();
                    myInventory.myPlayer.Item_stat[0].AttackPower += CharPower[0];
                    FreshSlot();
                    Debug.Log($"{myInventory.myPlayer.Item_stat[0].AttackPower}");
                }
                    else
                    {
                    myInventory.myPlayer.Item_stat[0].AttackPower -= CharPower[0];
                    ChangeItem(0,myInventory.myEquipment.items[0],myItem, myGrade, myType, myEquipmentType, Power,Pindex);
                    FreshSlot();
                    Debug.Log($"{myInventory.myPlayer.Item_stat[0].AttackPower}");
                }
                break;
                case ItemSet.ItemType.Armor:
                    if (myEquipmentType == ItemSet.EquipmentType.Helmet)
                    {
                        if (myInventory.myEquipment.items[1] == null)
                        {
                            myInventory.myEquipment.items[1] = myItem;
                            this.myGrade = myGrade;
                            slots[1].myItemGrade = myGrade;
                            myInventory.DestroyItem(Pindex);
                        CharPower[1] = slots[1].CheckPower();
                        myInventory.myPlayer.Item_stat[0].DeffencePower += CharPower[1];
                        FreshSlot();
                        Debug.Log($"{myInventory.myPlayer.Item_stat[0].DeffencePower}");
                    }
                    else
                        {
                        myInventory.myPlayer.Item_stat[0].DeffencePower -= CharPower[1];
                        Debug.Log($"{myInventory.myPlayer.Item_stat[0].DeffencePower}");
                        ChangeItem(1, myInventory.myEquipment.items[1], myItem, myGrade, myType, myEquipmentType, Power, Pindex);
                        FreshSlot();
                        }
                }
                    
                if (myEquipmentType == ItemSet.EquipmentType.Armor)
                    {
                        if (myInventory.myEquipment.items[1] == null)
                        {
                        myInventory.myEquipment.items[1] = myItem;
                        this.myGrade = myGrade;
                        slots[1].myItemGrade = myGrade;

                        //myInventory.myPlayer.DeffencePower += Power;
                        //GetComponentInChildren<EquipmentSlot>().ChangeOutLine(myGrade);
                        myInventory.DestroyItem(Pindex);
                        CharPower[1] = slots[1].CheckPower();
                        myInventory.myPlayer.Item_stat[0].DeffencePower += CharPower[1];
                        FreshSlot();
                        Debug.Log($"{myInventory.myPlayer.Item_stat[0].DeffencePower}");
                    }
                    else
                        {
                        myInventory.myPlayer.Item_stat[0].DeffencePower -= CharPower[1];
                        Debug.Log($"{myInventory.myPlayer.Item_stat[0].DeffencePower}");
                        ChangeItem(1, myInventory.myEquipment.items[1], myItem, myGrade, myType, myEquipmentType, Power, Pindex);
                        FreshSlot();
                        }
                    }
                    if (myEquipmentType == ItemSet.EquipmentType.Boots)
                    {
                        if  (myInventory.myEquipment.items[1] == null)
                        {
                            myInventory.myEquipment.items[1] = myItem;
                            this.myGrade = myGrade;
                            slots[1].myItemGrade = myGrade;
                        myInventory.DestroyItem(Pindex);
                        CharPower[1] = slots[1].CheckPower();
                        myInventory.myPlayer.Item_stat[0].DeffencePower += CharPower[1];
                        FreshSlot();
                        Debug.Log($"{myInventory.myPlayer.Item_stat[0].DeffencePower}");
                    }
                        else
                        {
                        myInventory.myPlayer.Item_stat[0].DeffencePower -= CharPower[1];
                        Debug.Log($"{myInventory.myPlayer.Item_stat[0].DeffencePower}");
                        ChangeItem(1, myInventory.myEquipment.items[1], myItem, myGrade, myType, myEquipmentType, Power, Pindex);
                            FreshSlot();
                        }
                    }
                    break;
                case ItemSet.ItemType.Accessories:
                    if (myEquipmentType == ItemSet.EquipmentType.Ring)
                    {
                        if (myInventory.myEquipment.items[2] == null)
                        {
                            myInventory.myEquipment.items[2] = myItem;
                            this.myGrade = myGrade;
                            slots[2].myItemGrade = myGrade;
                        myInventory.DestroyItem(Pindex);
                        CharPower[2] = slots[2].CheckPower();
                        FreshSlot();
                    }
                        else
                        {
                        ChangeItem(2, myInventory.myEquipment.items[2], myItem, myGrade, myType, myEquipmentType, Power, Pindex);
                            FreshSlot();
                        }

                    }
                    if (myEquipmentType == ItemSet.EquipmentType.Necklace)
                    {
                        if (myInventory.myEquipment.items[3] == null)
                        {
                            myInventory.myEquipment.items[3] = myItem;
                            this.myGrade = myGrade;
                            slots[3].myItemGrade = myGrade;
                        myInventory.DestroyItem(Pindex);
                        CharPower[3] = slots[3].CheckPower();
                        FreshSlot();
                    }
                        else
                        {
                        myInventory.myPlayer.Item_stat[0].AttackPower -= CharPower[3];
                        ChangeItem(3, myInventory.myEquipment.items[3], myItem, myGrade, myType, myEquipmentType, Power, Pindex);
                            FreshSlot();
                        }
                    }
                    if (myEquipmentType == ItemSet.EquipmentType.EarRing)
                    {
                        if (myInventory.myEquipment.items[4] == null)
                        {
                            myInventory.myEquipment.items[4] = myItem;
                            this.myGrade = myGrade;
                            slots[4].myItemGrade = myGrade;
                        myInventory.DestroyItem(Pindex);
                        CharPower[4] = slots[4].CheckPower();
                        FreshSlot();
                    }
                        else
                        {
                        ChangeItem(4, myInventory.myEquipment.items[4], myItem, myGrade, myType, myEquipmentType, Power, Pindex);
                            FreshSlot();
                        }
                    }
                    if (myEquipmentType == ItemSet.EquipmentType.Bracelet)
                    {
                        if (myInventory.myEquipment.items[5] == null)
                        {
                            myInventory.myEquipment.items[5] = myItem;
                            this.myGrade = myGrade;
                            slots[5].myItemGrade = myGrade;
                        myInventory.DestroyItem(Pindex);
                        CharPower[5] = slots[5].CheckPower();
                        FreshSlot();
                    }
                        else
                        {
                        ChangeItem(5, myInventory.myEquipment.items[5], myItem, myGrade, myType, myEquipmentType, Power, Pindex);
                            FreshSlot();
                        }
                    }
                break;
                    
                    
        }
    }
    public void DestroyItem(int index)
    {
        //items.RemoveAt(index);
        myInventory.myEquipment.items[index] = null;
    }
    public void ChangeItem(int index,ItemSet orgItem ,ItemSet myItem, ItemSet.ItemGrade myGrade, ItemSet.ItemType myType, ItemSet.EquipmentType myEquipmentType, float Power,int Cindex)
    {
        ItemSet ChangeItem = myItem;
        ItemSet.ItemGrade ChangeGrade = myGrade;
        ItemSet.ItemType ChangeType = myType;
        ItemSet.EquipmentType ChangeEquipmentType = myEquipmentType;
        float ChangePower = Power;
        ItemSet.ItemGrade orgGrade = slots[index].myItemGrade;
        myInventory.myEquipment.items[index] = null;
        Additem(ChangeItem, ChangeGrade, ChangeType, ChangeEquipmentType, ChangePower,Cindex);
        myInventory.ChangeItem(orgItem, orgGrade, Cindex);

    }
}
