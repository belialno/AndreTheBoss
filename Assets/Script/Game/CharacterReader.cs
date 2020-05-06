using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
public class CharacterReader
{
    private string path = "/Resources/CharacterData.xml";

    public XmlDocument xmlDoc;
    public class CharacterData
    {
        public int attack;
        public int defense;
        public int HP; 
        public int dexterity; 
        public int attackRange;

        public override string ToString()
        {
            return "Attack: " + attack + " defense: " + defense + " HP: " + HP + " dexterity: " + dexterity + " attackRange: " + attackRange;
        }
    };

    public void ReadFile()
    {
        xmlDoc = new XmlDocument();
        xmlDoc.Load(Application.dataPath + path);
    }

    public CharacterData GetCharacterData(PawnType pawnType, string characterName, int level)
    {
        CharacterData data = new CharacterData();
        string xpath = characterName;
        if (pawnType == PawnType.Enemy)
            xpath = "/characters/enemies/" + xpath;
        else if (pawnType == PawnType.Monster)
            xpath = "/characters/monsters/" + xpath;
        
        XmlElement node = (XmlElement)xmlDoc.SelectSingleNode(xpath).ChildNodes[level-1];
        
        if(node == null)
        {
            Debug.Log("On CharacterReader: " + characterName + " not found");
            return null;
        }

        data.attack = int.Parse(node["attack"].InnerXml);
        data.defense = int.Parse(node["defense"].InnerXml);
        data.HP = int.Parse(node["hp"].InnerXml);
        data.dexterity = int.Parse(node["dexterity"].InnerXml);
        data.attackRange = int.Parse(node["attackRange"].InnerXml);
        return data;
    }

    public bool InitPawnData(ref Pawn pawn, PawnType pawnType, int characterTypeEnum, int level)
    {
        if (pawn == null)
            return false;

        if(pawnType == PawnType.Enemy)
        {
            Enemy enemy = (Enemy)pawn;
            CharacterData data = GetCharacterData(pawnType, ((EnemyType)characterTypeEnum).ToString(), level);
            enemy.InitializeEnemy((EnemyType)characterTypeEnum, ((EnemyType)characterTypeEnum).ToString(), 
                data.attack, data.defense, data.HP, data.dexterity, data.attackRange);
        }
        else if(pawnType == PawnType.Monster)
        {
            Monster monster = (Monster)pawn;
            CharacterData data = GetCharacterData(pawnType, ((MonsterType)characterTypeEnum).ToString(), level);
            monster.InitializeMonster((MonsterType)characterTypeEnum, ((MonsterType)characterTypeEnum).ToString(),
                data.attack, data.defense, data.HP, data.dexterity, data.attackRange);
        }
        return true;
    }

}
