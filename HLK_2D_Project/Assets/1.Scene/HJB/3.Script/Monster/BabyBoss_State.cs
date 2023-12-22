using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyBoss_State : Monster_State
{
    WaitForSeconds cool = new WaitForSeconds(0.9f);
    private bool ChangeCheck = false;
    private void Start()
    {
        MonsterDataSetting();
    }
    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "Knight1", hp: 10, detection: 5, range: 2, attackSpeed: 1,
                strength: 2, moveSpeed: 5, jumpForce: 0);
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
                BabyBossTransformation_PlayerCheck();
                break;
            case Unit_state.Move:
                monsterMove.TotalMove();
                BabyBossAttack_PlayerCheck();
                    break;
            case Unit_state.Attack:
                StartCoroutine(BabyBossAttack_co());
                break;
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Hit:
                break;
            case Unit_state.Jump://변신상태로 일단 
                StartCoroutine(Transformation_co());
                break;
                
            default:
                
                break;
        }

        if (!state.Equals(Unit_state.Default))
        {
            Monster_HealthCheck();
        }
    }
    private void BabyBossTransformation_PlayerCheck()
    {
        float targetDistance = monsterMove.DistanceAndDirection();

        if (targetDistance < 3f)
        {
            state = Unit_state.Jump;
        }
    }
    private void BabyBossAttack_PlayerCheck()
    {
        float targetDistance = monsterMove.DistanceAndDirection();

        if (targetDistance < 3f)
        {
            state = Unit_state.Attack;
        }
    }


    #region //베이비 공격(Co)
    private IEnumerator BabyBossAttack_co()
    {
        state = Unit_state.Default;
        rigidbody.velocity = Vector2.zero;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Default");
        state = Unit_state.Move;
    }
    #endregion

    #region// 베이비 변신(Co)
    private IEnumerator Transformation_co()
    {

        state = Unit_state.Default;
        animator.SetTrigger("Transformation");
        yield return new WaitForSeconds(2f);
        ChangeCheck = true;
        animator.SetBool("Change", ChangeCheck);
        state = Unit_state.Attack;
    }
    #endregion


    public override void Monster_HealthCheck()
    {
        if (Health <= 0 )
        {            
            base.Die();
        }
    }
}
