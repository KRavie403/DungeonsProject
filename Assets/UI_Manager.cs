using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Manager : MonoBehaviour
{
    public GameObject TurnSystemUI;
    public GameObject char_Frame;

    public void AddPlayer(Sprite _spt)
    {
        char_Frame.GetComponent<Image>().sprite = _spt;
        char_Frame.transform.SetParent(TurnSystemUI.transform);
    }

    
}
