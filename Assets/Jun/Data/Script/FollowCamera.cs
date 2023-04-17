using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour //카메라동작??
{
    public LayerMask crashMask;
    public float Rotspeed;
    public float zoomspeed;
    public Transform myTarget;

    [Range(0, 45)]
    public float MinAngle = 45; //0~45
    [Range(45, 80)]
    public float MaxAngle = 80; //45~80


    Vector3 dir = Vector3.zero;
    float Dist = 0.0f;
    float targetDist = 0.0f;

    private void Awake()
    {
        transform.LookAt(myTarget); //플레이어에 붙어있는 ViewPoint를 보게함
        dir = transform.position - myTarget.position; 
        targetDist = Dist = dir.magnitude;
        dir.Normalize();
        dir = transform.InverseTransformDirection(dir); //플레이어와 카메라의 거리유지??

    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = myTarget.position + Quaternion.Euler(MinAngle, 0, 0) * dir * Dist; //카메라 위치와 각도 초기화??
    }

    Quaternion rotX = Quaternion.identity, rotY = Quaternion.identity;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float x = Input.GetAxis("Mouse Y"); //마우스 Y축움직일시 -1~1로 수치나옴
            float y = Input.GetAxis("Mouse X"); //마우스 X축움직일시 -1~1로 수치나옴
            //쿼터니언

            rotX *= Quaternion.Euler(x, 0, 0);

            float angle = rotX.eulerAngles.x;
            if (angle > 180.0f)
                angle -= 360.0f;
            angle = Mathf.Clamp(angle, MinAngle, MaxAngle);

            rotX = Quaternion.Euler(angle, 0, 0);

            rotY *= Quaternion.Euler(0, y, 0);

            //Quaternion rot = Quaternion.Euler(x, y, 0);
            //transform.forward = -dir;
        }
        targetDist -= Input.GetAxis("Mouse ScrollWheel") * zoomspeed;
        targetDist = Mathf.Clamp(targetDist, 1.0f, 10.0f);

        Dist = Mathf.Lerp(Dist, targetDist, Time.deltaTime * 3.0f);

        Vector3 target_dir = rotY * rotX * dir;
        float radius = 0.5f;
        if (Physics.Raycast(new Ray(myTarget.position, target_dir), out RaycastHit hit, Dist + radius, crashMask))
        {
            //transform.position = hit.point + -target_dir * radius;
            Dist = hit.distance - radius;
        }
        //transform.position = myTarget.position + target_dir * Dist;
        transform.position = myTarget.position + target_dir * Dist;
        transform.LookAt(myTarget);
    }
}
