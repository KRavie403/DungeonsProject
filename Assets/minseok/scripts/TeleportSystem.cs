using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSystem : MonoBehaviour
{
    public static TeleportSystem main_teleport = null;

    public GameObject orgObject;
    public GameObject TPUI;
    Vector2Int pos = Vector2Int.zero;
    Vector3 testpos = Vector3.zero;
    public List<Teleport> teleporters;
    public GameObject mytarget = null;
    int x = 0, y = 0;
    public void random(int rd)
    {

        switch (rd)
        {
            case 1:
                x = 80;
                y = 80;
                break;
            case 2:
                x = 70;
                y = 15;
                break;
            case 3:
                x = 58;
                y = 50;
                break;
            case 4:
                x = 42;
                y = 89;
                break;
            case 5:
                x = 18;
                y = 85;
                break;
            case 6:
                x = 24;
                y = 10;
                break;
            case 7:
                x = 26;
                y = 33;
                break;
            case 8:
                x = 15;
                y = 62;
                break;
            case 9:
                x = 45;
                y = 63;
                break;
            case 10:
                x = 76;
                y = 44;
                break;
        }

        Vector2Int my_Pos = new Vector2Int(x, y);

        float half = GameManager.Inst.scale * 0.5f;
        testpos = new Vector3((float)my_Pos.x + half, 0, (float)my_Pos.y + half);

        pos = new Vector2Int(x, y);
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
            random(list[i]);
            GameObject obj = Instantiate(orgObject, testpos, Quaternion.identity);
            obj.GetComponent<Teleport>().pos = pos;
        }
        
        GameObject obj1 = Instantiate(orgObject, new Vector3(8.5f,0,8.5f), Quaternion.identity);
        GameObject obj2 = Instantiate(orgObject, new Vector3(2.5f, 0, 2.5f), Quaternion.identity);

    }

    List<int> list = new List<int>();

    void Start()
    {
        teleportSystem();
        teleporters = new List<Teleport>();
        main_teleport = this;
    }

    
    void Update()
    {
        
    }

    public  void Ontp()
    {
        TPUI.SetActive(false);
        mytarget.GetComponent<Teleport>().tp();
    }
    public void testtarget(Transform target)
    {
        mytarget = target.gameObject;
    }
}
