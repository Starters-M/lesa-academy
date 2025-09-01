using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTeam : MonoBehaviour
{
    public static FirstTeam instance;

    //[SerializeField] AlliesData[] alliesData;

    Queue<Ally> allies = new Queue<Ally>();

    bool nexTurn;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        nexTurn = FindAllMembers();
    }

    public bool FindAllMembers()
    {
        Ally[] ally = GameObject.FindObjectsOfType<Ally>();

        foreach (var a in ally)
        {
            allies.Enqueue(a);
        }

        if (allies.Count <= 0) return false;

        return true;
    }

    public void Initialize(AlliesData data)
    {
        foreach (var ally in allies)
        {
            int p = Random.Range(1, 4), a = Random.Range(1, 4), s = Random.Range(1, 4);
            CharacterStats stats = new CharacterStats(p, a, s);
            ally.Initialize(data, stats);
        }
    }

    public void Turn()
    {
        if (allies.Count <= 0)
        {
            GameManager.instance.Next();
        }
        else if (nexTurn)
        {
            nexTurn = false;
            allies?.Peek()?.TakeAction();
            allies?.Dequeue();
        }
    }

    public void Next()
    {
        nexTurn = true;

        Turn();
    }

    public void ClearQueue(bool isAll = false)
    {
        if (isAll) allies?.Clear();
        else if (allies.TryDequeue(out Ally a))
            Debug.Log(a.ToString());
    }
}
