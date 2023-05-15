using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharcterSet : MonoBehaviour
{
    public GameObject CharacterSet;
    public GameObject CharacterSkill;
    public GameObject ResetButton;
    public GameObject ApplyButton;


    int charIdx = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SendIdx(int Idx)
    {
        charIdx = Idx;
    }
    public void SkillSetting()
    {

    }

    public void ClickResetButton() //Settings
    {
    }    
    
    public void ClickApplyButton() //Settings
    {
        CharacterSet.SetActive(false);
        CharacterSlotDB.cdb.ChooseCharacters(charIdx);
    }

}
