using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class CharactorProperty : MonoBehaviour
{
    [SerializeField]
    public float Aggro = 5;
    public List<SkillSet> skilList;
    public Sprite my_Sprite;
    public OB_TYPES myType = OB_TYPES.NONE;
    public Vector2Int my_Pos;
    public float MaxHP = 100.0f;
    public float _curHP = -100.0f;
    public float my_Size = 1.0f;
    public float _mySize = 1.0f;
    public float MoveSpeed = 1.0f;
    public float RotSpeed = 35.0f;

    public float OrgAttackPower = 10.0f;
    public float AttackPower = 10.0f;
    public float OrgDeffencePower = 10.0f;
    public float DeffencePower = 10.0f;
    public float OrgSpeed = 1.0f; //전투 우선도 
    public float Speed = 1.0f; //전투 우선도 

    public List<StatModifire> AD_stat = new List<StatModifire>();

    protected int ActionPoint = 10;
    public int _curActionPoint;
    protected int curAP {
        get
        {
            if (_curActionPoint <= 0) _curActionPoint = ActionPoint;

            return _curActionPoint;
        }
        set => _curActionPoint = value;
    }


    protected float curHP
    {
        get
        {
            if (_curHP < 0.0f) _curHP = MaxHP;

            return _curHP;
        }
        set => _curHP = Mathf.Clamp(value, 0.0f, MaxHP);
    }
    Animator _anim = null;

    protected Animator myAnim
    {
        get
        {
            if (_anim == null)
            {
                _anim = GetComponent<Animator>();
                if (_anim == null)
                {
                    _anim = GetComponentInChildren<Animator>();
                }
            }
            return _anim;
        }
    }

    protected static MapManager GetMMInst()
    {
        return MapManager.Inst;
    }
    protected static GameManager GetGMInst()
    {
        return GameManager.Inst;
    }

}
