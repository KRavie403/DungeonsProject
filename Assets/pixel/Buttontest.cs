using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttontest : MonoBehaviour
{
    public GameObject Stat;
    bool active = false;
    public void Button()
    {
        active = !active;
        Stat.SetActive(active);
    }
    void Start()
    {
        Stat.SetActive(active);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
