using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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

    public GameObject myConfirmButton;

    public ItemSet.ItemGrade myGrade;
    public ItemSet.EquipmentType myEquipmentType;
    public int Pindex;
    // Start is called before the first frame update



    void Start()
    {
        FreshSlot();
        slotsIndex = EquipmentSlotParent.GetComponentsInChildren<EquipmentSlot>();
        for (int i = 0; i < slotsIndex.Length; i++)
        {
            slots.Add(slotsIndex[i]);
        }
        for (int i = 0; i < slots.Count; i++)
        {
            items.Add(null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
        }
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
    public void Additem(ItemSet myItem,ItemSet.ItemGrade myGrade,ItemSet.ItemType myType,ItemSet.EquipmentType myEquipmentType,float Power,int Pindex)
    {
        this.Pindex = Pindex;
        this.myEquipmentType = myEquipmentType;
            switch (myType)
            {
                case ItemSet.ItemType.Power:
                    if (items[0] == null)
                    {
                        items[0] = myItem;

                        this.myGrade = myGrade;
                        slots[0].myItemGrade = myGrade;

                        myInventory.DestroyItem(Pindex);
                    //myInventory.myPlayer.AttackPower += Power;
                    //GetComponentInChildren<EquipmentSlot>().ChangeOutLine(myGrade);
                    FreshSlot();
                    }
                    else
                    {
                    //myInventory.slots[Pindex].myItem = GetComponentInChildren<DragItem>();
                    //myInventory.slots[Pindex].image = myInventory.slots[Pindex].myItem.GetComponent<Image>();
                    //myInventory.slots[Pindex].index = myInventory.slots[Pindex].myItem.myIndex;
                    ChangeItem(0,items[0],myItem, myGrade, myType, myEquipmentType, Power,Pindex);
                    FreshSlot();
                    }
                break;
                case ItemSet.ItemType.Armor:
                    if (myEquipmentType == ItemSet.EquipmentType.Helmet)
                    {
                        if (items[1] == null)
                        {
                            items[1] = myItem;
                            this.myGrade = myGrade;
                            slots[1].myItemGrade = myGrade;
                            myInventory.DestroyItem(Pindex);
                        //myInventory.myPlayer.DeffencePower += Power;
                        //GetComponentInChildren<EquipmentSlot>().ChangeOutLine(myGrade);
                        FreshSlot();
                        }
                        else
                        {
                        ChangeItem(1, items[1], myItem, myGrade, myType, myEquipmentType, Power, Pindex);
                        FreshSlot();
                        }
                }
                    
                if (myEquipmentType == ItemSet.EquipmentType.Armor)
                    {
                        if (items[1] == null)
                        {
                        items[1] = myItem;
                        this.myGrade = myGrade;
                        slots[1].myItemGrade = myGrade;

                        //myInventory.myPlayer.DeffencePower += Power;
                        //GetComponentInChildren<EquipmentSlot>().ChangeOutLine(myGrade);
                        myInventory.DestroyItem(Pindex);
                        FreshSlot();
                        }
                        else
                        {
                        ChangeItem(1, items[1], myItem, myGrade, myType, myEquipmentType, Power, Pindex);
                        FreshSlot();
                        }
                    }
                    if (myEquipmentType == ItemSet.EquipmentType.Boots)
                    {
                        if (items[1] == null)
                        {
                            items[1] = myItem;
                            this.myGrade = myGrade;
                            slots[1].myItemGrade = myGrade;
                        myInventory.DestroyItem(Pindex);
                        //myInventory.myPlayer.DeffencePower += Power;
                        //GetComponentInChildren<EquipmentSlot>().ChangeOutLine(myGrade);
                        FreshSlot();
                        }
                        else
                        {
                            ChangeItem(1, items[1], myItem, myGrade, myType, myEquipmentType, Power, Pindex);
                            FreshSlot();
                        }
                    }
                    break;
                case ItemSet.ItemType.Accessories:
                    if (myEquipmentType == ItemSet.EquipmentType.Ring)
                    {
                        if (items[2] == null)
                        {
                            items[2] = myItem;
                            this.myGrade = myGrade;
                            slots[2].myItemGrade = myGrade;
                        myInventory.DestroyItem(Pindex);
                        //GetComponentInChildren<EquipmentSlot>().ChangeOutLine(myGrade);
                        FreshSlot();
                        }
                        else
                        {
                            ChangeItem(2, items[2], myItem, myGrade, myType, myEquipmentType, Power, Pindex);
                            FreshSlot();
                        }

                    }
                    if (myEquipmentType == ItemSet.EquipmentType.Necklace)
                    {
                        if (items[3] == null)
                        {
                            items[3] = myItem;
                            this.myGrade = myGrade;
                            slots[3].myItemGrade = myGrade;
                        myInventory.DestroyItem(Pindex);
                        //GetComponentInChildren<EquipmentSlot>().ChangeOutLine(myGrade);
                        FreshSlot();
                        }
                        else
                        {
                            ChangeItem(3, items[3], myItem, myGrade, myType, myEquipmentType, Power, Pindex);
                            FreshSlot();
                        }
                    }
                    if (myEquipmentType == ItemSet.EquipmentType.EarRing)
                    {
                        if (items[4] == null)
                        {
                            items[4] = myItem;
                            this.myGrade = myGrade;
                            slots[4].myItemGrade = myGrade;
                        myInventory.DestroyItem(Pindex);
                        //GetComponentInChildren<EquipmentSlot>().ChangeOutLine(myGrade);
                        FreshSlot();
                        }
                        else
                        {
                            ChangeItem(4, items[4], myItem, myGrade, myType, myEquipmentType, Power, Pindex);
                            FreshSlot();
                        }
                    }
                    if (myEquipmentType == ItemSet.EquipmentType.Bracelet)
                    {
                        if (items[5] == null)
                        {
                            items[5] = myItem;
                            this.myGrade = myGrade;
                            slots[5].myItemGrade = myGrade;
                        myInventory.DestroyItem(Pindex);
                        //GetComponentInChildren<EquipmentSlot>().ChangeOutLine(myGrade);
                        FreshSlot();
                        }
                        else
                        {
                            ChangeItem(5, items[5], myItem, myGrade, myType, myEquipmentType, Power, Pindex);
                            FreshSlot();
                        }
                    }
                    break;
        }
    }
    public void DestroyItem(int index)
    {
        //items.RemoveAt(index);
        items[index] = null;
    }
    public void Open()
    {
        this.gameObject.SetActive(true);
    }
    public void ChangeItem(int index,ItemSet orgItem ,ItemSet myItem, ItemSet.ItemGrade myGrade, ItemSet.ItemType myType, ItemSet.EquipmentType myEquipmentType, float Power,int Cindex)
    {
        ItemSet ChangeItem = myItem;
        ItemSet.ItemGrade ChangeGrade = myGrade;
        ItemSet.ItemType ChangeType = myType;
        ItemSet.EquipmentType ChangeEquipmentType = myEquipmentType;
        float ChangePower = Power;
        ItemSet.ItemGrade orgGrade = slots[index].myItemGrade;
        items[index] = null;
        Additem(ChangeItem, ChangeGrade, ChangeType, ChangeEquipmentType, ChangePower,Cindex);
        myInventory.ChangeItem(orgItem, orgGrade, Cindex);

    }
}
