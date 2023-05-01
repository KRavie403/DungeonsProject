using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectItem : MonoBehaviour, IObjectItem
{
    [Header("아이템")]
    public ItemSet item;
    //[Header("아이템 이미지")]
    //public SpriteRenderer itemImage;

    void Start()
    {
        //itemImage.sprite = item.MySprite;
    }
    public ItemSet TriggerItem()
    {
        return this.item;
    }
   
}
