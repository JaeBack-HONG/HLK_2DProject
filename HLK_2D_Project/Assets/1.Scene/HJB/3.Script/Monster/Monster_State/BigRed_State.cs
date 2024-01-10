using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRed_State : Monster_State
{
    WaitForSeconds cool = new WaitForSeconds(0.25f);
    
    IEnumerator bigRedAttack_co;
    IEnumerator specialAttack_co;

    private void Start()
    {
        MonsterDataSetting();
    }

    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "BigRed", hp: 1, detection: 10, range: 2, attackSpeed: 1,
                strength: 2, moveSpeed: 5, jumpForce: 1);
        Health = data.HP;
        Strength = data.Strength;        
        ability_Item = Ability_Item.BigRed;
        bigRedAttack_co = BigRedAttack_Co();
        specialAttack_co = SpecialAttack_Co();
        base.MonsterDataSetting();
        ChangeState(Unit_state.Move);
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
                BigRed_PlayerCheck();
                break;
            case Unit_state.Attack:
                break;
            case Unit_state.Grab:
                break;
            case Unit_state.Stun:
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

        StopCoroutine();

        state = newState;

        switch (state)
        {

            case Unit_state.Idle:
                break;
            case Unit_state.Move:                
                break;
            case Unit_state.Attack:                                   
                BigRedSelectAttack();                                    
                break;
            case Unit_state.Grab:
                IsGrab();                
                break;
            case Unit_state.Stun:                
                break;
            case Unit_state.Die:
                break;
        }
    }

    private void StopCoroutine()
    {
        StopCoroutine(bigRedAttack_co);
        StopCoroutine(specialAttack_co);
        specialAttack_co = SpecialAttack_Co();
        bigRedAttack_co = BigRedAttack_Co();
    }
    private void BigRedSelectAttack()
    {
        
        int randomAttack = Random.Range(0,6);
        rigidbody.velocity = Vector2.zero;
        Debug.Log(randomAttack);
        if (randomAttack.Equals(5))
        {            
            StartCoroutine(specialAttack_co);
        }
        else
        {            
            StartCoroutine(bigRedAttack_co);
        }
        
    }

    #region //BigRed 플레이어 공격사거리 탐지
    private void BigRed_PlayerCheck()
    {
        float targetDistance = monsterMove.DistanceAndDirection();
        if (targetDistance < 2.5f)
        {            
            ChangeState(Unit_state.Attack);
        }
    }
    #endregion



    #region //BigRed Attack 공격_코루틴
    private IEnumerator BigRedAttack_Co()
    {        
        animator.SetTrigger("Attack");                
        yield return cool;
        animator.SetTrigger("Default");
        yield return new WaitForSeconds(1f);        
        ChangeState(Unit_state.Move);
        yield return null;
    }
    #endregion

    #region //BigRed Special 공격_코루틴
    private IEnumerator SpecialAttack_Co()
    {        
        animator.SetTrigger("Special");                
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
