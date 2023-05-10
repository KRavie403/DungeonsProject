using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillDataBase", menuName = "ScriptableObjects/SkillsetDB", order = 2)]

public class SkillSetDB : ScriptableObject
{
    [SerializeField]
    public List<SkillSet> SkillList = new List<SkillSet>(8);
}
