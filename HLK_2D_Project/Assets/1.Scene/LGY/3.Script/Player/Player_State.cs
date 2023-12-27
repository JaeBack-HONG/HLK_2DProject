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

    private Player_Ability P_Ability;
    private Player_Move P_Move;

    private void Start()
    {
        TryGetComponent<Player_Move>(out P_Move);
        PlayerDataSetting();
        actState = Unit_state.Idle;
        JumState = Jump_State.Idle;
        TryGetComponent<Unit_Hit>(out unithit);
        TryGetComponent<Player_Ability>(out P_Ability);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            actState = Unit_state.Attack;
        }
        P_Ability.AbilitySetting();
        State_Check();

        Player_HealthCheck();
    }

    private void PlayerDataSetting()
    {
        data = new UnitData(name: "Player", hp: 10, detection: 5, range: 1, attackSpeed: 1, strength: 2, moveSpeed: 2, jumpForce: 100);
        Health = data.HP;
    }

    private void State_Check()
    {
        switch (actState)
        {
            case Unit_state.Default:
                break;
            case Unit_state.Idle:
                P_Move.MoveCheck();
                break;
            case Unit_state.Move:
                P_Move.MoveCheck();
                break;
            case Unit_state.Attack:
                P_Ability.current_Ab.UseAbility();
                break;
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Hit:
                unithit.Hit(gameObject.layer);
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
        Debug.DrawRay(transform.position, Vector2.down, Color.red, 0.5f);
        if (hit.collider != null)
        {
            if (JumState.Equals(Jump_State.Idle)) P_Move.jumpCount = P_Move.maxJumps;

            if (JumState.Equals(Jump_State.Falling)) JumState = Jump_State.Idle;

            if (!actState.Equals(Unit_state.Idle)) actState = Unit_state.Idle;
        }

    }
    public void IsFalling()
    {
        if (P_Move.rigidbody.velocity.y < -0.01f)
        {
            if(P_Move.jumpCount.Equals(P_Move.maxJumps)) P_Move.jumpCount--;
            
            if(!JumState.Equals(Jump_State.Falling)) JumState = Jump_State.Falling;
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
}
