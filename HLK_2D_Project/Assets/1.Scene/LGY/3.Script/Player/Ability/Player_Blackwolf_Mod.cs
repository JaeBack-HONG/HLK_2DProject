using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Blackwolf_Mod : Ability
{
    public override void UseAbility()
    {
        StartCoroutine(Dash_Co());
    }

    IEnumerator Dash_Co()
    {
        float currenttime = 0f;

        animator.SetTrigger("WolfMod");

        P_state.actState = Unit_state.Default;
        rigidbody.gravityScale = 0f;
        PlayerManager.instance.UsedAb();

        yield return new WaitForSeconds(0.1f);

        while (currenttime < 0.2f)
        {
            currenttime += Time.deltaTime;
            rigidbody.velocity = P_state.direction * 30f;
            yield return null;
        }

        animator.SetTrigger("Idle");
        P_state.actState = Unit_state.Idle;
        rigidbody.gravityScale = 4f;

        yield return null;
    }
}
