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

    //ChosenCharList
    public GameObject[] ChosenCharList;

    //text
    public TMP_Text[] chosenCharTextList;

    private void Start()
    {
        cdb = this;

        for(int i = 0; i<4; i++)
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

        //버튼활성화 비활성화
        if (!ChosenCharList[0].activeSelf) ChosenCharList[0].SetActive(true);
        else if (!ChosenCharList[1].activeSelf) ChosenCharList[1].SetActive(true);
        else if (!ChosenCharList[2].activeSelf) ChosenCharList[2].SetActive(true);
        else ChosenCharList[3].SetActive(true);

        //텍스트 수정
        int number = int.Parse(chosenCharTextList[0].text) + 1;
        chosenCharTextList[0].text = number.ToString();

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
}
