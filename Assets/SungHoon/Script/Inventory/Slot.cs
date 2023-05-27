using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using System;

public class Slot : MonoBehaviour, IPointerClickHandler/*, IDropHandler*/
{
    [SerializeField]
    public List<ItemSet> items;

    public Image image;
    
    public Sprite defaltSprite;

    public Inventory myInventory;

    private ItemSet _item;

    public DragItem myItem;

    [SerializeField]
    public ItemSet.ItemGrade myItemGrade;

    public int index;

    public Slot orgSlot;

    public Transform orgChild;

    public Transform ChangeChild;

    public Transform ChangeChild2;
    private void Start()
    {
        for (int i = 0; i < myInventory.slots.Length; i++)
        {
            items.Add(null);
        }
        myItem = GetComponentInChildren<DragItem>();
        image = myItem.GetComponent<Image>();
        index =myInventory.slotsIndex.IndexOf(GetComponent<Slot>()); 
    }
    private void Update()
    {
        index = myInventory.slotsIndex.IndexOf(GetComponent<Slot>());
    }
    public ItemSet item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item != null)
            {
                image.sprite = item.MySprite;
                image.color = new Color(1, 1, 1, 1); 
            }
        }
    }
    //public void OnDrop(PointerEventData eventData)
    //{
        
    //    DragItem newItem = eventData.pointerDrag.GetComponent<DragItem>();
    //    DragItem curItem = GetComponentInChildren<DragItem>();
    //    if (curItem != null)
    //    {
    //        curItem.ChageParent(newItem.orgParent, true);
    //    }
    //    newItem.ChageParent(transform);
    //}
    float Power;

    public void ItemEquipment()
    {
        if (myInventory.myItemList[myInventory.curIndex].items[index] != null)
        {
            switch (myItemGrade)
            {
                case ItemSet.ItemGrade.Rare:
                    Power = myInventory.myItemList[myInventory.curIndex].items[index].power * 1.0f;
                    break;
                case ItemSet.ItemGrade.Epic:
                    Power = myInventory.myItemList[myInventory.curIndex].items[index].power * 2.0f;
                    break;
                case ItemSet.ItemGrade.Legendary:
                    Power = myInventory.myItemList[myInventory.curIndex].items[index].power * 3.0f;
                    break;
            }
            switch (myInventory.myItemList[myInventory.curIndex].items[index].myType)
            {
                case ItemSet.ItemType.Power:
                    Debug.Log("Power ÀåÂø");
                        myInventory.myEquipment.Additem(myInventory.myItemList[myInventory.curIndex].items[index], myItemGrade, ItemSet.ItemType.Power, myInventory.myItemList[myInventory.curIndex].items[index].myEquipmentType,Power,index);
                    break;
                case ItemSet.ItemType.Armor:
                    Debug.Log("Defence ÀåÂø");
                    myInventory.myEquipment.Additem(myInventory.myItemList[myInventory.curIndex].items[index], myItemGrade, ItemSet.ItemType.Armor, myInventory.myItemList[myInventory.curIndex].items[index].myEquipmentType, Power,index);
                    break;
                case ItemSet.ItemType.Accessories:
                    Debug.Log("Accessories ÀåÂø");
                    myInventory.myEquipment.Additem(myInventory.myItemList[myInventory.curIndex].items[index], myItemGrade, ItemSet.ItemType.Accessories, myInventory.myItemList[myInventory.curIndex].items[index].myEquipmentType, Power,index);
                    break;
                case ItemSet.ItemType.Potion:
                    myInventory.DestroyItem(index);
                    break;
            }

        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            ItemEquipment();
        }
        //if (eventData.clickCount == 3)
        //{
        //    myInventory.DestroyItem(index);
        //}
    }
}