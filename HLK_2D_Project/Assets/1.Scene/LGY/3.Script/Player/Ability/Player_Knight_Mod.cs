using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Knight_Mod : Ability
{
    WaitForSeconds cool = new WaitForSeconds(0.9f);

    public override void UseAbility()
    {
        StartCoroutine(SwordAttack_co());

    }

    private IEnumerator SwordAttack_co()
    {
        rigidbody.isKinematic = true;
        transform.position = new Vector2(transform.position.x, transform.position.y + 1f);
        PlayerManager.instance.UsedAb();
        animator.SetTrigger("KnightMod");
        rigidbody.velocity = Vector2.zero;
        P_state.actState = Unit_state.Default;
        yield return cool;
        animator.SetTrigger("Idle");
        transform.position = new Vector2(transform.position.x, transform.position.y -1f);
        rigidbody.isKinematic = false;
        P_state.actState = Unit_state.Idle;
        yield return null;
    }
}
