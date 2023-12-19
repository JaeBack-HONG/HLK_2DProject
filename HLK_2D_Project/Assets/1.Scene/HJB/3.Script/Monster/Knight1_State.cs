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
        monsterMove.TotalMove();
        Knight_PlayerCheck();
        Monster_HealthCheck();        
        SwordAttack();
    }
    
    public void SwordAttack()
    {

        if (state.Equals(Unit_state.Attack) && !isAttack)
        {
            isAttack = true;
            StartCoroutine(SwordAttack_co());
        }
    }

    private void Knight_PlayerCheck()
    {
        float targetDistance = monsterMove.DistanceAndDirection();

        if (targetDistance < 2.5f)
        {
            state = Unit_state.Attack;
        }
    }

    private IEnumerator SwordAttack_co()
    {     
        animator.SetTrigger("Attack");
        yield return cool;
        animator.SetTrigger("Default");
        state = Unit_state.Move;
        isAttack = false;
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
