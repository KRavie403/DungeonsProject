using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSystem : MonoBehaviour
{
    public GameObject orgObject;
    Vector3 pos = Vector3.zero;
    int x = 0, z = 0;
    public void random(int rd)
    {
        switch (rd)
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
    void teleportSystem()
    {
        int rd;

        for (int j = 0; j < 10;) //10번반복
        {
            rd = Random.Range(1, 11); //1-10 랜덤숫자 넣기
            if (list.Contains(rd)) //list에서 중복된숫자가있다면
            {
                rd = Random.Range(1, 11);
            }
            else //list에서 중복된숫자가없다면 list에 숫자넣고 j++
            {
                list.Add(rd); //0번인덱스부터 9까지 1-11랜덤숫자 넣기              
                j++;
            }
        }

        for (int i = 0; i < 5; ++i)
        {
            Debug.Log(list[i] + "   " + i);
            random(list[i]);
            GameObject obj = Instantiate(orgObject, pos, Quaternion.identity);
        }
    }

    List<int> list = new List<int>();

    void Start() //랜덤숫자 뽑기 -> 중복됬다면 다시 뽑기 -> 위치이동
    {
        teleportSystem();
    }
    void Update()
    {
        
    }
}
