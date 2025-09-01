using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ally", menuName = "Custom Stuff/Ally", order = 3)]
public class AlliesData : ScriptableObject
{
    [Header("Ally Info")]
    [SerializeField] string nameOfAlly;
    [SerializeField] int health;
    [SerializeField] WeaponData weapon;
    [Header("Stats")]
    [SerializeField] CharacterStats stats;
    [SerializeField] Ability[] abilities;
    //[SerializeField] TypeWeapon[] disabilities;

    public string NameOfAlly
    {
        get { return nameOfAlly; }
    }
    public int Health
    {
        get { return health; }
    }
    public WeaponData Weapon
    {
        get { return weapon; }
    }
    public CharacterStats Stats
    {
        get { return stats; }
        set { stats = value; }
    }
    public Ability[] Abilities
    {
        get { return abilities; }
    }
    /*public TypeWeapon[] Disabilities
    {
        get { return disabilities; }
    }*/
}
