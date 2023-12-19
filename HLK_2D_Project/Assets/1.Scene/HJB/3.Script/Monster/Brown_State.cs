using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brown_State : Monster_State
{
   
    private void Start()
    {
        MonsterDataSetting();
    }

   
    public override void MonsterDataSetting()
    {
        data = new UnitData
        (name: "Brown", hp: 5, detection: 3, range: 0.5f, 
            attackSpeed: 2,strength: 2, moveSpeed: 1, jumpForce: 0);
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
