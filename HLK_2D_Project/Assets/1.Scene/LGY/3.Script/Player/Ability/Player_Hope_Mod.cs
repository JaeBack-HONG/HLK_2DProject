using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Hope_Mod : Ability
{
    [SerializeField] private GameObject player_Bullet;

    public override void UseAbility()
    {
        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        PlayerManager.instance.UsedAb();
        P_state.actState = Unit_state.Default;
        rigidbody.constraints |= RigidbodyConstraints2D.FreezePositionX;
        animator.SetTrigger("HopeMod");
        yield return new WaitForSeconds(0.55f);
        Vector3 start = transform.position + new Vector3(P_state.direction.x * 2f, 0.5f, 0);
        Instantiate(player_Bullet, start, Quaternion.identity, transform);


        P_state.actState = Unit_state.Idle;
        animator.SetTrigger("Idle");
        rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(0.2f);

        yield return null;
    }
}
