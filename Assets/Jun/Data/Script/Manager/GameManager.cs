using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;




public class GameManager : Singleton<GameManager>
{
    public List<GameObject> characters;
    public FollowCamera Main_Cam;
    public int curCharacter =0;
    [SerializeField]
    CharacterDB selectedOrgChars;
    [SerializeField]
    SkillSetDB selectedOrgSkills;

    [SerializeField]
    GameObject boss;

    private void Awake()
    {
        characters = new List<GameObject>();
    }
    
    public void GameStart()
    {
        GameObject obj;
        foreach (var chosen in selectedOrgChars.characterList)
        {
            obj = Instantiate(chosen.Preb);
            obj.GetComponent<Player>().PlayerSetting(chosen);

            characters.Add(obj);
        }

        obj = Instantiate(boss);
        obj.GetComponent<BossMonster>().PlayerSetting();
        characters.Add(obj);

        UI_Manager.Inst.GameStart();
        UI_Manager.Inst.StateUpdate(curCharacter);
        Main_Cam.enabled = true;
        Main_Cam.SetCam(0);

        if (characters != null)
        {
            characters[0].GetComponent<Player>().ChangeState(STATE.ACTION);
            CurrentSkill();
        }


    }
    
    //Player
    public void OnMove()
    {
        characters[curCharacter].GetComponent<Player>().OnMove();
    }
    public void OnInteraction()
    {
        characters[curCharacter].GetComponent<Player>().OnInteract();
    }
    public void ChangeTurn()
    {
        characters[curCharacter].GetComponent<CharactorMovement>().ChangeState(STATE.IDLE);
        curCharacter = (++curCharacter) % (characters.Count);
        UI_Manager.Inst.StateUpdate(curCharacter);
        Main_Cam.SetCam(curCharacter);
        characters[curCharacter].GetComponent<CharactorMovement>().ChangeState(STATE.ACTION);

        if (characters[curCharacter].TryGetComponent(out BossMonster boss))
            boss.StartScnarioMachine();

        CurrentSkill();


    }
    private void CurrentSkill()
    {
        if (characters[curCharacter].GetComponent<CharactorMovement>().myType == OB_TYPES.PLAYER)
        {
            if (UI_Manager.Inst.currentSkillSet != null)
                UI_Manager.Inst.currentSkillSet.Clear();
            UI_Manager.Inst.skill_Count = 0;

            for (int i = curCharacter * 4; i < curCharacter * 4 + 4; i++)
                UI_Manager.Inst.AddSkills(selectedOrgSkills.List[i]);
        }
    }
 
    
}
