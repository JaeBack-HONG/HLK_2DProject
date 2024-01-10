using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kimu_State : Monster_State
{  

    private float currentTime = 0f;
    
    private void Start()
    {
        MonsterDataSetting();
    }
    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "Knight1", hp: healthSet, detection: 5, range: 2, attackSpeed: 1,
                strength: damageSet, moveSpeed: speedSet, jumpForce: 0);
        Health = data.HP;
        Strength = data.Strength;
        state = Unit_state.Move;
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
            case Unit_state.Default:
                break;
            case Unit_state.Idle:
                break;
            case Unit_state.Move:
                monsterMove.TotalMove();
                KimuJump_PlayerCheck();
                break;            
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Stun:
                break;            
            case Unit_state.Die:
                break;
            default:
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

    private void KimuJump_PlayerCheck()
    {
        float timeRandom = Random.Range(2f, 4f);
        if (monsterMove.target && currentTime>timeRandom)
        {
            int randomJump = Random.Range(6, 8);
            rigidbody.AddForce(Vector2.up * randomJump, ForceMode2D.Impulse);            
            currentTime = 0;
        }
        currentTime += Time.deltaTime;
    }


   

    public override void Monster_HealthCheck()
    {
        if (Health <= 0)
        {
            ChangeState(Unit_state.Die);
            base.Die();
        }
    }
}
