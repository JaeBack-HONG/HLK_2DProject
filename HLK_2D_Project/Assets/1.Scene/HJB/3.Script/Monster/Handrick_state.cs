using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handrick_state : Monster_State
{
    WaitForSeconds cool = new WaitForSeconds(0.9f);

    private bool rush = false;

    private float direction;

    private GameObject targetPlayer;
    private void Start()
    {
        MonsterDataSetting();
    }
    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "Knight1", hp: 5, detection: 5, range: 2, attackSpeed: 1,
                strength: 2, moveSpeed: 3, jumpForce: 0);
        Health = data.HP;
        Strength = data.Strength;
        state = Unit_state.Move;
        base.MonsterDataSetting();
    }
    private void FixedUpdate()
    {
        switch (state)
        {

            case Unit_state.Default:
                break;
            case Unit_state.Idle:
                break;
            case Unit_state.Move:
                monsterMove.TotalMove();
                HandrickAttack_PlayerCheck();
                break;
            case Unit_state.Attack:
                HandrickAttack_co();
                break;
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Hit:
                break;
            case Unit_state.Jump:
                break;
            case Unit_state.Dash:
                break;
            default:
                break;
        }

        if (!state.Equals(Unit_state.Default))
        {
            Monster_HealthCheck();

        }
    }
    private void HandrickAttack_PlayerCheck()
    {
        if (monsterMove.target.Equals(true))
        {
            targetPlayer = Player.gameObject;
            direction = (transform.localPosition.x - targetPlayer.transform.localPosition.x);
            direction = (direction < 0) ? -1 : 1;
            Debug.Log(direction);
            state = Unit_state.Attack;
        }
    }

    private IEnumerator HandrickAttack_co()
    {
        state = Unit_state.Dash;
        rush = true;
        yield return new WaitForSeconds(0.5f);
        //animator.SetTrigger();
        while (rush)
        {
            //rigidbody.velocity = new Vector2(direction * 5f, rigidbody.velocity.y);
            Debug.Log(rush);
            rigidbody.velocity = new Vector2(direction * 5f, rigidbody.velocity.y);
            yield return null;
        }

        state = Unit_state.Default;
        yield return new WaitForSeconds(0.5f);
        state = Unit_state.Move;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals((int)Layer_Index.Ground)
            )
        {
            //rush = false;
        }
    }

    public override void Monster_HealthCheck()
    {
        if (Health <= 0)
        {
            base.Die();
        }
    }
}
