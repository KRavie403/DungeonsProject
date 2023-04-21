//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ResolutionSetting : MonoBehaviour
//{
//    public GameObject GraphicBtn;

//    public GameObject ResetButton;
//    float currentPosition;
//    Vector3 dir = new Vector3(10, 0, 0); //이동 속도 방향
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
//            float delta = Time.deltaTime; //남은 거리 계산
//            if (currentPosition - delta <= 0.0f) //이동할 때 위치로 생각하지 말고 방향과 거리로 생각
//            {
//                delta = currentPosition; //남은 거리 계산 = 내가 가야할 거리 - 현재 거리
//                currentPosition = 50.0f;
//                dir = -dir;
//            }
//            currentPosition -= delta; //이동한 만큼 거리 감소
//            transform.Translate(dir * delta);

//            if(currentPosition <= 0)
//            {
//                yield return null;
//            }

//        }
//        Vector3 dir = pos - currentPosition; //거리 (좌표-좌표)
//        float dist = dir.magnitude; // Vector3.Distance(pos, transform.position); //두 지점 사이의 거리
//        dir.Normalize(); //순서 중요

//        while (dist > 0.0f)
//        {
//            float delta = 10.0f * Time.deltaTime; //초당 2m속도
//            if (dist - delta < 0.0f)
//            {
//                delta = dist;
//            }
//            transform.Translate(dir * delta/*, Space.World*/); //vector에 실수를 곱해주면 길이가 변함

//            dist -= delta;

//            yield return null;
//        }


//    }

//    //IEnumerator CloseResolution()
//    //{
//    //    while ()
//    //    {
//    //        float delta = Time.deltaTime; //남은 거리 계산
//    //        if (currentPosition - delta <= 0.0f) //이동할 때 위치로 생각하지 말고 방향과 거리로 생각
//    //        {
//    //            currentPosition += delta;
//    //        }
//    //        transform.Translate(dir * delta);
//    //        yield return null;
//    //    }

//    //}


//}
