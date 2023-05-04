using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapsControl : MonoBehaviour
{
    //public GameObject Maps;
    private Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MapSetting()
    {
        if (ani.GetBool("isNext"))
        {
            ani.SetBool("isNext", false);
        }
        else
            ani.SetBool("isNext", true);
        
    }
}
