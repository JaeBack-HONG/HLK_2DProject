using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Blankey_Mod : Ability
{

    [SerializeField] private GameObject detectbox;

    public override void UseAbility()
    {
        StartCoroutine(OnOffDetect());
    }
    
    IEnumerator OnOffDetect()
    {
        P_state.isAttack = true;
        rigidbody.velocity = Vector2.zero;
        rigidbody.constraints |= RigidbodyConstraints2D.FreezePositionY;
        P_state.actState = Unit_state.Default;
        detectbox.SetActive(true);
        PlayerManager.instance.UsedAb();
        animator.SetTrigger("BlankeyMod");
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.3f);

        Time.timeScale = 1f;
        P_state.actState = Unit_state.Idle;

        detectbox.SetActive(false);
        animator.SetTrigger("Idle");
        rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        P_state.isAttack = false;

        if (P_state.isFairy && PlayerManager.instance.count_List[PlayerManager.instance.current_Count] >= 2)
        {
            P_state.isFairy = false;
            P_state.actState = Unit_state.Attack;
        }

        yield return null;
    }



}
