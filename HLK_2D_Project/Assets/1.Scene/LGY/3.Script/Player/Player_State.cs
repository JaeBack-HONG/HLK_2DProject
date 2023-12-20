using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State : MonoBehaviour
{
    public UnitData data;

    public Unit_state actState;
    public Condition_state conState;

    public Unit_Hit unithit;

    public int Health;

    public Player_Ability P_Ability;
    public Player_Move P_Move;

    private void Start()
    {
        TryGetComponent<Player_Move>(out P_Move);
        PlayerDataSetting();
        actState = Unit_state.Idle;
        TryGetComponent<Unit_Hit>(out unithit);
    }

    public void PlayerDataSetting()
    {
        data = new UnitData(name: "Player", hp: 10, detection: 5, range: 1, attackSpeed: 1, strength: 2, moveSpeed: 2, jumpForce: 100);
        Health = data.HP;

    }
    private void Update()
    {     
        State_Check();

        Player_HealthCheck();        
    }

    private void State_Check()
    {
        switch (actState)
        {
            case Unit_state.Default:
               
                break;
            case Unit_state.Idle:
                P_Move.MoveCheck();
                P_Move.GroundRayCheck();
                break;
            case Unit_state.Move:
                P_Move.MoveCheck();
                P_Move.GroundRayCheck();
                break;
            case Unit_state.Attack:

                break;
            case Unit_state.Grab:
                break;
            case Unit_state.Hit:
                break;
            case Unit_state.Jump:
                P_Move.MoveCheck();
                break;
            case Unit_state.Falling:
                P_Move.MoveCheck();
                break;
            default:
                break;
        }
        P_Move.IsFalling();
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
