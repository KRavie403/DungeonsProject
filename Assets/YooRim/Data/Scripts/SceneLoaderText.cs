using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SceneLoaderText : Singleton<SceneLoaderText>
{
    public GameObject MapImage;

    public TMP_Text mapText;
    public TMP_Text loadingText;
    string[] textList = new string[3];
    string[] map = new string[2];
    int rnd, i;

    // Start is called before the first frame update
    void Start()
    {
        textList[0] = "�ٶ��� ������ ������ �츮�� �Ҿ���� ������ ����� �Ʒ��ϰ� ���ø���.";
        textList[1] = "�׳��� ������ �츮�� ������� ���Ѵ�. ���� �� ������ ������ ������ �� ���̿���.";
        textList[2] = "������ �뷡�� ����, ������ �뷡�� ���� , ������ �뷡�� ����.";

        map[0] = "Map1";
        map[1] = "Map2";

        Setting(i);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setting(int i)
    {
        string path = "Database\\CharacterStatus\\" + map[i];
        Sprite loadedSprite = Resources.Load<Sprite>(path);
        if (path != null)
        {
            // �̹��� ��������Ʈ�� ���ο� ��������Ʈ�� ��ü
            MapImage.gameObject.GetComponentInChildren<Image>().sprite = loadedSprite;
        }
        else
        {
            Debug.LogWarning("��������Ʈ�� �ε��ϴ� �� �����߽��ϴ�.");
        }

        mapText.text = i == 1 ? "�ǳ���" : "z";
        rnd = Random.Range(0, 3);
        loadingText.text = textList[rnd];
    }
}
