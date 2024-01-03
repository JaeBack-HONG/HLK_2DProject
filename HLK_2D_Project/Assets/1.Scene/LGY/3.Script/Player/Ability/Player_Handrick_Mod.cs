using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Handrick_Mod : Ability
{
    public float gauge = 0f;
    [SerializeField] float rushSpeed = 10f;
    private bool isRush = false;

    public override void UseAbility()
    {
        if (!isRush) StartCoroutine(HandrickAttack_co());
    }

    private IEnumerator HandrickAttack_co()
    {
        isRush = true;
        //P_state.actState = Unit_state.Default;
        rigidbody.velocity=Vector2.zero;
        animator.SetTrigger("Charge");
        

        while (Input.GetKey(KeyCode.Z) && gauge <= 3f)
        {
            gauge += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        while (Input.GetKey(KeyCode.Z))
        {
            yield return new WaitForFixedUpdate();
        }
        PlayerManager.instance.UsedAb();
        P_state.actState = Unit_state.Idle;

        animator.SetTrigger("Rush");
        while (gauge >= 0)
        {
            gauge -= Time.deltaTime;
            rigidbody.velocity = new Vector2(P_state.direction.x * rushSpeed, rigidbody.velocity.y);
            yield return null;
        }

        animator.SetTrigger("Idle");
        isRush = false;
        yield return null;
    }



}
