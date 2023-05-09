using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSlotDB : MonoBehaviour
{
    public CharacterDB chosenDB;
    public CharacterDB charDB;

    public CharcterSet charSet;

    static public CharacterSlotDB cdb = null;
    
    public TMP_Text CharacterSkillSetText;

    //ChosenCharList
    public GameObject[] ChosenCharList;

    //text
    public TMP_Text[] chosenCharTextList;

    public Button[] characterButtonList;

    public Button skill1;

    int count = 0;
    int[] temp = new int[4];

    private void Start()
    {
        cdb = this;
        chosenDB.characterList.Clear();

        for (int i = 0; i<4; i++)
        {
            chosenCharTextList[i].text = "0";
        }
}

    private void Update()
    {
    }


    public void ChooseCharacters(int idx)
    {

        if (chosenDB.characterList.Count < 4)
        {
            chosenDB.characterList.Add(charDB.characterList[idx]);
            chosenDB.characterList = chosenDB.characterList.Distinct().ToList();
        }

        //버튼활성화 비활성화 && 텍스트 수정
        if (!ChosenCharList[0].activeSelf)
        {
            ChosenCharList[0].SetActive(true);
            chosenCharTextList[0].text = charDB.characterList[idx].Name;
            temp[0] = idx;
        }
        else if (!ChosenCharList[1].activeSelf)
        {
            ChosenCharList[1].SetActive(true);
            chosenCharTextList[1].text = charDB.characterList[idx].Name;
            temp[1] = idx;
        }
        else if (!ChosenCharList[2].activeSelf)
        {
            ChosenCharList[2].SetActive(true);
            chosenCharTextList[2].text = charDB.characterList[idx].Name;
            temp[2] = idx;
        }
        else
        {
            ChosenCharList[3].SetActive(true);
            chosenCharTextList[3].text = charDB.characterList[idx].Name;
            temp[3] = idx;
        }
    }

    public void DeleteCharacters(int idx)
    {
        if (ChosenCharList[idx].activeSelf)
        {
            ChosenCharList[idx].SetActive(false);
            charSet.SendIdx(temp[idx]);
            chosenDB.characterList.Remove(charDB.characterList[temp[idx]]);
        }
    }

    public void CharacterSelection(int idx) //Settings
    {
        charSet.gameObject.SetActive(true);        
        CharacterSkillSetText.text = charDB.characterList[temp[idx]].Name;
    }

    public void ClickSkills()
    {
        skill1.interactable = false;
    }

    public void DeactiveCharacters(int idx)
    {
        if (!characterButtonList[temp[idx]].interactable)
        {
            characterButtonList[temp[idx]].interactable = true;
            count--;
            Debug.Log(count);
        }
        else
        {
            if (count < 4)
            {
                characterButtonList[idx].interactable = false;
                count++;
                Debug.Log(count);
            }
        }
    }


}
