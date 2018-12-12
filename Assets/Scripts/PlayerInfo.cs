using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int level = 0;
    public int damage = 0;
    public int manaAmount = 1000;
    public int requiredExp = 10;
    public int expScale = 5;
    private int multiplier = 2;

    public delegate void Level(int level);
    public delegate void OnDamage(int damage);
    public delegate void Mana(int mana);

    public OnDamage damaged;
    public Level playerLevel;
    public Mana playerMana;

    private int _health;
    private int _exp;

    public int Exp
    {
        get
        {
            return _exp;
        }

        set
        {
            _exp = value;

            if (_exp >= requiredExp)
            {
                level++;
                playerLevel(level);
                _exp = 0;
                if (level % expScale == 0)
                {
                    requiredExp *= multiplier;
                }

            }
        }
    }

    public int Health
    {
        get
        {
            return _health;
        }

        set
        {
            _health = value;
            damaged(damage);
        }
    }
}
