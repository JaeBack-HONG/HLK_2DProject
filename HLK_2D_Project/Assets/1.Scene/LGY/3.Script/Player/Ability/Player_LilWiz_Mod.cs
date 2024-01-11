using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_LilWiz_Mod : Ability
{
    [SerializeField] private GameObject lil_WizMagic_obj;

    public override void UseAbility()
    {
        StartCoroutine(Lil_WizAttack_co());
    }

    private IEnumerator Lil_WizAttack_co()
    {
        P_state.isAttack = true;
        rigidbody.velocity = Vector2.zero;
        P_state.actState = Unit_state.Default;
        animator.SetTrigger("LilWizMod");
        animator.speed = anispeed;
        PlayerManager.instance.UsedAb();

        yield return new WaitForSeconds(0.3f / anispeed);

        CreateBubble();

        yield return new WaitForSeconds(0.15f / anispeed);
        animator.SetTrigger("Idle");
        P_state.actState = Unit_state.Idle;
        P_state.isAttack = false;

        if (P_state.isFairy && PlayerManager.instance.count_List[PlayerManager.instance.current_Count] >= 2)
        {
            P_state.isFairy = false;
            P_state.actState = Unit_state.Attack;
        }

        yield return null;
    }

    private void CreateBubble()
    {
        GameObject bubble = Instantiate(lil_WizMagic_obj,
            new Vector3(transform.position.x + P_state.direction.x, transform.position.y + 0.2f), Quaternion.identity);
        Player_Projectile bubble_C = bubble.GetComponent<Player_Projectile>();
        bubble_C.StartCoroutine(bubble_C.Shot(P_state.direction));



    }
}
