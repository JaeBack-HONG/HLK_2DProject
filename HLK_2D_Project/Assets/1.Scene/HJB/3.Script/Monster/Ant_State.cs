using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant_State : Monster_State
{
    
    private void Start()
    {
        MonsterDataSetting();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer.Equals((int)Layer_Index.Player))
        {
            Player_State P_state =col.gameObject.GetComponent<Player_State>();
            //1219
        }
    }

    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "Ant", hp: 1, detection: 4, range: 1, attackSpeed: 1,
                strength: 1, moveSpeed: 1, jumpForce: 0);
        Health = data.HP;
        base.MonsterDataSetting();
    }
    public override void Monster_HealthCheck()
    {
        if (Health<=0)
        {
            base.Die();
        }
    }
    
}
