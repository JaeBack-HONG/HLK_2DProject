using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Skeleton_Mod : Ability
{
    IEnumerator Dash_temp;

    public override void UseAbility()
    {
        PlayerManager.instance.UsedAb();
        Dash();
    }

    public void Dash()
    {
        Dash_temp = Dash_Co();
        StartCoroutine(Dash_temp);
    }

    IEnumerator Dash_Co()
    {
        float currenttime = 0f;

        P_state.actState = Unit_state.Default;

        while (currenttime < 0.2f)
        {
            currenttime += Time.fixedDeltaTime;
            rigidbody.velocity = new Vector2(P_state.direction.x * 24f, 0);
            yield return new WaitForFixedUpdate();
        }

        P_state.actState = Unit_state.Idle;
        yield return null;
    }
}
