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

    public void UpdatePawnStatusPanel(Pawn pawn)
    {
        UpdatePanel(pawn.Attack, pawn.Defense, pawn.Life, pawn.Dexterity, pawn.AttackRange);
    }
    private void UpdatePanel(int attack, int def, int life, int dex, int atkRange)
    {
        txtAttak.text = attack.ToString();
        txtDefense.text = def.ToString();
        txtLife.text = life.ToString();
        txtDexterity.text = dex.ToString();
        txtAttackRange.text = atkRange.ToString();
    }
}
