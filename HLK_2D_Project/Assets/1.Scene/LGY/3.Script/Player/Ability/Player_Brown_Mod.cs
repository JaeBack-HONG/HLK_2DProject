using System.Collections;
using UnityEngine;

public class Player_Brown_Mod : Ability
{
    float gravityscale;

    public override void UseAbility()
    {
        Brown_Ability();
    }

    private void Brown_Ability()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, P_state.direction, 3f, LayerMask.GetMask("Enemy"));

        if (hit.collider != null)
        {
            DefaulutSet("BrownMod");

            PlayerManager.instance.UsedAb();
            hit.collider.transform.position = new Vector2(transform.position.x, transform.position.y + 3f);
            hit.collider.TryGetComponent<Monster_State>(out Monster_State M_state);
            hit.collider.TryGetComponent<Rigidbody2D>(out Rigidbody2D otherRigid);
            gravityscale = otherRigid.gravityScale;
            M_state.state = Unit_state.Grab;
            StartCoroutine(Brown_Ab(otherRigid, M_state));
        }
        else
        {
            P_state.actState = Unit_state.Idle;
            P_state.isAttack = false;
        }
    }

    IEnumerator Brown_Ab(Rigidbody2D otherRigid, Monster_State M_state)
    {

        rigidbody.velocity = Vector2.zero;


        yield return new WaitForSeconds(0.2f / anispeed);
        otherRigid.gravityScale = gravityscale;
        M_state.state = Unit_state.Default;
        otherRigid.AddRelativeForce(-P_state.direction * 20f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.4f / anispeed);
        EndSet();
        yield return new WaitForSeconds(1.1f);
        M_state.state = Unit_state.Move;
        yield return null;
    }
}
