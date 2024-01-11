using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Armand_Mod : Ability
{

    IEnumerator ArmandTemp;

    public override void UseAbility()
    {
        ArmandTemp = ArmandMod_Co();
        StartCoroutine(ArmandTemp);
    }

    IEnumerator ArmandMod_Co()
    {
        P_state.actState = Unit_state.Idle;
        //animator.SetBool("ArmandMod", true);
        P_state.isArmand = true;
        animator.speed = 0.7f;
        float duration = 0;

        animator.SetLayerWeight(animator.GetLayerIndex("ArmandMod"), 1f);

        while (duration < 10)
        {
            duration += Time.deltaTime;

            ChangeState();

            if (Input.GetKeyDown(KeyCode.Z))
            {
                animator.SetTrigger("ArmandAttack");
            }



            yield return null;
        }

        animator.SetLayerWeight(animator.GetLayerIndex("ArmandMod"), 0f);


        P_state.isArmand = false;
        animator.SetBool("ArmandMod", false);
        yield return null;
    }


    private void ChangeState()
    {
        Jump_State jumpstate = P_state.JumState;
        if (P_state.JumState.Equals(jumpstate))
        {
            return;
        }


    }
}
