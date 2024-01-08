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
        PlayerManager.instance.UsedAb();
        rigidbody.velocity = Vector2.zero;
        P_state.actState= Unit_state.Default;
        animator.SetTrigger("LilWizMod");
        PlayerManager.instance.UsedAb();

        yield return new WaitForSeconds(0.25f);


        CreateBubble();

        animator.SetTrigger("Idle");
        P_state.actState = Unit_state.Idle;
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
