using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Armand_Mod : Ability
{
    private bool isAttack = false;
    [SerializeField] private CircleCollider2D attackcol;
    [SerializeField] private float duration = 30f;
    IEnumerator ArmandTemp;

    public override void UseAbility()
    {
        ArmandTemp = ArmandMod_Co();
        StartCoroutine(ArmandTemp);
    }

    IEnumerator ArmandMod_Co()
    {
        P_state.actState = Unit_state.Idle;
        P_state.isArmand = true;
        animator.speed = anispeed;
        float currentTime = 0;

        animator.SetLayerWeight(animator.GetLayerIndex("ArmandMod"), 1f);
        animator.SetTrigger("Idle");

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Z) && P_state.JumState.Equals(Jump_State.Idle) && !isAttack)
            {
                StartCoroutine(ArmandAttack_Co());
            }
            yield return null;
        }

        animator.SetLayerWeight(animator.GetLayerIndex("ArmandMod"), 0f);
        P_state.isArmand = false;

        yield return null;
    }

    IEnumerator ArmandAttack_Co()
    {
        isAttack = true;
        rigidbody.velocity = Vector2.zero;
        P_state.actState = Unit_state.Default;
        animator.SetTrigger("ArmandAttack");

        yield return new WaitForSeconds(0.21f);
        attackcol.enabled = true;
        yield return new WaitForSeconds(1f);
        attackcol.enabled = false;
        P_state.actState = Unit_state.Idle;
        isAttack = false;
        yield return null;
    }
}
