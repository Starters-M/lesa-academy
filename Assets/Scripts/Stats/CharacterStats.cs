using System;

[Serializable]
public struct CharacterStats
{
    public int strength;
    public int agility;
    public int stamina;

    public CharacterStats(int p, int a, int s)
    {
        strength = p;
        agility = a;
        stamina = s;
    }
}
