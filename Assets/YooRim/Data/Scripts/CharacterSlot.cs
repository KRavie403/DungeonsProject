using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSlot : MonoBehaviour
{
    public int idx;
    public void SendIndex()
    {
        CharacterSlotDB.cdb.ChooseCharacters(idx);
    }
}
