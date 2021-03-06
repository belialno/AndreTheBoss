﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private GameManager gameManager;
    private CharacterReader characterReader;

    public Enemy EnemyPrefab_thief;
    public Enemy EnemyPrefab_sword;
    public Enemy EnemyPrefab_magic;

    private List<Enemy> EnemyPawns;

    private GameObject EnemyRoot;
    public void OnEnable()
    {
        EnemyPawns = new List<Enemy>();
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
        characterReader = gameManager.getCharacterReader();
        EnemyRoot = new GameObject();
        EnemyRoot.transform.SetParent(transform);
        EnemyRoot.transform.position = Vector3.zero;
    }

    public void SpawnEnemy(int turnNum)
    {
        if(turnNum < 10)
        {
            
        }
        else if(turnNum < 20)
        {
            
        }
        else if(turnNum < 30)
        {

        }
    }

    public void SpawnEnemyAtCell(EnemyType type, HexCell cell)
    {
        if(cell.CanbeDestination())
        {
            Enemy newEnemy = null;
            CharacterReader.CharacterData data = characterReader.GetCharacterData(PawnType.Enemy, type.ToString(), 1);

            switch (type)
            {
                case EnemyType.wanderingSwordman:
                    newEnemy = Instantiate<Enemy>(EnemyPrefab_sword);
                    break;
                case EnemyType.magicApprentice:
                    newEnemy = Instantiate<Enemy>(EnemyPrefab_magic);
                    break;
                case EnemyType.thief:
                    newEnemy = Instantiate<Enemy>(EnemyPrefab_thief);
                    break;
            }
        }
    }

    public EnemyType getRandomEnemyType()
    {
        return (EnemyType)Random.Range(0, (int)EnemyType.NUM);
    }


    /*
    public void SpawnEnemies()
    {
        for (int i = 0; i < 3; i++)
        {
            int ran = Random.Range(0, (int)EnemyType.NUM);
            EnemyType type = (EnemyType)ran;

            


            newEnemy.InitializeEnemy(type, newEnemy.ToString(), data.attack, data.defense, data.HP, data.dexterity, data.attackRange);
            EnemyPawns.Add(newEnemy);


            HexCell cell = hexMap.GetRandomCellToSpawn();
            cell.pawn = newEnemy;
            newEnemy.currentCell = cell;
            newEnemy.transform.SetParent(EnemyRoot.transform);
            newEnemy.transform.position = cell.transform.position;

            hexMap.RevealCellsFrom(cell);
        }

    }
    */

}
