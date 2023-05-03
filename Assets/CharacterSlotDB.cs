using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterSlotDB : MonoBehaviour
{
    public CharacterDB chosenDB;
    public CharacterDB charDB;       

    static public CharacterSlotDB cdb = null;

    //ChosenCharList
    public GameObject ChosenChar1;
    public GameObject ChosenChar2;
    public GameObject ChosenChar3;
    public GameObject ChosenChar4;

    private void Start()
    {
        cdb = this;
    }

    private void Update()
    {
    }


    public void ChooseCharacters(int idx)
    {
        chosenDB.characterList.Add(charDB.characterList[idx]);
        chosenDB.characterList = chosenDB.characterList.Distinct().ToList();

        if (!ChosenChar1.activeSelf) ChosenChar1.SetActive(true);
        else if (!ChosenChar2.activeSelf) ChosenChar2.SetActive(true);
        else if (!ChosenChar3.activeSelf) ChosenChar3.SetActive(true);
        else ChosenChar4.SetActive(true);
    }

    public void DeleteCharacters(int idx)
    {
        if (ChosenChar1.activeSelf)
        {
            ChosenChar1.SetActive(false);
            chosenDB.characterList.Remove(charDB.characterList[idx]);
        }
        else if (ChosenChar2.activeSelf)
        {
            ChosenChar2.SetActive(false);
            chosenDB.characterList.Remove(charDB.characterList[idx]);
        }
        else if (ChosenChar3.activeSelf)
        {
            ChosenChar3.SetActive(false);
            chosenDB.characterList.Remove(charDB.characterList[idx]);
        }
        else
        {
            ChosenChar4.SetActive(false);
            chosenDB.characterList.Remove(charDB.characterList[idx]);
        }
    }
}
