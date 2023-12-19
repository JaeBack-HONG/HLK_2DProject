using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brown_State : Monster_State
{
    public int Health;
    private void Start()
    {
        MonsterDataSetting();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //머리위가 플레이어라면
        if (collision.gameObject.layer.Equals(8))
        {
            Health--;
        }
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
