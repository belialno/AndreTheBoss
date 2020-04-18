﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    public int Dexterity { get; set; }
    public int Level { get; set; }
    public int Life { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int AttackRange { get; set; }
    public string Name { get; set; }

    public HexCell currentCell;
    public PawnType Type { get; set; }
    public void InitializePawn(PawnType type, string name,
    int attack, int defense, int life, int dexterity, int attackRange)
    {
        Type = type;
        Name = name;
        Attack = attack;
        Defense = defense;
        Life = life;
        Dexterity = dexterity;
        AttackRange = attackRange;
    }
    public void DoAttack(Pawn other)
    {
        other.Life -= (Attack - other.Defense) > 0 ? Attack - other.Defense : 1;
    }

}