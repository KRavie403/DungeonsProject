using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionControl : MonoBehaviour
{
    private Animator ani;

    public GameObject GraphicBtn;
    public GameObject ResetBtn;

    void Start()
    {
        ani = GetComponent<Animator>();
    }
    void Update()
    {

    }

    int count = 0;
    public void ResolutionSetting()
    {
        if (ani.GetBool("isMovingRight"))
        {
            ani.SetBool("isMovingRight", false);
        }
        else
        {
            ani.SetBool("isMovingRight", true);
        }

    }
}
