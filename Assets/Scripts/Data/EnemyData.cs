using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Custom Stuff/Enemy", order = 2)]
public class EnemyData : ScriptableObject
{
    [Header("Enemy Info")]
    [SerializeField] string nameOfEnemy;
    [SerializeField] int health;
    [SerializeField] int damageWeapon;
    [Header("Stats")]
    [SerializeField] CharacterStats stats;
    [SerializeField] Ability abilities;
    [SerializeField] TypeAttack disabilities;
    [Header("Reward")]
    [SerializeField] WeaponData reward;

    public string NameOfEnemy
    {
        get { return nameOfEnemy; }
    }
    public int Health
    {
        get { return health; }
    }
    public int DamageWeapon
    {
        get { return damageWeapon; }
    }
    public CharacterStats Stats
    {
        get { return stats; }
    }
    public Ability Abilities
    {
        get { return abilities; }
    }
    public TypeAttack Disabilities
    {
        get { return disabilities; }
    }
    public WeaponData Reward
    {
        get { return reward; }
    }

}
