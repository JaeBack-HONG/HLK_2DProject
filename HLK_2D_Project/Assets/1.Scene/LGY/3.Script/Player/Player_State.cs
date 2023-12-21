using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Jump_State
{
    Idle = 0,
    Jumping = 1,
    Falling,
}


public class Player_State : MonoBehaviour
{
    public UnitData data;

    public Unit_state actState;
    public Condition_state conState;
    public Jump_State JumState;

    public Unit_Hit unithit;

    public int Health;

    public Player_Ability P_Ability;
    public Player_Move P_Move;

    private void Start()
    {
        TryGetComponent<Player_Move>(out P_Move);
        PlayerDataSetting();
        actState = Unit_state.Idle;
        JumState = Jump_State.Idle;
        TryGetComponent<Unit_Hit>(out unithit);
    }

    public void PlayerDataSetting()
    {
        data = new UnitData(name: "Player", hp: 10, detection: 5, range: 1, attackSpeed: 1, strength: 2, moveSpeed: 2, jumpForce: 100);
        Health = data.HP;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            actState = Unit_state.Attack;
        }
        State_Check();

        Player_HealthCheck();
    }

    private void State_Check()
    {
        switch (actState)
        {
            case Unit_state.Idle:
                P_Move.MoveCheck();
                break;
            case Unit_state.Move:
                P_Move.MoveCheck();
                break;
            case Unit_state.Attack:
                P_Ability = GetComponent<Player_Brown_Mod>();
                P_Ability.UseAbility();
                break;
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Hit:
                break;
        }

        IsFalling();
        GroundRayCheck();

    }

    public void IsGrab()
    {
        P_Move.rigidbody.velocity = Vector2.zero;
        P_Move.rigidbody.gravityScale = 0f;
    }


    public void GroundRayCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, LayerMask.GetMask("Ground"));
        if (hit.collider != null)//&& JumState.Equals(Junmp_State.Idle))
        {
            JumState = Jump_State.Idle;

            if(actState != Unit_state.Idle)
            {
                actState = Unit_state.Idle;
            }
        }

    }
    public void IsFalling()
    {
        if (P_Move.rigidbody.velocity.y < -0.01f)
        {
            JumState = Jump_State.Falling;
        }
        else if (P_Move.rigidbody.velocity.y > 0.01f)
        {
            JumState = Jump_State.Jumping;
        }


    }

    public void Player_HealthCheck()
    {
        if (Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject, 1f);
    }

    public void Attack(Monster_State other)
    {
        other.Health -= data.Strength;
        other.UnitHit.Hit(other.gameObject.layer);
    }

    public void AbilitySetting()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {

        }
        if (Input.GetKeyDown(KeyCode.W))
        {

        }
        if (Input.GetKeyDown(KeyCode.E))
        {

        }
    }
}
