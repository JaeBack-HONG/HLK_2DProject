using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight1_State : Monster_State
{
    WaitForSeconds cool = new WaitForSeconds(0.9f);
    private bool isAttacking = false;
    IEnumerator coroutine;
    
    private void Start()
    {
        MonsterDataSetting();
        coroutine = SwordAttack_co();
    }    
    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "Knight1", hp: healthSet, detection: 5, range: 2, attackSpeed: 1,
                strength: damageSet, moveSpeed: speedSet, jumpForce: 0);
        Health = data.HP;
        Strength = data.Strength;
        state = Unit_state.Move;
        //ability_Item = Ability_Item.Knight;
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
                    Knight_PlayerCheck();
                    monsterMove.TotalMove();                
                break;
            case Unit_state.Attack:
                if (!isAttacking)
                {
                    coroutine = SwordAttack_co();
                    StartCoroutine(coroutine);
                }
                break;
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Hit:
                break;
            case Unit_state.Stun:
                break;
            case Unit_state.Die:
                break;
            default:
                break;
        }

        if (!state.Equals(Unit_state.Default))
        {
            Monster_HealthCheck();

        }
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
        isAttacking = true;
        animator.SetTrigger("Attack");
        rigidbody.velocity = Vector2.zero;
        state = Unit_state.Idle;                
        yield return cool;
        animator.SetTrigger("Default");
        state = Unit_state.Move;        
        isAttacking = false;
        yield return null;
    }
    public override void Monster_HealthCheck()
    {
        if (Health <= 0)
        {
            base.Die();
            GameObject ability_obj = Instantiate(Ability_Item_obj, transform.position, Quaternion.identity);
            ability_obj.GetComponent<AbilityItem>().itemidx = ability_Item;
        }
    }
}
