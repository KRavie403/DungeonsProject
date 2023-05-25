using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem : MonoBehaviour/*,IBeginDragHandler, IDragHandler, IEndDragHandler*/
{
    Vector2 dragOffset = Vector2.zero;

    public Slot mySlot;
    public int myIndex;
    [field: SerializeField]
    public Transform orgParent
    {
        get; private set;
    }
    private void OnValidate()
    {
        mySlot = GetComponentInParent<Slot>();
    }
    private void Start()
    {
        myIndex = mySlot.myInventory.slotsIndex.IndexOf(mySlot);
        orgParent = transform.parent;
    }
    private void Update()
    {
        if (orgParent != null)
        {
            mySlot = GetComponentInParent<Slot>();
            
        }   
    }
    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    dragOffset = (Vector2)transform.position - eventData.position;
    //    orgParent = transform.parent;
    //    transform.SetParent(transform.parent.parent.parent);
    //    transform.SetAsLastSibling();
    //    GetComponent<Image>().raycastTarget = false;
    //}
    //public void OnDrag(PointerEventData eventData)
    //{
    //    transform.position = eventData.position + dragOffset;
    //}
    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    transform.SetParent(orgParent);
    //    transform.localPosition = Vector3.zero;
    //    GetComponent<Image>().raycastTarget = true;
    //}

    //public void ChageParent(Transform p, bool update = false)
    //{
    //    orgParent = p;
    //    if (update)
    //    {
    //        transform.SetParent(p);
    //        transform.localPosition = Vector3.zero;
    //    }
    //}

}
