using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mr_Chomps_State : Monster_State
{
    WaitForSeconds cool = new WaitForSeconds(0.25f);

    IEnumerator Mr_ChompsAttack_co;

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
        Mr_ChompsAttack_co = Mr_Chomps_co();
        base.MonsterDataSetting();
    }
    private void FixedUpdate()
    {
        if (!state.Equals(Unit_state.Die))
        {
            Monster_HealthCheck();
        }
        switch (state)
        {

            case Unit_state.Idle:
                break;
            case Unit_state.Move:                
                monsterMove.TotalMove();
                Mr_Chomps_PlayerCheck();
                break;
            case Unit_state.Attack:                
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
    private void ChangeState(Unit_state newState)
    {
        if (state.Equals(newState))
        {
            return;
        }
        state = newState;

        StopCoroutine();

        switch (state)
        {

            case Unit_state.Idle:
                break;
            case Unit_state.Move:
                break;
            case Unit_state.Attack:
                StartCoroutine(Mr_ChompsAttack_co);
                break;
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Stun:
                break;
            case Unit_state.Dash:
                break;
            case Unit_state.Die:
                break;
        }
    }

    private void StopCoroutine()
    {
        StopCoroutine(Mr_ChompsAttack_co);
        Mr_ChompsAttack_co = Mr_Chomps_co();
    }

    #region //Mr.Chopms 플레이어 공격사거리 탐지
    private void Mr_Chomps_PlayerCheck()
    {        
        float targetDistance = monsterMove.DistanceAndDirection();
        if (targetDistance <2.5f)
        {
            ChangeState(Unit_state.Attack);
        }
    }
    #endregion



    #region //Mr.Chopms 깨물기공격_코루틴
    private IEnumerator Mr_Chomps_co()
    {
        animator.SetTrigger("Attack");
        
        rigidbody.velocity = Vector2.zero;
        
        yield return cool;
        animator.SetTrigger("Default");
        yield return new WaitForSeconds(1f);
        ChangeState(Unit_state.Move);        
        yield return null;
    }
    #endregion
    public override void Monster_HealthCheck()
    {
        if (Health <= 0)
        {
            ChangeState(Unit_state.Die);
            GameObject ability_obj = Instantiate(Ability_Item_obj, transform.position, Quaternion.identity);
            ability_obj.GetComponent<AbilityItem>().itemidx = ability_Item;
            base.Die();            
        }
    }
}
