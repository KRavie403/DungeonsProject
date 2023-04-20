using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CharacterStatData", menuName = "ScriptableObjects/Character", order = 3)]
public class CharacterSet : ScriptableObject
{
    [SerializeField]
    private string _name;
    public string Name{ get { return _name; } }

    [SerializeField]
    private int _damage;
    public int Damage{ get { return _damage; } }

    [SerializeField]
    private Sprite _sprite;
    public Sprite Sprite { get { return _sprite; } }

    [SerializeField]
    private JOB_CLASS _class;
    public JOB_CLASS JobClass { get { return _class; } }

    [SerializeField]
    private float _maxHP;
    public float MaxHP { get { return _maxHP; } }


    [SerializeField]
    private float _attackPower;
    public float AttackPower { get { return _attackPower; } }
    [SerializeField]
    private float _deffencePower;
    public float DeffencePower { get { return _deffencePower; } }

    [SerializeField]
    private float _speed;
    public float Speed { get { return _speed; } }

    [SerializeField]
    private int _actionPoint;
    public int ActionPoint { get { return _actionPoint; 
        } }

}
