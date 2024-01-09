using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRed_State : Monster_State
{
    WaitForSeconds cool = new WaitForSeconds(0.25f);

    private bool bigRedisAttack = false;

    IEnumerator bigRedAttack_co;
    IEnumerator specialAttack_co;

    private void Start()
    {
        MonsterDataSetting();
    }

    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "Blackwolf", hp: 1, detection: 10, range: 2, attackSpeed: 1,
                strength: 2, moveSpeed: 5, jumpForce: 1);
        Health = data.HP;
        Strength = data.Strength;
        state = Unit_state.Move;
        ability_Item = Ability_Item.BigRed;
        bigRedAttack_co = BigRedAttack_Co();
        specialAttack_co = SpecialAttack_Co();
        base.MonsterDataSetting();
    }
    private void FixedUpdate()
    {
        if (!state.Equals(Unit_state.Default))
        {
            Monster_HealthCheck();
        }


        switch (state)
        {

            case Unit_state.Idle:
                break;
            case Unit_state.Move:
                monsterMove.TotalMove();
                BigRed_PlayerCheck();
                break;
            case Unit_state.Attack:
                                   
                BigRedSelectAttack();                    
                
                break;
            case Unit_state.Grab:
                IsGrab();
                StopCoroutine();
                break;
            case Unit_state.Stun:
                StopCoroutine();
                break;
            case Unit_state.Die:
                break;
        }
    }

    private void StopCoroutine()
    {
        StopCoroutine(bigRedAttack_co);
        StopCoroutine(specialAttack_co);
    }
    private void BigRedSelectAttack()
    {
        if (!bigRedisAttack)
        {
            int randomAttack = Random.Range(0, 100);
            rigidbody.velocity = Vector2.zero;            
            if (randomAttack.Equals(5))
            {
                bigRedAttack_co = BigRedAttack_Co();
                StartCoroutine(specialAttack_co);
            }
            else
            {
                specialAttack_co = SpecialAttack_Co();
                StartCoroutine(bigRedAttack_co);
            }
        }
    }

    #region //Mr.Chopms 플레이어 공격사거리 탐지
    private void BigRed_PlayerCheck()
    {
        float targetDistance = monsterMove.DistanceAndDirection();
        if (targetDistance < 2.5f)
        {
            state = Unit_state.Attack;
        }
    }
    #endregion



    #region //BigRed Attack 공격_코루틴
    private IEnumerator BigRedAttack_Co()
    {
        bigRedisAttack = true;
        animator.SetTrigger("Attack");        
        state = Unit_state.Idle;
        yield return cool;
        animator.SetTrigger("Default");
        yield return new WaitForSeconds(1f);
        state = Unit_state.Move;
        bigRedisAttack = false;
        yield return null;
    }
    #endregion

    #region //BigRed Special 공격_코루틴
    private IEnumerator SpecialAttack_Co()
    {
        bigRedisAttack = true;
        animator.SetTrigger("Special");        
        state = Unit_state.Idle;
        yield return cool;
        animator.SetTrigger("Default");
        yield return new WaitForSeconds(1f);
        state = Unit_state.Move;
        bigRedisAttack = false;
        yield return null;
    }
    #endregion
    public override void Monster_HealthCheck()
    {
        if (Health <= 0)
        {
            base.Die();
        }
    }
}
