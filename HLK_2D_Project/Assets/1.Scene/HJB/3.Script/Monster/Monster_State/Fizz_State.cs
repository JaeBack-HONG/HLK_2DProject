using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fizz_State : Monster_State
{
    
    
    private void Start()
    {
        MonsterDataSetting();        
    }    
    
    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "Fizz", hp: 2, detection: 5, range: 1, attackSpeed: 1,
                strength: 2, moveSpeed: 2, jumpForce: 0);
        Health = data.HP;
        state = Unit_state.Move;
        base.MonsterDataSetting();
        
    }
    private void FixedUpdate()
    {
        if (!state.Equals(Unit_state.Die))
        {
            Monster_HealthCheck();
        }

        switch (state)
        {
            case Unit_state.Default:
                break;
            case Unit_state.Idle:
                break;
            case Unit_state.Move:
                monsterMove.TotalMove();
                break;
            case Unit_state.Attack:
                break;
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Stun:                
                break;
            case Unit_state.Hit:
                break;
            case Unit_state.Die:
                break;
            default:
                break;
        }

        
    }
    private void ChangeState(Unit_state newState)
    {
        if (state.Equals(newState))
        {
            return;
        }
        state = newState;

        switch (state)
        {

            case Unit_state.Idle:
                break;
            case Unit_state.Move:
                break;
            case Unit_state.Attack:
                break;
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Stun:
                break;
            case Unit_state.Dash:
                break;
            case Unit_state.Die:
                break;
        }
    }
    public override void Monster_HealthCheck()
    {
        if (Health <= 0)
        {
            ChangeState(Unit_state.Die);
            base.Die();
        }
    }


}
