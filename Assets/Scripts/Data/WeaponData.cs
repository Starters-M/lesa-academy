using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Custom Stuff/Weapon", order = 1)]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    string nameOfWeapon;
    [SerializeField]
    int damage;
    [SerializeField]
    TypeAttack type;

    public string NameOfWeapon
    {
        get { return nameOfWeapon; }
        //set { nameOfWeapon = value; }
    }

    public int Damage
    {
        get { return damage; }
    }

    public TypeAttack Type
    {
        get { return type; }
    }
}

public enum TypeAttack
{
    Slash,
    Crush,
    Stab,
    None
}