using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSlot : MonoBehaviour
{
    public int idx;
    int count = 0;
    public void SendIndex()
    {
            CharacterSlotDB.cdb.ChooseCharacters(idx);
            CharacterSlotDB.cdb.CharacterSelection(idx); //��ųâ charName.text����
    }
}
