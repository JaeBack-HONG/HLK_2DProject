using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player_BabyBoss_Mod : Ability
{
    [SerializeField] private int noisescale = 10;
    [SerializeField] private BoxCollider2D attackbox;

    public override void UseAbility()
    {
        StartCoroutine(BabyBossAttack_co(noisescale));
    }

    private IEnumerator BabyBossAttack_co(int noisescale)
    {
        P_state.isAttack = true;
        gameObject.layer = (int)Layer_Index.Hit;
        P_state.actState = Unit_state.Default;
        rigidbody.velocity = Vector2.zero;
        PlayerManager.instance.UsedAb();
        animator.SetTrigger("BBMod");
        yield return new WaitForSeconds(0.6f);
        attackbox.enabled = true;
        noise.m_AmplitudeGain = noisescale;
        yield return new WaitForSeconds(0.2f);
        attackbox.enabled = false;
        noise.m_AmplitudeGain = 0;
        animator.SetTrigger("Idle");
        yield return new WaitForSeconds(0.5f);
        gameObject.layer = (int)Layer_Index.Player;
        P_state.actState = Unit_state.Move;
        P_state.isAttack = false;
    }

}
