//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ResolutionSetting : MonoBehaviour
//{
//    public GameObject GraphicBtn;

//    public GameObject ResetButton;
//    float currentPosition;
//    Vector3 dir = new Vector3(10, 0, 0); //�̵� �ӵ� ����
//    void Start()
//    {
//        currentPosition = transform.position.x;

//    }
//    void Update()
//    {

//    }

//    public void ClickResolution()
//    {
//        StartCoroutine(OpenResolution());
//        //StartCoroutine(CloseResolution());
//    }

//    IEnumerator OpenResolution(Vector3 pos)
//    {
//        while (currentPosition >= 0)
//        {
//            float delta = Time.deltaTime; //���� �Ÿ� ���
//            if (currentPosition - delta <= 0.0f) //�̵��� �� ��ġ�� �������� ���� ����� �Ÿ��� ����
//            {
//                delta = currentPosition; //���� �Ÿ� ��� = ���� ������ �Ÿ� - ���� �Ÿ�
//                currentPosition = 50.0f;
//                dir = -dir;
//            }
//            currentPosition -= delta; //�̵��� ��ŭ �Ÿ� ����
//            transform.Translate(dir * delta);

//            if(currentPosition <= 0)
//            {
//                yield return null;
//            }

//        }
//        Vector3 dir = pos - currentPosition; //�Ÿ� (��ǥ-��ǥ)
//        float dist = dir.magnitude; // Vector3.Distance(pos, transform.position); //�� ���� ������ �Ÿ�
//        dir.Normalize(); //���� �߿�

//        while (dist > 0.0f)
//        {
//            float delta = 10.0f * Time.deltaTime; //�ʴ� 2m�ӵ�
//            if (dist - delta < 0.0f)
//            {
//                delta = dist;
//            }
//            transform.Translate(dir * delta/*, Space.World*/); //vector�� �Ǽ��� �����ָ� ���̰� ����

//            dist -= delta;

//            yield return null;
//        }


//    }

//    //IEnumerator CloseResolution()
//    //{
//    //    while ()
//    //    {
//    //        float delta = Time.deltaTime; //���� �Ÿ� ���
//    //        if (currentPosition - delta <= 0.0f) //�̵��� �� ��ġ�� �������� ���� ����� �Ÿ��� ����
//    //        {
//    //            currentPosition += delta;
//    //        }
//    //        transform.Translate(dir * delta);
//    //        yield return null;
//    //    }

//    //}


//}
