using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_obj_System : MonoBehaviour
{
    public static Create_obj_System main_obj_create = null;

    public GameObject TeleportObj;
    public GameObject ChestObj;
    public GameObject TPUI;
    public GameObject ChestUI;
    public GameObject SecretObj;
    Vector2Int pos = Vector2Int.zero;
    Vector3 mypos = Vector3.zero;
    public List<Teleport> teleporters;
    public GameObject myTPtarget = null;
    public GameObject myChesttarget = null;
    int x = 0, y = 0;
    public void random(int rd)
    {

        switch (rd)
        {
            case 1: x = 80; y = 80; break;
            case 2: x = 76; y = 44; break;
            case 3: x = 58; y = 50; break;
            case 4: x = 42; y = 89; break;
            case 5: x = 18; y = 85; break;
            case 6: x = 24; y = 10; break;
            case 7: x = 26; y = 33; break;
            case 8: x = 15; y = 62; break;
            case 9: x = 45; y = 63; break;
            case 10: x = 70; y = 15; break;
            case 11: x = 51; y = 7; break;
            case 12: x = 92; y = 34; break;
            case 13: x = 4; y = 95; break;
            case 14: x = 3; y = 35; break;
            case 15: x = 61; y = 94; break;
        }

        Vector2Int my_Pos = new Vector2Int(x, y);

        float half = GameManager.Inst.scale * 0.5f;
        mypos = new Vector3((float)my_Pos.x + half, 0, (float)my_Pos.y + half);

        pos = new Vector2Int(x, y);
    }
    void teleportSystem()
    {
        int rd;

        for (int j = 0; j < 15;) //10���ݺ�
        {
            rd = Random.Range(1, 16); //1-10 �������� �ֱ�
            if (list.Contains(rd)) //list���� �ߺ��ȼ��ڰ��ִٸ�
            {
                rd = Random.Range(1, 16);
            }
            else //list���� �ߺ��ȼ��ڰ����ٸ� list�� ���ڳְ� j++
            {
                list.Add(rd); //0���ε������� 9���� 1-11�������� �ֱ�       
                j++;
            }
        }

        for (int i = 0; i < 5; ++i)
        {
            random(list[i]);
            GameObject obj1 = Instantiate(TeleportObj, mypos, Quaternion.identity);
            obj1.GetComponent<Teleport>().pos = pos;
        }
        for (int i = 5; i < 15; ++i)
        {
            random(list[i]);
            GameObject obj2 = Instantiate(ChestObj, mypos, Quaternion.identity);
            obj2.GetComponent<Chest>().pos = pos;
        }
        
        GameObject obj3 = Instantiate(ChestObj, new Vector3(8.5f,0,8.5f), Quaternion.identity);
        GameObject obj4 = Instantiate(TeleportObj, new Vector3(5.5f, 0, 5.5f), Quaternion.identity);
        GameObject obj5 = Instantiate(SecretObj, new Vector3(16.5f, 0, 3.5f), Quaternion.identity);
    }

    List<int> list = new List<int>();

    void Start()
    {
        teleportSystem();
        //teleporters = new List<Teleport>();
        main_obj_create = this;
    }

    
    void Update()
    {
        
    }

    public  void Ontp()
    {
        TPUI.SetActive(false);
        myTPtarget.GetComponent<Teleport>().tp();
    }
    public void TPtarget(Transform target)
    {
        myTPtarget = target.GetComponent<TileState>().my_target.gameObject;
    }
    public void OnChest()
    {
        ChestUI.SetActive(false);
        myChesttarget.GetComponent<Chest>().chest();
    }
    public void Chesttarget(Transform target)
    {
        myChesttarget = target.GetComponent<TileState>().my_target.gameObject;
        myChesttarget.GetComponent<Animator>().SetTrigger("OPEN");
    }
}
