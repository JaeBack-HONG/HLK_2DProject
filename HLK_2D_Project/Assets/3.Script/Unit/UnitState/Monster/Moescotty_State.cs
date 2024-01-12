using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moescotty_State : Monster_State
{
    [Header("탐지 시간")]
    [SerializeField] private float detectionSet = 0;

    [SerializeField] float currentTime = 0f;   

    [SerializeField] private Vector2 M_position;

    IEnumerator moe_return_co;
    private void Start()
    {
        MonsterDataSetting();
        M_position = transform.position;
    }
    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "MoeScotty", hp: healthSet, detection: 7, range: 5, attackSpeed: 0.5f,
                strength: damageSet, moveSpeed: speedSet, jumpForce: 1);
        Health = data.HP;
        Strength = data.Strength;
        state = Unit_state.Idle;
        ability_Item = Ability_Item.MoeScotty;
        moe_return_co = MoescottyReturn_Co();
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
                TargetPlayerCheck();
                break;
            case Unit_state.Move:
                Blankey_PlayerDetection();
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
                break;
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Stun:
                break;
            case Unit_state.Dash:
                StartCoroutine(moe_return_co);
                break;
            case Unit_state.Die:                
                break;
        }
    }
    private void StopCoroutine()
    {
        StopCoroutine(moe_return_co);
        moe_return_co = MoescottyReturn_Co();
    }
    private void TargetPlayerCheck()
    {
        if (monsterMove.target)
        {
            ChangeState(Unit_state.Move);
        }
    }
    private void Blankey_PlayerDetection()
    {
        Transform player = monsterMove.targetPlayer;
        monsterMove.PlayerDirectionCheck();
        Vector2 playerDirection = (player.position - transform.position).normalized;

        rigidbody.velocity = playerDirection * data.MoveSpeed;

        currentTime += Time.deltaTime;

        if (currentTime >= detectionSet)
        {
            currentTime = 0f;
            ChangeState(Unit_state.Dash);
        }
    }
    private IEnumerator MoescottyReturn_Co()
    {        
        currentTime = 0f;
        rigidbody.velocity = Vector2.zero;        
        while (currentTime < 3f)
        {
            currentTime += Time.fixedDeltaTime;
            transform.position = Vector2.Lerp(transform.position, M_position, currentTime/20f);            
            yield return new WaitForFixedUpdate();            
        }
        rigidbody.velocity = Vector2.zero;
        currentTime = 0f;
        ChangeState(Unit_state.Idle);
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
