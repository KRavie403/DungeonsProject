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
                        Power = myEquipment.items[myEquipment.slots.IndexOf(this)].power * 1.0f;
                        GetComponent<Image>().color = Color.white;
                        break;
                    case ItemSet.ItemGrade.Epic:
                        Power = myEquipment.items[myEquipment.slots.IndexOf(this)].power * 2.0f;
                        GetComponent<Image>().color = Color.green;
                        break;
                    case ItemSet.ItemGrade.Legendary:
                        Power = myEquipment.items[myEquipment.slots.IndexOf(this)].power * 3.0f;
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
    // Start is called before the first frame update
    void Start()
    {
        myEquipment = GetComponentInParent<Equipment>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
