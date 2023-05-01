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
        DontDestroyOnLoad(gameObject); //scene 전환되어도 오브젝트 파괴되지 않음
    }

    public Character currentCharacter; //캐릭터를 선택하면 그걸로 다른 scene에서 그 캐릭터 쓰는 거 
}
