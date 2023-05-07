using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    [SerializeField] Image image;
    private ItemSet _item;
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
    public void OnDrop(PointerEventData eventData)
    {
        DragItem newItem = eventData.pointerDrag.GetComponent<DragItem>();
        DragItem curItem = GetComponentInChildren<DragItem>();
        if (curItem != null)
        {
            curItem.ChageParent(newItem.orgParent, true);
        }
        newItem.ChageParent(transform);
    }
}