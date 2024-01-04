using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Brown_Mod : Ability
{
    public override void UseAbility()
    {
        Brown_Ability();
    }

    private void Brown_Ability()
    {
        float ho = Input.GetAxis("Horizontal");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, ho * Vector2.right, 3f, LayerMask.GetMask("Enemy"));
        Debug.DrawRay(transform.position, Vector2.right * 3f, Color.red);
        if (hit.collider != null)
        {
            P_state.actState = Unit_state.Default;
            PlayerManager.instance.UsedAb();
            hit.collider.transform.position = new Vector2(transform.position.x, transform.position.y + 3f);
            animator.SetTrigger("BrownMod");
            hit.collider.TryGetComponent<Monster_State>(out Monster_State M_state);
            hit.collider.TryGetComponent<Rigidbody2D>(out Rigidbody2D otherRigid);
            M_state.state = Unit_state.Grab;
            StartCoroutine(Brown_Ab(otherRigid, M_state));
        }
        else P_state.actState = Unit_state.Idle;
    }


    IEnumerator Brown_Ab(Rigidbody2D otherRigid, Monster_State M_state)
    {

        rigidbody.velocity = Vector2.zero;


        yield return new WaitForSeconds(0.2f);
        otherRigid.gravityScale = 4f;
        M_state.state = Unit_state.Default;
        otherRigid.AddRelativeForce(-P_state.direction * 20f, ForceMode2D.Impulse);
        animator.SetTrigger("Idle");
        yield return new WaitForSeconds(0.2f);
        P_state.actState = Unit_state.Idle;
        yield return new WaitForSeconds(1f);
        M_state.state = Unit_state.Move;

        yield return null;
    }
}
