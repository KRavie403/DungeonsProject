using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CharcterSet : MonoBehaviour
{
    public GameObject CharacterSet;
    public GameObject CharacterSkill;
    public GameObject ResetButton;
    public GameObject ApplyButton;

    public Button[] skillButtonList;

    public SkillSetDB chosenSkillDB;
    public SkillSetDB skillDB;

    public TMP_Text countSkills;

    private Animator ani;

    int charIdx = 0;
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
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
        if(count == 4)
        {
            this.gameObject.SetActive(false);
            CharacterSlotDB.cdb.ChosenCharacterButtonsActive(charIdx);
            CharacterSlotDB.cdb.DeactiveCharacters(charIdx);
        }
        else
        {
            CountSkillSetting();
        }
    }

    public void SetSkillList()
    {
        if(chosenSkillDB.skillList.Count < 16)
        {
            chosenSkillDB.skillList.Add(skillDB.skillList[charIdx]);
            chosenSkillDB.skillList = skillDB.skillList.Distinct().ToList();
        }
    }

    public void ActiveSkills(int idx)
    {
        if (!skillButtonList[idx].interactable)
        {
            skillButtonList[idx].interactable = true;
            count--;
            countSkills.text = count.ToString();
            Debug.Log(count);
        }
    }

    public void DeactiveSkills(int idx)
    {
        if (skillButtonList[idx].interactable)
        {
            if (count < 4)
            {
                skillButtonList[idx].interactable = false;
                count++;
                countSkills.text = count.ToString();
                Debug.Log(count);
            }
        }
    }
    public void CountSkillSetting()
    {
        ani.SetTrigger("Notify");
    }
}
