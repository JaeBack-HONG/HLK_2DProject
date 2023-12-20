using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Brown_Mod : Player_Ability
{
    Vector2 randomvec;

    public override void UseAbility()
    {
        P_state.actState = Unit_state.Default;
        Brown_Ability();
    }

    private void Brown_Ability()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 3f, LayerMask.GetMask("Enemy"));
        Debug.DrawRay(transform.position, Vector2.right * 3f, Color.red);
        if (hit.collider != null)
        {
            if(hit.collider.gameObject.CompareTag("Enemy"))
            {
                hit.collider.gameObject.transform.position = new Vector2(transform.position.x, transform.position.y + 3f);
                animator.SetTrigger("BrownMod");

                hit.collider.gameObject.TryGetComponent<Monster_State>(out Monster_State M_state);
                hit.collider.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D otherRigid);
                M_state.state = Unit_state.Grab;
                StartCoroutine(Brown_Ab(otherRigid, M_state));
            }
        }
    }


    IEnumerator Brown_Ab(Rigidbody2D otherRigid, Monster_State M_state)
    {

        while (true)
        {
            rigidbody.velocity = Vector2.zero;
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                randomvec = new Vector2(-1f, 1f).normalized;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                randomvec = new Vector2(1f, 1f).normalized;
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                M_state.state = Unit_state.Idle;
                otherRigid.gravityScale = 4f;
                otherRigid.AddRelativeForce(randomvec * 20f, ForceMode2D.Impulse);
                animator.SetTrigger("Idle");
                break;
            }
            yield return null;
        }

        yield return null;
    }
}
