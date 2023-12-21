using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight1_State : Monster_State
{
    WaitForSeconds cool = new WaitForSeconds(0.9f);
    
    private void Start()
    {
        MonsterDataSetting();
    }    
    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "Knight1", hp: 5, detection: 5, range: 2, attackSpeed: 1,
                strength: 2, moveSpeed: 5, jumpForce: 0);
        Health = data.HP;
        Strength = data.Strength;
        state = Unit_state.Move;
        base.MonsterDataSetting();
    }
    private void FixedUpdate()
    {
        switch (state)
        {           
            case Unit_state.Idle:
                break;
            case Unit_state.Move:
                monsterMove.TotalMove();
                Knight_PlayerCheck();
                break;
            case Unit_state.Attack:
                StartCoroutine(SwordAttack_co());
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
    private void Knight_PlayerCheck()
    {
        float targetDistance = monsterMove.DistanceAndDirection();

        if (targetDistance < 2f)
        {
            state = Unit_state.Attack;
        }
    }
    private IEnumerator SwordAttack_co()
    {
        animator.SetTrigger("Attack");
        rigidbody.velocity = Vector2.zero;
        state = Unit_state.Idle;        
        yield return cool;
        animator.SetTrigger("Default");
        state = Unit_state.Move;        
        yield return null;
    }
    public override void Monster_HealthCheck()
    {
        if (Health <= 0)
        {
            base.Die();
        }
    }
}
