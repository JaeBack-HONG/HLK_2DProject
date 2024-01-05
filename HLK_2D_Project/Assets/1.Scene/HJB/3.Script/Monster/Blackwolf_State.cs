using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackwolf_State : Monster_State
{
    WaitForSeconds cool = new WaitForSeconds(0.25f);
    private float detectionTime = 5f;
    private float currentTime;

    private int P_DefaultHP = 0;

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
        //ability_Item = Ability_Item.BlackWolf;
        base.MonsterDataSetting();
    }
    private void FixedUpdate()
    {
        switch (state)
        {         
            
            case Unit_state.Idle:
                break;
            case Unit_state.Move:
                DetectPlayer();
                monsterMove.TotalMove();
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
            case Unit_state.Hit:
                break;
            case Unit_state.Jump:
                break;
            case Unit_state.Stun:
                StartCoroutine(Stun_co());
                break;
            case Unit_state.Dash:
                StartCoroutine(BlackWolfDash());
                break;
            case Unit_state.Die:
                break;
            default:
                break;
        }
        if (!state.Equals(Unit_state.Default))
        {
            BlackWolf_PlayerCheck();
            Monster_HealthCheck();
        }
    }

    #region //BlackWolf 플레이어 체력확인 및 탐지
    public void DetectPlayer()
    {

        if (Player == null)
        {
            return;
        }
        //타겟이 True가 아니라면 현재 플레이어의 체력을 계속 담기
        if (monsterMove.target && P_DefaultHP.Equals(0))
        {
            P_DefaultHP = Player.Health;

        }
        //타겟이 True라면 플레이어 현재 체력과 이전 체력 비교 10초간 확인
        if (monsterMove.target)
        {
            currentTime += Time.deltaTime;
            int P_CurrentHP = Player.Health;
            //Debug.Log($"디폴 체력 : {P_DefaultHP}, 현재체력 : {P_CurrentHP}, 탐지 : {currentTime} ");
            if (currentTime > detectionTime)
            {
                if (P_CurrentHP.Equals(P_DefaultHP))
                {
                    state = Unit_state.Dash;
                }
                //10초간 유지 후 탐지 되면 울프를 공격상태로 변경

                currentTime = 0;
                P_DefaultHP = 0;
            }
        }
    }
    #endregion

    #region //BlackWolf 플레이어 공격사거리 탐지
    private void BlackWolf_PlayerCheck()
    {
        if (state != Unit_state.Grab)
        {
            float targetDistance = monsterMove.DistanceAndDirection();        
            if (targetDistance < 3f)
            {
                state = Unit_state.Attack;
            }
        }
    }
    #endregion

    #region //BlackWolf 대쉬_코루틴
    private IEnumerator BlackWolfDash()
    {
        state = Unit_state.Idle;
        animator.SetTrigger("Dash");
        
        float elapsedTime = 0f;
        float attackDuration = 1f; // 이동이 완료되기를 원하는 시간

        while (elapsedTime < attackDuration)
        {
            float step = 10f * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, step);
            elapsedTime += Time.deltaTime;
            yield return null; // 다음 프레임까지 기다림
        }
        animator.SetTrigger("Default");
        P_DefaultHP = 0;
        state = Unit_state.Move;
    }
    #endregion

    #region //BlackWolf 깨물기공격_코루틴
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
            GameObject ability_obj = Instantiate(Ability_Item_obj, transform.position, Quaternion.identity);
            ability_obj.GetComponent<AbilityItem>().itemidx = ability_Item;
        }
    }
    
}
