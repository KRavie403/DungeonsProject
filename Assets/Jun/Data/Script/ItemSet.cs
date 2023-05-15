using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/Itemset", order = 5)]

public class ItemSet : ScriptableObject
{
    public enum ItemType { Power, Armor, Potion, Accessories }
    [SerializeField]
    private ItemType _type;
    public ItemType myType { get { return _type; } }

    [SerializeField]
    private float _power;
    public float power { get { return _power; } }

    [SerializeField]
    private Sprite _sprite;
    public Sprite MySprite { get { return _sprite; } }


}
