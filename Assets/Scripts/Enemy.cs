using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public struct EnemyStats
{
    public string nameOfEnemy;
    public int health;
    public int maxHealth;
    public int damageWeapon;

    public CharacterStats stats;
    public Ability abilities;
    public TypeAttack disabilities;
}

public class Enemy : MonoBehaviour
{
    EnemyStats eStats = new();
    public CharacterStats currentStats;

    WeaponData reward;
    SpriteRenderer render;
    Vector2 startPos;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        startPos = transform.position;
    }

    public void Initialisize(EnemyData data)
    {
        eStats.nameOfEnemy = data.NameOfEnemy;
        eStats.maxHealth = eStats.health = data.Health;
        eStats.damageWeapon = data.DamageWeapon;
        currentStats = eStats.stats = data.Stats;
        eStats.abilities = data.Abilities;
        eStats.disabilities = data.Disabilities;
        reward = data.Reward;

        transform.name = eStats.nameOfEnemy;

        GameManager.instance.UpdateStats(eStats);
    }

    public void TakeAction()
    {
        GameObject target = FindFirstObjectByType<Ally>()?.gameObject;

        render.sortingOrder = 1;

        transform?.DOJump(target.transform.position, 3, 1, 1.2f);
        //transform.DOPunchPosition(target.transform.position, 2, 10, 1, false);
        Debug.Log(eStats.nameOfEnemy + " is Attaking");

        StartCoroutine(TakeAct(target?.GetComponent<Ally>()));
    }

    public void TakeDamage(int attacked, Ally ally)
    {
        eStats.health -= Calculation.CalcShealdEnemy(eStats, ally, attacked);

        if (eStats.health <= 0)
        {
            ally.allStats.weapon = reward;
            Destroy(gameObject);
        }
        else
        {
            GameManager.instance.UpdateStats(eStats);
        }
    }

    private void OnDestroy()
    {
        SecondTeam.instance.ClearQueue();
        if (!SecondTeam.instance.FindAllMembers())
        {
            GameManager.instance.G_Over(GameState.Victory);
            Ally[] ally = FindObjectsOfType<Ally>();
            foreach (var a in ally)
            {
                a.SetDefault();
            }
        }
    }

    void Next()
    {
        render.sortingOrder = 0;
        //Debug.Log(nameOfEnemy + " Complited");
        SecondTeam.instance.Next();
    }

    IEnumerator TakeAct(Ally a)
    {
        yield return new WaitForSeconds(1.2f);
        a.TakeDamage(Calculation.CalcDamageEnemy(eStats, a, eStats.damageWeapon), this);
        currentStats = eStats.stats;
        transform.DOMove(startPos, 1);
        Invoke("Next", 1);
    }


}
