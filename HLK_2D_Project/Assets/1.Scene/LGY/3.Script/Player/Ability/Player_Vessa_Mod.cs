using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Vessa_Mod : Ability
{
    [Range(1f, 2f)]
    [SerializeField] private float speedBuff = 1.3f;

    [Range(1f, 2f)]
    [SerializeField] private float jumpBuff = 1.3f;

    [Range(3f, 7f)]
    [SerializeField] private float cool = 5f;

    private IEnumerator Buff_Co;
    public override void UseAbility()
    {
        Buff_Co = VessaMod_Co();
        StartCoroutine(Buff_Co);
    }

    private IEnumerator VessaMod_Co()
    {
        P_state.actState = Unit_state.Default;
        animator.SetTrigger("VessaMod");
        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
        animator.speed = 0.8f * anispeed;
        yield return new WaitForSeconds(0.3f * anispeed);



        animator.speed = 1f;
        animator.SetTrigger("Idle");
        P_state.actState = Unit_state.Idle;
        float currentTime = 0;
        P_Move.moveSpeed *= speedBuff;
        P_state.JumpForce *= jumpBuff;
        StartCoroutine(Buff_Cine(9.5f));

        while (currentTime < cool)
        {
            currentTime += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(Buff_Cine(8f));
        P_state.JumpForce = P_state.data.JumpForce;
        P_Move.moveSpeed = P_state.data.MoveSpeed;
        yield return null;
    }
    IEnumerator Buff_Cine(float value)
    {
        while (cinemachinevir.m_Lens.OrthographicSize < value)
        {
            cinemachinevir.m_Lens.OrthographicSize += Time.fixedDeltaTime * 8f;
            yield return new WaitForFixedUpdate();
        }

    }

}
