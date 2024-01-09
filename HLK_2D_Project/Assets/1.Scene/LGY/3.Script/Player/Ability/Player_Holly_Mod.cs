using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Holly_Mod : Ability
{
    [SerializeField] private BoxCollider2D Attackcol;

    public override void UseAbility()
    {
        StartCoroutine(HollyAttack_co());
    }

    IEnumerator HollyAttack_co()
    {
        gameObject.layer = (int)Layer_Index.Hit;
        P_state.actState = Unit_state.Default;
        PlayerManager.instance.UsedAb();
        animator.SetTrigger("HollyMod");
        animator.speed = anispeed;
        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce((P_state.direction * 4f + Vector2.up * 5f).normalized * 20f, ForceMode2D.Impulse);
        Attackcol.enabled = true;

        yield return new WaitForSeconds(0.7f / anispeed);

        Attackcol.enabled = false;
        animator.SetTrigger("Idle");
        animator.speed = 1;
        gameObject.layer = (int)Layer_Index.Player;
        P_state.actState = Unit_state.Idle;

        if (P_state.isFairy && PlayerManager.instance.count_List[PlayerManager.instance.current_Count] >= 2)
        {
            P_state.isFairy = false;
            P_state.actState = Unit_state.Attack;
        }


        yield return null;
    }
}
