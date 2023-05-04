using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButtonSlot : MonoBehaviour
{
    private Animator ani;
    public Button[] characterButtonList;

    private int count = 0;

    public void SendIndex()
    {
        ani = GetComponent<Animator>();
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

        if (ani.GetBool("isChange"))
        {
            ani.SetBool("isChange", false);
        }
        else
            ani.SetBool("isChange", true);
    }
}
