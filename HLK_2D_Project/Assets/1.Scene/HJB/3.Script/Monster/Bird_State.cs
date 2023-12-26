using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird_State : Monster_State
{
    WaitForSeconds cool = new WaitForSeconds(0.9f);

    

    private float direction;

    
    private void Start()
    {
        MonsterDataSetting();
    }
    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "Knight1", hp: 5, detection: 5, range: 2, attackSpeed: 1,
                strength: 2, moveSpeed: 3, jumpForce: 0);
        Health = data.HP;
        Strength = data.Strength;
        state = Unit_state.Idle;
        base.MonsterDataSetting();
    }
    private void FixedUpdate()
    {
        switch (state)
        {

            case Unit_state.Default:
                break;
            case Unit_state.Idle:
                BirdAttack_PlayerCheck();
                break;
            case Unit_state.Move:                
                break;
            case Unit_state.Attack:
                StartCoroutine(BirdkAttack_co());
                break;
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Hit:
                break;
            case Unit_state.Jump:
                break;
            case Unit_state.Dash:
                
                break;
            default:
                break;
        }

        if (!state.Equals(Unit_state.Default))
        {
            Monster_HealthCheck();

        }
    }
    private void BirdAttack_PlayerCheck()
    {
        if (monsterMove.target)
        {            
            state = Unit_state.Attack;
        }
    }

    private IEnumerator BirdkAttack_co()
    {

        float currentTime = 0f;
        float attackTime = 2f;

        state = Unit_state.Default;
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.5f);

        direction = ( monsterMove.targetPlayer.localPosition.x - transform.localPosition.x);
        direction = (direction >= 0) ? 1 : -1;        
        
        if (direction < 1)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        Vector2 targetDirection = (monsterMove.targetPlayer.transform.localPosition - transform.localPosition).normalized;        

        Dash = true;
        animator.SetTrigger("Default");
        while (Dash&&currentTime<attackTime)
        {
            currentTime += Time.deltaTime;
            rigidbody.velocity = targetDirection * 8f;
            yield return null;
            Debug.Log(currentTime);
            
        }
        rigidbody.velocity = Vector2.zero;

        currentTime = 0f;

        while (currentTime < 1f)
        {
            currentTime += Time.deltaTime;
            rigidbody.velocity = Vector2.up * 2f;
            yield return null;

        }

        Dash = false;
        rigidbody.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        state = Unit_state.Idle;
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
