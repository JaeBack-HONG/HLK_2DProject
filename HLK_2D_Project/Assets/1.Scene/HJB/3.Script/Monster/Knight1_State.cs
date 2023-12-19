using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight1_State : Monster_State
{
    public int Health;
    private void Start()
    {
        MonsterDataSetting();
    }

    
    public override void MonsterDataSetting()
    {
        data = new UnitData
    (name: "Knight1", hp: 5, detection: 5, range: 2, attackSpeed: 1,
        strength: 2, moveSpeed: 1, jumpForce: 0);
        Health = data.HP;
        base.MonsterDataSetting();
    }

    public override void Monster_HealthCheck()
    {
        if (data.HP <= 0)
        {
            base.Die();
        }
    }
}
