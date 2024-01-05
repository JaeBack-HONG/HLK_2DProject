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

        yield return new WaitForSeconds(0.25f);


        CreateBubble(P_state.direction);

        animator.SetTrigger("Idle");
        P_state.actState = Unit_state.Idle;
        yield return null;
    }

    private void CreateBubble(Vector3 direction)
    {
        Vector3 startPos = transform.position + new Vector3(direction.x, 0.5f, 0);
        GameObject bubble = Instantiate(lil_WizMagic_obj, startPos, Quaternion.identity);
        Player_MagicBubble bubble_C = bubble.GetComponent<Player_MagicBubble>();
        bubble_C.Start_Co(direction);


    }
}
