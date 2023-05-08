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

    static public CharacterSlotDB cdb = null;

    public GameObject CharacterSet;
    public TMP_Text CharacterSkillSetText;

    //ChosenCharList
    public GameObject[] ChosenCharList;

    //text
    public TMP_Text[] chosenCharTextList;

    public Button skill1;

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
        chosenDB.characterList.Add(charDB.characterList[idx]);
        chosenDB.characterList = chosenDB.characterList.Distinct().ToList();

        //버튼활성화 비활성화 && 텍스트 수정
        if (!ChosenCharList[0].activeSelf)
        {
            ChosenCharList[0].SetActive(true);
            chosenCharTextList[0].text = charDB.characterList[idx].Name;
        }
        else if (!ChosenCharList[1].activeSelf)
        {
            ChosenCharList[1].SetActive(true);
            chosenCharTextList[1].text = charDB.characterList[idx].Name;
        }
        else if (!ChosenCharList[2].activeSelf)
        {
            ChosenCharList[2].SetActive(true);
            chosenCharTextList[2].text = charDB.characterList[idx].Name;
        }
        else
        {
            ChosenCharList[3].SetActive(true);
            chosenCharTextList[3].text = charDB.characterList[idx].Name;
        }
    }

    public void DeleteCharacters(int idx)
    {
        if (ChosenCharList[0].activeSelf)
        {
            ChosenCharList[0].SetActive(false);
            chosenDB.characterList.Remove(charDB.characterList[idx]);
        }
        else if (ChosenCharList[1].activeSelf)
        {
            ChosenCharList[1].SetActive(false);
            chosenDB.characterList.Remove(charDB.characterList[idx]);
        }
        else if (ChosenCharList[2].activeSelf)
        {
            ChosenCharList[2].SetActive(false);
            chosenDB.characterList.Remove(charDB.characterList[idx]);
        }
        else
        {
            ChosenCharList[3].SetActive(false);
            chosenDB.characterList.Remove(charDB.characterList[idx]);
        }
    }

    public void CharacterSelection(int idx) //Settings
    {
        CharacterSet.SetActive(true);
        CharacterSkillSetText.text = charDB.characterList[idx].Name;
    }

    public void ClickSkills()
    {
        skill1.interactable = false;
    }


}
