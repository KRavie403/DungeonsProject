using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class EquipmentSlot : MonoBehaviour
{
    public Image image;

    public Sprite defaltSprite;

    public Inventory myInventory;

    private ItemSet _item;

    public Equipment myEquipment;

    public ItemSet.ItemGrade myItemGrade;

    public float Power;

    private void Awake()
    {
        myEquipment = GetComponentInParent<Equipment>();
        myInventory = myEquipment.myInventory;
    }

    private void Start()
    {
        myEquipment = GetComponentInParent<Equipment>();
        myInventory = myEquipment.myInventory;
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
                switch (myItemGrade)
                {
                    case ItemSet.ItemGrade.Rare:
                        GetComponent<Image>().color = Color.white;
                        break;
                    case ItemSet.ItemGrade.Epic:
                        GetComponent<Image>().color = Color.green;
                        break;
                    case ItemSet.ItemGrade.Legendary:
                        GetComponent<Image>().color = Color.red;
                        break;
                }
            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
            }
        }
    }

    public float CheckPower()
    {
        switch (myItemGrade)
        {
            case ItemSet.ItemGrade.Rare:
                Power = myEquipment.items[myEquipment.slots.IndexOf(this)].power * 1.0f;
                break;
            case ItemSet.ItemGrade.Epic:
                Power = myEquipment.items[myEquipment.slots.IndexOf(this)].power * 2.0f;
                break;
            case ItemSet.ItemGrade.Legendary:
                Power = myEquipment.items[myEquipment.slots.IndexOf(this)].power * 3.0f;
                break;
        }
        return Power;
    }
}
