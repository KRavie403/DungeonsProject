using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSystem : MonoBehaviour
{
    public GameObject orgObject;
    Vector3 pos = Vector3.zero;
    int x = 0, z = 0;
    void random(int r)
    {
        int p = Random.Range(1, 11);
        switch (p)
        {
            case 1:
                x = 80;
                z = 80;
                break;
            case 2:
                x = 70;
                z = 15;
                break;
            case 3:
                x = 58;
                z = 50;
                break;
            case 4:
                x = 42;
                z = 89;
                break;
            case 5:
                x = 18;
                z = 85;
                break;
            case 6:
                x = 24;
                z = 10;
                break;
            case 7:
                x = 26;
                z = 33;
                break;
            case 8:
                x = 15;
                z = 62;
                break;
            case 9:
                x = 45;
                z = 63;
                break;
            case 10:
                x = 76;
                z = 44;
                break;
        }
        pos = new Vector3(x, 1, z);
    }
    List<int> list = new List<int>();
    void Start() //랜덤숫자 뽑기 -> 중복됬다면 다시 뽑기 -> 위치이동
    {

        random(list[0]);
        GameObject obj = Instantiate(orgObject, pos, Quaternion.identity);
    }
    void Update()
    {
        
    }
}
