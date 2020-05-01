using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PawnStatus : MonoBehaviour
{
    public Text txtAttak;
    public Text txtDefense;
    public Text txtLife;
    public Text txtDexterity;
    public Text txtAttackRange;
    public Text txtName;

    public void UpdatePawnStatusPanel(Pawn pawn)
    {
        UpdatePanel(pawn.Attack, pawn.Defense, pawn.HP, pawn.Dexterity, pawn.AttackRange, pawn.ToString());
    }
    private void UpdatePanel(int attack, int def, int life, int dex, int atkRange, string name)
    {
        txtAttak.text = attack.ToString();
        txtDefense.text = def.ToString();
        txtLife.text = life.ToString();
        txtDexterity.text = dex.ToString();
        txtAttackRange.text = atkRange.ToString();
        txtName.text = name;
    }
}
