using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Handrick_Mod : Ability
{
    [SerializeField] private float gauge = 0f;
    [SerializeField] private float rushSpeed = 13f;
    [SerializeField] private CircleCollider2D spearcol;
    [SerializeField] private float gaugecor = 2f;
    private bool isRush = false;

    public override void UseAbility()
    {
        if (!isRush) StartCoroutine(HandrickAttack_co());
    }

    private IEnumerator HandrickAttack_co()
    {
        isRush = true;
        P_state.actState = Unit_state.Idle;

        while (Input.GetKey(KeyCode.Z) && gauge <= 3f)
        {
            gauge += Time.fixedDeltaTime * gaugecor;
            yield return new WaitForFixedUpdate();
        }

        while (Input.GetKey(KeyCode.Z))
        {
            yield return new WaitForFixedUpdate();
        }
        transform.gameObject.layer = (int)Layer_Index.Hit;
        spearcol.enabled = true;
        PlayerManager.instance.UsedAb();

        animator.SetTrigger("HandrickMod");
        while (gauge >= 0)
        {
            gauge -= Time.deltaTime;
            rigidbody.velocity = new Vector2(P_state.direction.x * rushSpeed, rigidbody.velocity.y);
            yield return null;
        }

        transform.gameObject.layer = (int)Layer_Index.Player;
        spearcol.enabled = false;
        animator.SetTrigger("Idle");
        isRush = false;
        yield return null;
    }



}
