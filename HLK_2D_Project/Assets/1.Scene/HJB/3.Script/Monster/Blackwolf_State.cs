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

    #region //BlackWolf �÷��̾� ü��Ȯ�� �� Ž��
    public void DetectPlayer()
    {

        if (Player == null)
        {
            return;
        }
        //Ÿ���� True�� �ƴ϶�� ���� �÷��̾��� ü���� ��� ���
        if (monsterMove.target && P_DefaultHP.Equals(0))
        {
            P_DefaultHP = Player.Health;

        }
        //Ÿ���� True��� �÷��̾� ���� ü�°� ���� ü�� �� 10�ʰ� Ȯ��
        if (monsterMove.target)
        {
            currentTime += Time.deltaTime;
            int P_CurrentHP = Player.Health;
            //Debug.Log($"���� ü�� : {P_DefaultHP}, ����ü�� : {P_CurrentHP}, Ž�� : {currentTime} ");
            if (currentTime > detectionTime)
            {
                if (P_CurrentHP.Equals(P_DefaultHP))
                {
                    state = Unit_state.Dash;
                }
                //10�ʰ� ���� �� Ž�� �Ǹ� ������ ���ݻ��·� ����

                currentTime = 0;
                P_DefaultHP = 0;
            }
        }
    }
    #endregion

    #region //BlackWolf �÷��̾� ���ݻ�Ÿ� Ž��
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

    #region //BlackWolf �뽬_�ڷ�ƾ
    private IEnumerator BlackWolfDash()
    {
        state = Unit_state.Idle;
        animator.SetTrigger("Dash");
        
        float elapsedTime = 0f;
        float attackDuration = 1f; // �̵��� �Ϸ�Ǳ⸦ ���ϴ� �ð�

        while (elapsedTime < attackDuration)
        {
            float step = 10f * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, step);
            elapsedTime += Time.deltaTime;
            yield return null; // ���� �����ӱ��� ��ٸ�
        }
        animator.SetTrigger("Default");
        P_DefaultHP = 0;
        state = Unit_state.Move;
    }
    #endregion

    #region //BlackWolf ���������_�ڷ�ƾ
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
