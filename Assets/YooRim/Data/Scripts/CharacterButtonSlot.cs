using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButtonSlot : MonoBehaviour
{
    public Button[] characterButtonList;

    private int count = 0;

    public void Start()
    {
    }

    public void DeactiveCharacters(int idx)
    {
        if (!characterButtonList[idx].interactable)
        {
            characterButtonList[idx].interactable = true;
            count--;
        }
        else
        {
            if (count < 4)
            {
                characterButtonList[idx].interactable = false;
                count++;
            }
        }
    }
}
