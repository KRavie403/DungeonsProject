using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ChestUISlot : MonoBehaviour, IPointerClickHandler
{
    public Image image;

    public Sprite defaltSprite;

    public Inventory myInventory;

    public ChestUI myChest;

    private ItemSet _item;

    [Serializable]
    public  struct myItem
    {
        [SerializeField]
        public ItemSet.ItemGrade grade;

    }
    public myItem myItemGrade;

    //상자에 랜덤으로 넣기 완 
    //장비창 
    //변경

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
    public void OnPointerClick(PointerEventData eventData)
    {
        image = GetComponentInChildren<Image>();
        int index = myChest.slots.IndexOf(this);
        if (eventData.clickCount == 2)
        {
            image.sprite = defaltSprite;
            bool flag = false;
            for (int i = 0; i < myInventory.myItemList[myInventory.curIndex].items.Count; i++)
            {
                if (myInventory.myItemList[myInventory.curIndex].items[i] == null)
                {
                    flag = true;
                }
            }
            if (flag == true)
            {
                myInventory.AddItem(myChest.items[index], myItemGrade.grade);
            }
            myChest.DestroyItem(index);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        myInventory = myChest.myInventory;
        int randGrade = UnityEngine.Random.Range(0, 100);
        if(randGrade > 90)
        {
            myItemGrade.grade = ItemSet.ItemGrade.Legendary;
        }else if(randGrade > 50)
        {
            myItemGrade.grade = ItemSet.ItemGrade.Epic;
        }
        else if(randGrade > 0)
        {
            myItemGrade.grade = ItemSet.ItemGrade.Rare;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
