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

    
    private void CharacterUpdate(int idx)
    {
        ChosenCharacters(idx);
    }


    public void ChooseCharacters(int idx)
    {
        chosenDB.characterList.Add(charDB.characterList[idx]);
        chosenDB.characterList = chosenDB.characterList.Distinct().ToList();
    }

    public void ChosenCharacters(int idx)
    {
        switch (idx)
        {
            case 0:
                ChosenChar1.SetActive(true);
                break;
            case 1:
                ChosenChar2.SetActive(true);
                break;
            case 2:
                ChosenChar3.SetActive(true);
                break;
            case 3:
                ChosenChar4.SetActive(true);
                break;
            default:
                break;
        }
    }
}
