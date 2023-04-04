using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInventory : MonoBehaviour
{
    public GameObject Invetory;
    bool active = false;
    public void Button()
    {
        active = !active;
        Invetory.SetActive(active);
    }
    void Start()
    {
        Invetory.SetActive(active);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
