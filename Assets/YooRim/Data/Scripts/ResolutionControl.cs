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

    public void ResolutionSetting()
    {
        if (ani.GetBool("isMovingRight"))
        {
            ani.SetBool("isMovingRight", false);
            ani.SetBool("isMovingLeft", true);
        }
        else
            ani.SetBool("isMovingRight", true);
            ani.SetBool("isMovingLeft", false);
    }
}
