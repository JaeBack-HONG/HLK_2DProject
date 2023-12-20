using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackwolf_State : Monster_State
{
    
    private void Start()
    {
        MonsterDataSetting();
    }

    
    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "Blackwolf", hp: 15, detection: 10, range: 2, attackSpeed: 1,
                strength: 2, moveSpeed: 5, jumpForce: 1);
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
                
                break;
            case Unit_state.Attack:
                
                break;
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Hit:
                break;
            case Unit_state.Jump:
                break;
            default:
                break;
        }        
        Monster_HealthCheck();
    }
    public override void Monster_HealthCheck()
    {
        if (data.HP <= 0)
        {
            base.Die();
        }
    }
}
