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

    private void OnTriggerEnter(Collider other) //������ǥ�� �������� ����ǥ�� ������븦 �̵�
    {
        int rnd = Random.Range(1, 11);
        Debug.Log("����");
        random(rnd);
        //pos���� ����������?
    }
}
