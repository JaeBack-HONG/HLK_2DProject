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

        yield return new WaitForSeconds(0.1f);

        Vector2 direction = (transform.rotation.y.Equals(0)) ? Vector2.right : Vector2.left;

        while (currenttime < 0.2f)
        {
            currenttime += Time.deltaTime;
            rigidbody.velocity = direction * 30f;
            yield return null;
        }

        animator.SetTrigger("Idle");
        P_state.actState = Unit_state.Idle;
        rigidbody.gravityScale = 4f;

        yield return null;
    }
}
