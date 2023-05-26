using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler/*, IDropHandler*/
{
    
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
        
        myItem = GetComponentInChildren<DragItem>();
        image = myItem.GetComponent<Image>();
        index = myItem.myIndex;
        orgChild = transform.GetChild(0);
        orgSlot = orgChild.GetComponentInParent<Slot>();
        Debug.Log($"{orgSlot}");
        ChangeChild = orgChild;
        ChangeChild2 = orgChild;
    }
    private void Update()
    {
        int i = 0;
        if (transform.childCount != 0)
        {
            orgChild = transform.GetChild(0);
            if (orgChild != ChangeChild)
            {
                myItem = GetComponentInChildren<DragItem>();
                image = myItem.GetComponent<Image>();
                index = myItem.myIndex;
                ChangeChild = orgChild;
                ChangeChild2 = orgChild;
            }
        }
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
            else
            {
                image.color = new Color(1, 1, 1, 0);
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
        if (myInventory.items[index] != null)
        {
            switch (myItemGrade)
            {
                case ItemSet.ItemGrade.Rare:
                    Power = myInventory.items[index].power * 1.0f;
                    break;
                case ItemSet.ItemGrade.Epic:
                    Power = myInventory.items[index].power * 2.0f;
                    break;
                case ItemSet.ItemGrade.Legendary:
                    Power = myInventory.items[index].power * 3.0f;
                    break;
            }
            switch (myInventory.items[index].myType)
            {
                case ItemSet.ItemType.Power:
                    Debug.Log("Power ÀåÂø");
                        myInventory.myEquipment.Additem(myInventory.items[index], myItemGrade, ItemSet.ItemType.Power, myInventory.items[index].myEquipmentType,Power,index);
                        //myInventory.DestroyItem(index);
                    break;
                case ItemSet.ItemType.Armor:
                    Debug.Log("Defence ÀåÂø");
                    myInventory.myEquipment.Additem(myInventory.items[index], myItemGrade, ItemSet.ItemType.Armor, myInventory.items[index].myEquipmentType, Power,index);
                    //myInventory.DestroyItem(index);
                    break;
                case ItemSet.ItemType.Accessories:
                    Debug.Log("Accessories ÀåÂø");
                    myInventory.myEquipment.Additem(myInventory.items[index], myItemGrade, ItemSet.ItemType.Accessories, myInventory.items[index].myEquipmentType, Power,index);
                    //myInventory.DestroyItem(index);
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