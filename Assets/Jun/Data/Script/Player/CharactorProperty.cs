using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JOB_CLASS { DEALER, TANKER, SUPPORTER}

public class CharactorProperty : MonoBehaviour
{
    public int pos_x, pos_y;
    public int ActionPoint = 8;
    public float MoveSpeed = 1.0f;
    public float RotSpeed = 35.0f;
    public float AttackPower = 10.0f;
    public float DeffencePower = 10.0f;
    public float MaxHP = 100.0f;
    public JOB_CLASS jclass;
    public float speed = 1.0f; //전투 우선도 
    float _curHP = -100.0f;

    public GameBoard GameMapManger = null;
        

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
