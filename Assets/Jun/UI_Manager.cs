using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Manager : MonoBehaviour
{
    public GameObject InGameUI;
    public Transform TurnSystem;
    public GameObject char_Frame;
    public SkillSetDB currentSkillSet;
    public GameObject start_button;
    public List<GameObject> skillSlots;
    public TMPro.TextContainer currentHP;
    public Image currentActionPoint;
    public int skill_Count = 0;

    public void StateUpdate(int p)
    {
        currentActionPoint.fillAmount = GameManager.GM.Players[p].GetComponent<Player>().CheckAP() / 10.0f;


    }
    public void AddPlayer(Sprite _spt)
    {
        GameObject obj = Instantiate(char_Frame);
        obj.GetComponent<Image>().sprite = _spt;
        obj.transform.SetParent(TurnSystem);
    }
    public void AddSkills(SkillSet _set)
    {
        currentSkillSet.SkillList.Add(_set);
        skillSlots[skill_Count].GetComponent<Image>().sprite = _set.MySprite;
        skillSlots[skill_Count++].GetComponent<SkillSlot>()._my_skill = _set;
    }


}
