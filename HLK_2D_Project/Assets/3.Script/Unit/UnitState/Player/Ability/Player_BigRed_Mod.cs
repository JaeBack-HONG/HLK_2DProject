using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_BigRed_Mod : Ability
{
    [SerializeField] private CircleCollider2D punchCol;

    public override void UseAbility()
    {
        StartCoroutine(BigRedAttack_Co());
    }

    private IEnumerator BigRedAttack_Co()
    {
        DefaulutSet("BigRedMod");
        PlayerManager.instance.UsedAb();
        punchCol.enabled=true;
        yield return new WaitForSeconds(0.55f / anispeed);
        punchCol.enabled = false;

        EndSet();

        if (P_state.isFairy && PlayerManager.instance.count_List[PlayerManager.instance.Select_Idx] >= 2)
        {
            P_state.isFairy = false;
            P_state.actState = Unit_state.Attack;
        }

        yield return null;
    }
}
