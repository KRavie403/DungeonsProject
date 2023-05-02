using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterList
{
    None, T1, T2, T3, D1, D2, D3, H1
}

public class ChooseCharacter : MonoBehaviour/*, IBeginDragHandler, IDragHandler, IEndDragHandler*/
{
    public static ChooseCharacter instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;
        DontDestroyOnLoad(gameObject); //scene ��ȯ�Ǿ ������Ʈ �ı����� ����
    }

    public Character currentCharacter; //ĳ���͸� �����ϸ� �װɷ� �ٸ� scene���� �� ĳ���� ���� �� 
}
