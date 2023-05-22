using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CharcterSet : MonoBehaviour
{
    public GameObject CharacterSet;
    public GameObject ResetButton;
    public GameObject ApplyButton;

    public GameObject ProfileImage;
    public Button[] skillButtonList;

    public Character chosenDB;
    public SkillSetDB chosenSkillDB;
    public SkillSetDB tempChosenSkillDB;

    public TMP_Text countSkills;
    public TMP_Text charName;

    private Animator ani;

    int charIdx = 0;
    int count = 0;
    string[] file = new string[7];
    string[] profileImg = new string[7];

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        tempChosenSkillDB.List.Clear();
        string path = "Database\\CharacterStatus\\" + file[charIdx];
        UnityEngine.Object obj = Resources.Load(path);
        chosenDB = obj as Character;
    }
    void Setting()
    {
        foreach (var item in skillButtonList)
        {
            item.interactable = true;
        }
        count = 0;
        Debug.Log("CHARIDX" + charIdx);

        charName.text = chosenDB.Name;
        ProfileImage.gameObject.GetComponentInChildren<Image>().sprite = chosenDB.Sprite;
        for (int i = 0; i < chosenDB.Skill.List.Count; i++)
        {
            skillButtonList[i].gameObject.GetComponentInChildren<Image>().sprite = chosenDB.Skill.List[i].MySprite;
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
                file[0] = "Berserker";
                break;
            case 1:
                file[1] = "Paladin";
                break;
            case 2:
                file[2] = "Guardian";
                break;
            case 3:
                file[3] = "Ranger";
                break;
            case 4:
                file[4] = "Sorceress";
                break;
            case 5:
                file[5] = "Assassin";
                break;
            case 6:
                file[6] = "Priest";
                break;
            default:
                break;
        }
        Setting();
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
            foreach(var temp in tempChosenSkillDB.List)
            {
                chosenSkillDB.List.Add(temp);
            }
            CharacterSlotDB.cdb.ChosenCharacterButtonsActive(charIdx);
            CharacterSlotDB.cdb.DeactiveCharacters(charIdx);
        }
        else
        {
            CountSkillSetting();
        }
    }

    public void SetSkillList(int idx)
    {
        if(chosenSkillDB.List.Count < 16 && tempChosenSkillDB.List.Count < 4)
        {
            tempChosenSkillDB.List.Add(chosenDB.Skill.List[idx]);
            tempChosenSkillDB.List = chosenDB.Skill.List.Distinct().ToList();
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
