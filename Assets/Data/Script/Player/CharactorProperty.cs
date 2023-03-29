using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorProperty : MonoBehaviour
{
    public int ActionPoint = 8;
    public float AttackPower = 10.0f;
    public float DeffencePower = 10.0f;
    public float MaxHP = 100.0f;
    float _curHP = -100.0f;

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
}
