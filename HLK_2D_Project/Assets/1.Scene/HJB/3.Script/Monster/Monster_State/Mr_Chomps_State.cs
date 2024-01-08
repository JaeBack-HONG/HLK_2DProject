using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mr_Chomps_State : Monster_State
{
    WaitForSeconds cool = new WaitForSeconds(0.25f);    

    private bool skeletonAttack = false;

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
        ability_Item = Ability_Item.Mr_Chomps;
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
                BlackWolf_PlayerCheck();
                break;
            case Unit_state.Attack:
                if (!skeletonAttack)
                {
                    StartCoroutine(WolfAttack_co());
                }
                break;
            case Unit_state.Grab:
                IsGrab();
                break;                       
            case Unit_state.Stun:
                break;            
            case Unit_state.Die:
                break;            
        }
        if (!state.Equals(Unit_state.Default))
        {
            Monster_HealthCheck();
        }
    }    
    

    #region //Mr.Chopms 플레이어 공격사거리 탐지
    private void BlackWolf_PlayerCheck()
    {        
        float targetDistance = monsterMove.DistanceAndDirection();
        if (targetDistance <2.5f)
        {
            state = Unit_state.Attack;
        }
    }
    #endregion



    #region //Mr.Chopms 깨물기공격_코루틴
    private IEnumerator WolfAttack_co()
    {
        animator.SetTrigger("Attack");
        skeletonAttack = true;
        rigidbody.velocity = Vector2.zero;
        state = Unit_state.Idle;
        yield return cool;
        animator.SetTrigger("Default");
        yield return new WaitForSeconds(1f);
        state = Unit_state.Move;
        skeletonAttack = false;
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
