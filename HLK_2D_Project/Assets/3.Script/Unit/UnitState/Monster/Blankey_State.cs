using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blankey_State : Monster_State
{
    private void Start()
    {
        MonsterDataSetting();
    }


    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "Blankey", hp: healthSet, detection: 7, range: 5, attackSpeed: 0.5f,
                strength: damageSet, moveSpeed: speedSet, jumpForce: 1);
        Health = data.HP;
        Strength = data.Strength;
        state = Unit_state.Move;
        ability_Item = Ability_Item.Blankey;
        
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
            case Unit_state.Default:
                break;
            case Unit_state.Idle: 
                break;
            case Unit_state.Move:
                if (monsterMove.target)
                {                    
                    Blankey_PlayerDetection();
                }
                break;
            case Unit_state.Attack:
                
                break;
            case Unit_state.Grab:                
                IsGrab();
                break;
            case Unit_state.Stun:
                //여기서 모든 행동 및 코루틴 중지
                
                break;
            case Unit_state.Hit:
                break;
            case Unit_state.Die:
                break;
            default:
                break;
        }

    }

    private void ChangeState(Unit_state newState)
    {
        if (state.Equals(newState) && !newState.Equals(Unit_state.Move))
        {
            return;
        }
        state = newState;

        switch (state)
        {

            case Unit_state.Idle:
                break;
            case Unit_state.Move:
                break;
            case Unit_state.Attack:
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
    private void Blankey_PlayerDetection()
    {
        Transform player = monsterMove.targetPlayer;
        monsterMove.PlayerDirectionCheck();
        Vector2 playerDirection = (player.position - transform.position).normalized;        
        if (monsterMove.direction.Equals(-1))
        {
            if (player.eulerAngles.y.Equals(180))
            {                
                animator.enabled = true;
                rigidbody.velocity = playerDirection * data.MoveSpeed;
            }
            else
            {
                animator.enabled = false;
                rigidbody.velocity = Vector2.zero;
            }
        }
        else
        {
            if (player.eulerAngles.y.Equals(0))
            {
                rigidbody.velocity = playerDirection * data.MoveSpeed;
                animator.enabled = true;
            }
            else
            {
                animator.enabled = false;
                rigidbody.velocity = Vector2.zero;
            }
        }
        
    }


    public override void Monster_HealthCheck()
    {
        if (Health <= 0)
        {
            ChangeState(Unit_state.Die);
            base.Die();
            GameObject ability_obj = Instantiate(Ability_Item_obj, transform.position, Quaternion.identity);
            ability_obj.GetComponent<AbilityItem>().itemidx = ability_Item;
        }
    }
}
