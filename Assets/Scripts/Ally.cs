using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public struct ClassStats
{
    public string nameOfAlly;
    public int health;
    public int maxHealth;
    public int level;

    public WeaponData weapon;
    public CharacterStats stats;
    public Ability[] abilities;
}

public class Ally : MonoBehaviour
{
    
    public ClassStats allStats = new();
    public CharacterStats currentStats;

    SpriteRenderer render;
    Vector2 startPos;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();

        allStats.level = 1;
        startPos = transform.position;
    }

    public void Initialize(AlliesData data, CharacterStats stat)
    {
        allStats.nameOfAlly = data.NameOfAlly;
        allStats.maxHealth = allStats.health = data.Health + stat.stamina;
        allStats.weapon = data.Weapon;
        currentStats = allStats.stats = data.Stats = stat;
        allStats.abilities = data.Abilities;

        transform.name = allStats.nameOfAlly;

        GameManager.instance.UpdateStats(allStats);
    }

    public void TakeAction()
    {
        GameObject target = FindFirstObjectByType<Enemy>()?.gameObject;

        render.sortingOrder = 1;

        transform?.DOJump(target.transform.position, 1, 1, 1.2f);
        //transform.DOPunchPosition(target.transform.position, 2, 1, 1, false);
        Debug.Log(allStats.nameOfAlly + " is Attaking");
        
        StartCoroutine(TakeAct(target?.GetComponent<Enemy>()));
    }

    public void TakeDamage(int attacked, Enemy enemy)
    {
        allStats.health -= Calculation.CalcShealdAlly(this, enemy, attacked);

        

        if (allStats.health <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            GameManager.instance.UpdateStats(allStats);
        }
    }

    private void OnDestroy()
    {
        FirstTeam.instance.ClearQueue();

        if (!FirstTeam.instance.FindAllMembers())
        {
            SecondTeam.instance.ClearQueue(true);
            GameManager.instance.G_Over(GameState.GameOver);
        }
    }

    public void SetDefault()
    {
        allStats.health = allStats.maxHealth;
        allStats.level++;
    }

    void Next()
    {
        render.sortingOrder = 0;
        //Debug.Log(nameOfAlly + " Complited");
        FirstTeam.instance.Next();
    }

    IEnumerator TakeAct(Enemy e)
    {
        yield return new WaitForSeconds(1.2f);
        e.TakeDamage(Calculation.CalcDamageAlly(allStats, e, out allStats.stats), this);
        currentStats = allStats.stats;
        transform.DOMove(startPos, 1);
        Invoke("Next", 1);
    }
}
