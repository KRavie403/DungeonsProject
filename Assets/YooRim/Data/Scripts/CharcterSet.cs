using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CharcterSet : MonoBehaviour
{
    public GameObject CharacterSet;
    public GameObject ResetButton;
    public GameObject ApplyButton;

    public GameObject ProfileImage;
    public Button[] skillButtonList;

    public SkillSetDB chosenSkillDB;
    public SkillSetDB skillDB;

    public TMP_Text countSkills;

    private Animator ani;

    int charIdx = 0;
    int count = 0;
    string[] file = new string[7];

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        skillDB = Resources.Load<SkillSetDB>($"Assets/Jun/Data/Resources/Database/CharSkill/{file[charIdx]}");

        ProfileImage.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Assets/YooRim/Data/Resources/Profiles/Assassin.png");
        for (int i = 0; i < skillDB.List.Count; i++)
        {
            skillButtonList[i].gameObject.GetComponentInChildren<Image>().sprite = skillDB.List[i].MySprite;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SendIdx(int idx)
    {
        charIdx = idx;
        switch (idx)
        {
            case 0:
                file[0] = "BerserkerSkillDB";
                break;
            case 1:
                file[1] = "PaladinSkillDB";
                break;
            case 2:
                file[2] = "GuardianSkillDB";
                break;
            case 3:
                file[3] = "RangerSkillDB";
                break;
            case 4:
                file[4] = "SorceressSkillDB";
                break;
            case 5:
                file[5] = "AssassinSkillDB";
                break;
            case 6:
                file[6] = "PriestSkillDB";
                break;
            default:
                break;
        }
    }
    public void SkillSetting()
    {

    }

    public void ClickResetButton() //Settings
    {
    }

    public void ClickApplyButton() //Settings
    {
        if (count == 4)
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
        if (chosenSkillDB.List.Count < 16)
        {
            chosenSkillDB.List.Add(skillDB.List[charIdx]);
            chosenSkillDB.List = skillDB.List.Distinct().ToList();
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
