using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/Itemset", order = 5)]

public class ItemSet : ScriptableObject
{
    public enum ItemType { Power, Armor, Potion, Accessories }
    public enum ItemGrade { Rare, Legendary, Epic, Myth }
    public enum EquipmentType { None, Weapon, Helmet, Armor, Boots , EarRing , Necklace , Bracelet,Ring }
    [SerializeField]
    private ItemType _type;
    public ItemType myType { get { return _type; } }

    [SerializeField]
    private ItemGrade _Grade;
    public ItemGrade myGrade { get { return _Grade; } set {  } }
    [SerializeField]
    private EquipmentType _EquipmentType;
    public EquipmentType myEquipmentType { get { return _EquipmentType; } set { } }

    [SerializeField]
    private float _power;
    public float power { get { return _power; } }

    [SerializeField]
    private Sprite _sprite;
    public Sprite MySprite { get { return _sprite; } }


}
