using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Data;


public enum GameState
{
    Playing,
    GameOver,
    Victory
}

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [Header("Teams")]
    [SerializeField] FirstTeam firstTeam;
    [SerializeField] SecondTeam secondTeam;
    [Header("Spots and prefabs")]
    [SerializeField] Transform spot1;
    [SerializeField] Transform spot2;
    [SerializeField] GameObject allyObj, enemyObj;
    [Header("UI")]
    [SerializeField] GameObject ContainerUI;
    [SerializeField] GameObject ContainerBt;
    [SerializeField] TextMeshProUGUI roundTxt;
    [SerializeField] TextMeshProUGUI allyStats, enemyStats;

    GameState state;

    int round;
    public int Round
    {
        get { return round; }
    }

    int turn;
    public int turN
    {
        get { return turn; }
    }

    bool nexTeem;

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

        Initialize();
    }

    void Initialize()
    {
        state = GameState.Playing;

        round = 1;
        turn = 1;

        InitializeA();
        InitializeE();
    }

    void InitializeE()
    {
        int countOfEnemy = Random.Range(1, 2);

        switch (countOfEnemy)
        {
            case 1:
                Instantiate(enemyObj, spot2);
                break;
            case 2:
                Vector2 place = new Vector2(spot2.position.x, spot2.position.y);
                place.x = -2;
                GameObject g = Instantiate(enemyObj, place, Quaternion.identity);
                g.transform.parent = spot2;
                place.x = 2;
                g = Instantiate(enemyObj, place, Quaternion.identity);
                g.transform.parent = spot2;
                break;
            default:
                goto case 1;
        }

    }

    void InitializeA()
    {
        Instantiate(allyObj, spot1);
    }

    public void StartButton()
    {
        ContainerUI.SetActive(false);
        roundTxt.gameObject.SetActive(true);
        roundTxt.text = "Round " + round;

        Invoke("StartGame", 3);
    }

    void StartGame()
    {
        nexTeem = true;

        state = GameState.Playing;

        roundTxt.gameObject.SetActive(false);
    }

    public void UpdateStats(EnemyStats data)
    {
        enemyStats.text = "Enemy Stats:\nType: " + data.nameOfEnemy + "\nHealth: " + data.health + "\nPower: " + (data.damageWeapon + data.stats.strength) + "\nAbility: " + data.abilities;
    }
    
    public void UpdateStats(ClassStats data)
    {
        allyStats.text = "Ally Stats:\nType: " + data.nameOfAlly + "\nHealth: " + data.health + "\nWeapon: " + data.weapon.NameOfWeapon + ", " + data.weapon.Damage + "\nStats: " + data.stats.strength + ", " + data.stats.agility + ", " + data.stats.stamina;
    }

    private void Update()
    {
        if (state != GameState.Playing)
            return;

        if (nexTeem)
        {
            nexTeem = false;
            Turn(round);
        }
    }

    public void ResInitialize()
    {
        if (state == GameState.Victory)
        {
            InitializeE();
        }
        else if (state == GameState.GameOver)
        {
            InitializeA();
        }

        StartButton();
        roundTxt.gameObject.SetActive(false);
        ContainerBt.SetActive(false);
    }

    public void G_Over(GameState st)
    {
        state = st;

        if (st == GameState.Victory)
        {
            roundTxt.text = "Victory";   
        }
        else if (st == GameState.GameOver)
        {
            roundTxt.text = "Game Over";
        }

        roundTxt.gameObject.SetActive(true);
        ContainerBt.SetActive(true);
    }

    void Turn(int r)
    {
        if (round % 2 == 1)
        {
            if (turn % 2 == 1)
            {
                firstTeam.Turn();
            }
            else
            {
                secondTeam.Turn();
            }
        }
        else
        {
            if (turn % 2 == 0)
            {
                firstTeam.Turn();
            }
            else
            {
                secondTeam.Turn();
            }
        }
    }

    public void Next()
    {
        if (turn % 2 == 0)
        {
            if (firstTeam.FindAllMembers() && secondTeam.FindAllMembers())
            {
                round++;
                StartButton();
            }

        }
        else
        {
            nexTeem = true;
        }

        turn++;
    }

}
