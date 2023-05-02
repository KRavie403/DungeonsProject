using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSlotDB : MonoBehaviour
{
    public CharacterDB chosenDB;
    public CharacterDB charDB;       

    static public CharacterSlotDB cdb = null;

    private void Start()
    {
        cdb = this;
    }

    public void ChooseCharacters(int idx)
    {
        chosenDB.characterList.Add(charDB.characterList[idx]);
    }
}
