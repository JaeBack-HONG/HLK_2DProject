using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player_BabyBoss_Mod : Ability
{
    [SerializeField] private BoxCollider2D attackbox;

    public override void UseAbility()
    {
        StartCoroutine(BabyBossAttack_co());
    }

    private IEnumerator BabyBossAttack_co()
    {
        gameObject.layer = (int)Layer_Index.Hit;
        P_state.actState = Unit_state.Default;
        rigidbody.velocity = Vector2.zero;
        PlayerManager.instance.UsedAb();
        animator.SetTrigger("BBMod");
        yield return new WaitForSeconds(0.6f);
        attackbox.enabled = true;
        yield return new WaitForSeconds(0.2f);
        attackbox.enabled = false;
        animator.SetTrigger("Idle");
        yield return new WaitForSeconds(0.5f);
        gameObject.layer = (int)Layer_Index.Player;
        P_state.actState = Unit_state.Move;
    }

}
