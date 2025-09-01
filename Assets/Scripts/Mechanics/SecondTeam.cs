using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondTeam : MonoBehaviour
{
    public static SecondTeam instance;

    [SerializeField] EnemyData[] enemyDatas;

    Queue<Enemy> enemies = new Queue<Enemy>();

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

        FindMembers();

        nexTurn = FindAllMembers();
    }

    void FindMembers()
    {
        Enemy[] enemy = GameObject.FindObjectsOfType<Enemy>();

        foreach (var a in enemy)
        {
            int index = Random.Range(0, enemyDatas.Length);
            a.Initialisize(enemyDatas[index]);
        }
    }

    public bool FindAllMembers()
    {
        Enemy[] enemy = GameObject.FindObjectsOfType<Enemy>();

        foreach (var a in enemy)
        {
            enemies.Enqueue(a);
        }

        if (enemies.Count <= 0) return false;

        return true;
    }

    public void Turn()
    {
        if (enemies.Count <= 0)
        {
            GameManager.instance.Next();
        }
        else if (nexTurn)
        {
            nexTurn = false;
            enemies?.Peek()?.TakeAction();
            enemies?.Dequeue();
        }
    }

    public void Next()
    {
        nexTurn = true;

        Turn();
    }

    public void ClearQueue(bool isAll = false)
    {
        if (isAll) enemies?.Clear();
        else if (enemies.TryDequeue(out Enemy e))
        Debug.Log(e.ToString());
    }
}
