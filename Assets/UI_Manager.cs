using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Manager : MonoBehaviour
{
    public GameObject InGameUI;
    public Transform TurnSystem;
    public GameObject char_Frame;
    public GameObject start_button;
    public void AddPlayer(Sprite _spt)
    {
        GameObject obj = Instantiate(char_Frame);
        obj.GetComponent<Image>().sprite = _spt;
        obj.transform.SetParent(TurnSystem);
    }

    
}
