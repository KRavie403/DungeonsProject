using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectItem : MonoBehaviour, IObjectItem
{
    [Header("������")]
    public ItemSet item;
    //[Header("������ �̹���")]
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
