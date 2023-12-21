using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Brown_Mod : Player_Ability
{
    Vector2 direction;
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
            hit.collider.transform.position = new Vector2(transform.position.x, transform.position.y + 3f);
            transform.position = new Vector2(transform.position.x, transform.position.y + 1f);
            animator.SetTrigger("BrownMod");
            rigidbody.isKinematic = true;
            hit.collider.TryGetComponent<Monster_State>(out Monster_State M_state);
            hit.collider.TryGetComponent<Rigidbody2D>(out Rigidbody2D otherRigid);
            M_state.state = Unit_state.Grab;
            StartCoroutine(Brown_Ab(otherRigid, M_state));
        }
    }


    IEnumerator Brown_Ab(Rigidbody2D otherRigid, Monster_State M_state)
    {

        while (true)
        {
            rigidbody.velocity = Vector2.zero;

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                direction = new Vector2(-1f, 1f).normalized;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                direction = new Vector2(1f, 1f).normalized;
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                M_state.state = Unit_state.Idle;
                otherRigid.gravityScale = 4f;
                otherRigid.AddRelativeForce(direction * 20f, ForceMode2D.Impulse);
                animator.SetTrigger("Idle");
                transform.position = new Vector2(transform.position.x, transform.position.y - 1f);
                rigidbody.isKinematic = false;
                break;
            }
            yield return null;
        }

        yield return null;
    }
}
