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
                strength: 1, moveSpeed: 2, jumpForce: 0);
        Health = data.HP;
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
            case Unit_state.Stun:
                StartCoroutine(Stun_co());
                break;
            default:
                break;
        }

        if (!state.Equals(Unit_state.Default))
        {
            Monster_HealthCheck();
        }
    }
    public override void Monster_HealthCheck()
    {
        if (Health<=0)
        {
            base.Die();
        }
    }
    
}
