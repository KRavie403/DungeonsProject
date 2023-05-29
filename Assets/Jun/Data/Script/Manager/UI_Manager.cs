using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;


public struct SSkill
{
    public SkillSet.SkillType _type;
    public Sprite _sprite;
    public int _damage;
    public GameObject _effect;
    public List<Vector2Int> _AttackIndex;
    public int _distance;
}

public class UI_Manager : Singleton<UI_Manager>
{
    public GameObject InGameUI;
    public GameObject start_button;
    public GameObject char_Frame;
    public GameObject TPUI;
    public GameObject ChestUI;
    public GameObject MonsterUI;
    public CharStatus CharStatusUI;

    public Transform TurnSystem;
    public List<SSkill> currentSkillSet;
    public List<GameObject> skillSlots;
    //public TMPro.TextContainer currentHP;
    public Image currentActionPoint;
    
    public int skill_Count = 0;

    public void Start()
    {
        currentSkillSet = new List<SSkill>();
    }
    public void ShowStatusInven()
    {
        StateUpdate(GameManager.Inst.curCharacter);
        if (CharStatusUI.gameObject.activeSelf == false)
            CharStatusUI.gameObject.SetActive(true);
        else    CharStatusUI.gameObject.SetActive(false);
    }
    public void StateUpdate(int p)
    {
        currentActionPoint.fillAmount = GameManager.Inst.characters[p].GetComponent<CharactorMovement>().CheckAP() / GameManager.Inst.characters[p].GetComponent<CharactorProperty>().ActionPoint;
    //    CharStatusUI.HP.text = $"HP : {GameManager.Inst.characters[p].GetComponent<CharactorMovement>()._curHP} / {GameManager.Inst.characters[p].GetComponent<CharactorMovement>().MaxHP}";
    //    CharStatusUI.ATT.text = $"ATT : {GameManager.Inst.characters[p].GetComponent<CharactorMovement>().AttackPower}";
    //    CharStatusUI.DFF.text = $"DFF : {GameManager.Inst.characters[p].GetComponent<CharactorMovement>().DeffencePower}";
    //    CharStatusUI.SPD.text = $"SPD : {GameManager.Inst.characters[p].GetComponent<CharactorMovement>().Speed}";
    //    CharStatusUI.name.text = $"{GameManager.Inst.characters[p].GetComponent<CharactorMovement>().name}";
    //    for(int i=0;i<CharStatusUI.skills.Length;i++)
    //        CharStatusUI.skills[i].GetComponent<Image>().sprite = GameManager.Inst.characters[p].GetComponent<CharactorProperty>().skilList[i].MySprite;
    //
    }
    public void AddPlayer(Sprite _spt)
    {
        GameObject obj = Instantiate(char_Frame);
        obj.GetComponent<Image>().sprite = _spt;
        obj.transform.SetParent(TurnSystem);
    }
    public void AddSkills(SkillSet _set)
    {
        SSkill skill = new SSkill();
        skill._sprite = _set.MySprite;
        skill._effect = _set.Effect;
        skill._damage = _set.Damage;
        skill._type = _set.myType;
        skill._AttackIndex = _set.AttackIndex;
        skill._distance = _set.Distance;
        if (skill_Count < skillSlots.Count)
        {
            skillSlots[skill_Count].GetComponent<Image>().sprite = _set.MySprite;
            skillSlots[skill_Count].GetComponentInChildren<SkillSlot>()._my_skill = _set;
        }
        currentSkillSet.Add(skill);
        skill_Count++;
    }
    public void GameStart()
    {
        TPUI = GameObject.Find("Canvas").transform.Find("InGameUIs").transform.Find("TPUI").gameObject;
        ChestUI = GameObject.Find("Canvas").transform.Find("InGameUIs").transform.Find("ChestUI").gameObject;
        InGameUI.SetActive(true);
        start_button.SetActive(false);

    }

    
}
