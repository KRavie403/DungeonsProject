using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/Skillset", order = 1)]
public class SkillSet : ScriptableObject
{
    public enum SkillType  {Moveable, Targeting, Directing }
    [SerializeField]
    private SkillType _type;
    public SkillType myType { get { return _type; } }

    [SerializeField]
    private Sprite _sprite;
    public Sprite MySprite { get { return _sprite; } }

    [SerializeField]
    private int _damage;
    public int Damage{ get { return _damage; } }

    [SerializeField]
    private GameObject _effect;
    public GameObject Effect { get { return _effect; } }

    [SerializeField]
    public List<Vector2Int> AttackIndex;

    [SerializeField]
    private int _distance = 5;
    public int Distance { get { return _distance; } }

    [SerializeField]
    private int _cost = 1;
    public int Cost { get { return _cost; } }
}
