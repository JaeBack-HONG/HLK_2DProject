using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kimu_State : Monster_State
{
    WaitForSeconds cool = new WaitForSeconds(0.9f);

    private bool rush = false;

    private float direction;

    private GameObject targetPlayer;

    private float currentTime = 0f;
    
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
        state = Unit_state.Move;
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
                monsterMove.TotalMove();
                KimuJump_PlayerCheck();
                break;            
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Jump:
                
                break;
            default:
                break;
        }

        if (!state.Equals(Unit_state.Default))
        {
            Monster_HealthCheck();

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
            base.Die();
        }
    }
}
