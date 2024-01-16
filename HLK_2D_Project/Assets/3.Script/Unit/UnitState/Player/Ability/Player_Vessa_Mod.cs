using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Vessa_Mod : Ability
{
    [SerializeField] private float speedBuff = 12f;

    [SerializeField] private float jumpBuff = 19f;

    [Range(3f, 7f)]
    [SerializeField] private float duration = 5f;

    private IEnumerator Buff_Co;

    public override void UseAbility()
    {
        Buff_Co = VessaMod_Co();
        StartCoroutine(Buff_Co);
    }

    private IEnumerator VessaMod_Co()
    {
        DefaulutSet("VessaMod");

        rigidbody.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
        PlayerManager.instance.UsedAb();

        yield return new WaitForSeconds(0.35f * anispeed);

        EndSet();
        P_Move.moveSpeed = speedBuff;
        P_state.JumpForce = jumpBuff;

        yield return new WaitForSeconds(duration);
        P_state.JumpForce = P_state.data.JumpForce;
        P_Move.moveSpeed = P_state.data.MoveSpeed;

        yield return null;
    }

}
