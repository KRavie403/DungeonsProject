using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SceneLoaderText : Singleton<SceneLoaderText>
{
    public Image MapImage;
    public Sprite Map1;
    public Sprite Map2;
    public Sprite Title;

    public TMP_Text mapText;
    public TMP_Text loadingText;
    string[] textList = new string[3];
    int rnd;

    // Start is called before the first frame update
    void Start()
    {
        textList[0] = "�ٶ��� ������ ������ �츮�� �Ҿ���� ������ ����� �Ʒ��ϰ� ���ø���.";
        textList[1] = "�׳��� ������ �츮�� ������� ���Ѵ�. ���� �� ������ ������ ������ �� ���̿���.";
        textList[2] = "������ �뷡�� ����, ������ �뷡�� ���� , ������ �뷡�� ����.";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setting(int i)
    {
        MapImage.sprite = i == 1 ? Map1 : i == 2 ? Map2 : Title;
        mapText.text = i == 1 ? "�ǳ���" : i == 2 ? "����� �̱�" : "";
        rnd = Random.Range(0, 3);
        loadingText.text = textList[rnd];
    }
}
