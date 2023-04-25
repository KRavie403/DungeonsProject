using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSlot : MonoBehaviour
{
    public SkillSet _my_skill;

    public void CastingSkill()
    {
        GameManager.GM.Players[GameManager.GM.currentPlayer].GetComponent<Player>().OnSkillCast(_my_skill);
    }
}
