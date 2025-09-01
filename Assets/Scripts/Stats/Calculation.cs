using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Calculation
{
    public static int CalcDamageAlly(ClassStats allyStats, Enemy target, out CharacterStats stats)
    {
        int totalDamage = allyStats.stats.strength + allyStats.weapon.Damage;

        switch (allyStats.nameOfAlly)
        {
            case "Warrior":
                
                switch (allyStats.level)
                {
                    case 3:
                        allyStats.stats.strength++;
                        goto case 2;
                    case 2:
                    case 1:
                        if(GameManager.instance.Round % 2 == 1)
                            totalDamage *= 2;
                        break;
                    default:
                        goto case 3;
                }

                break;
            case "Barbarian":

                switch (allyStats.level)
                {
                    case 3:
                        allyStats.stats.stamina++;
                        goto case 2;
                    case 2:
                        goto case 1;
                    case 1:
                        if (GameManager.instance.Round < 4)
                            totalDamage += 2;
                        else
                            totalDamage -= 1;
                            break;
                    default:
                        goto case 3;
                }

                break;
            case "Rogue":

                switch (allyStats.level)
                {
                    case 3:
                        if (GameManager.instance.Round > 1)
                            totalDamage++;
                        if (GameManager.instance.Round > 2)
                            totalDamage++;
                        goto case 2;
                    case 2:
                        allyStats.stats.agility++;
                        goto case 1;
                    case 1:
                        if (allyStats.stats.agility > target.currentStats.agility)
                            totalDamage++;
                        break;
                    default:
                        goto case 3;
                }

                break;
            default:
                break;
        }

        
        stats = allyStats.stats;

        return totalDamage;
    }

    public static int CalcShealdAlly(Ally ally, Enemy target, int attacked)
    {
        int damage = attacked;

        foreach (var a in ally.allStats.abilities)
        {
            if (a == Ability.StoneSkin)
                damage = ally.allStats.stats.stamina;

            if (a == Ability.Sheald && ally.currentStats.strength > target.currentStats.strength)
                damage -= 3;
        }

        return damage;
    }

    public static int CalcDamageEnemy(EnemyStats stats, Ally target, int damage)
    {
        int totalDamage = stats.stats.strength + damage;

        if (stats.abilities == Ability.InvisibleAttack && stats.stats.agility > target.currentStats.agility)
        {
            totalDamage++;
        }

        if (stats.abilities == Ability.FlameBreath && GameManager.instance.Round % 3 == 0)
        {
            totalDamage += 3;
        }

        return totalDamage;
    }

    public static int CalcShealdEnemy(EnemyStats stats, Ally target, int attacked)
    {
        int damage = attacked;

        if (stats.disabilities == TypeAttack.Crush && target.allStats.weapon.Type == TypeAttack.Crush)
            damage *= 2;

        if (stats.abilities == Ability.ImuneToSlashAttack && target.allStats.weapon.Type == TypeAttack.Slash)
            damage = 0;

        if (stats.abilities == Ability.StoneSkin)
            damage = stats.stats.stamina;

        return damage;
    }
}

public enum Ability
{
    ImuneToSlashAttack,
    InvisibleAttack,
    DoubleAttack,
    FlameBreath,
    StoneSkin,
    Poison,
    Sheald,
    Rage,
    Stat,
    None
}
