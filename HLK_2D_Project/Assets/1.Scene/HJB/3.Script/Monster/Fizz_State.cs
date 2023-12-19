using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fizz_State : Monster_State
{
    
    
    private void Start()
    {
        MonsterDataSetting();
        state = Unit_state.Move;
    }

    
    
    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "Fizz", hp: 2, detection: 5, range: 1, attackSpeed: 1,
                strength: 2, moveSpeed: 2, jumpForce: 0);
        Health = data.HP;
        base.MonsterDataSetting();   
        
    }

    private void FixedUpdate()
    {
        Monster_HealthCheck();
    }

    public override void Monster_HealthCheck()
    {
        if (Health <= 0)
        {
            base.Die();
        }
    }


}
