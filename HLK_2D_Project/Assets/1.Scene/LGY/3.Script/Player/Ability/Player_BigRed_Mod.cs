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
        rigidbody.velocity = Vector2.zero;
        P_state.actState= Unit_state.Default;
        animator.SetTrigger("BigRedMod");
        animator.speed = anispeed;
        punchCol.enabled=true;
        yield return new WaitForSeconds(0.55f / anispeed);
        punchCol.enabled = false;

        animator.SetTrigger("Idle");
        animator.speed = 1;
        P_state.actState= Unit_state.Idle;
        yield return null;
    }
}
