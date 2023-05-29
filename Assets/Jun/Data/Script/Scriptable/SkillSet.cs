using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public struct ExtendStatus
{
    public bool Unbreakable;
    public bool Postive;
    public int lifeTime;
    public float AttackPower;
    public float DeffencePower;
    public int Speed;  
}

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/Skillset", order = 1)]

public class SkillSet : ScriptableObject
{
    [SerializeField]
    private string _tipText;
    public string myTipText { get { return _tipText; } }

    [SerializeField]
    private string _tipText2;
    public string myTipText2 { get { return _tipText2; } }

    [SerializeField]
    private Sprite _tipSprite;
    public Sprite myTipSprite { get { return _tipSprite; } }



    public enum SkillType  {Moveable, Targeting, Directing }
    public enum EffectType  {StatEffect, SpecialEffect, DamageEffcet}
    [SerializeField]
    private SkillType _type;
    public SkillType myType { get { return _type; } }

    [SerializeField]
    private EffectType _Etype;
    public EffectType myEType { get { return _Etype; } }

    [SerializeField]
    private Sprite _sprite;
    public Sprite MySprite { get { return _sprite; } }

    [SerializeField]
    private int _damage;
    public int Damage{ get { return _damage; } }

    [SerializeField]
    private int _LifeTime;
    public int life_time { get { return _LifeTime; } }

    public ExtendStatus _exStatus;


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

    [SerializeField]
    public AudioClip myAudioClip;
}
