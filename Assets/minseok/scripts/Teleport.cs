using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : TeleportSystem
{
    void Start()
    {

    }
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other) //랜덤좌표값 가져오기 그좌표로 닿은상대를 이동
    {
        int rnd = Random.Range(1, 11);
        Debug.Log("닿음");
        random(rnd);
        //pos값을 가져오려면?
    }
}
