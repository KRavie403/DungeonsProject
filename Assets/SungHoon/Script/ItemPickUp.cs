using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [Header("인벤토리")]
    public Inventory inventory;

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player 충돌");
        IObjectItem itemObj = other.gameObject.GetComponent<IObjectItem>();
        if (itemObj != null)
        {
            ItemSet item = itemObj.TriggerItem();
            print($"{item.name}");
            inventory.AddItem(item);
        }
    }
}
