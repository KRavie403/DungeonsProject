using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/Itemset", order = 5)]

public class ItemSet : ScriptableObject
{
    public enum SkillType { Buff, MoveAttack, DirectAttack, Heal }
    [SerializeField]
    private SkillType _type;
    public SkillType myType { get { return _type; } }

    [SerializeField]
    private int _damage;
    public int Damage { get { return _damage; } }

    [SerializeField]
    private GameObject _effect;
    public GameObject Effect { get { return _effect; } }

    [SerializeField]
    public List<Vector2Int> AttackIndex;

}
