using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileState : MonoBehaviour //����� ������ ĭ���鿡�� �־�������
{
    public int isVisited; //??????????
    public int x_pos; //??????????
    public int y_pos; //??????????


    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (other.gameObject.layer == LayerMask.NameToLayer("Structures")) //������Ʈ�� ������
        {
            Destroy(this.gameObject); //�ı�
        }
    }

}