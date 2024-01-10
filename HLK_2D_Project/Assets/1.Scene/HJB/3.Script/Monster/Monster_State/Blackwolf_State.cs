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
    IEnumerator skeletonDash_co;
    IEnumerator skeletonAttack_co;
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
        ability_Item = Ability_Item.Skeleton;
        skeletonDash_co = SkeletonDash_Co();
        skeletonAttack_co = SkeletonAttack_Co();
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
                DetectPlayer();
                monsterMove.TotalMove();
                Skeleton_PlayerCheck();
                break;
            case Unit_state.Attack:                                    
                break;
            case Unit_state.Grab:
                IsGrab();
                StopCoroutine();
                break;
            case Unit_state.Hit:
                break;
            case Unit_state.Jump:
                break;
            case Unit_state.Stun:
                StopCoroutine();
                break;
            case Unit_state.Dash:
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
                StartCoroutine(skeletonAttack_co);
                break;
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Stun:
                break;
            case Unit_state.Dash:
                StartCoroutine(skeletonDash_co);
                break;
            case Unit_state.Die:
                break;
        }
    }

    private void StopCoroutine()
    {
        StopCoroutine(skeletonDash_co);
        StopCoroutine(skeletonAttack_co);
        skeletonDash_co = SkeletonDash_Co();
        skeletonAttack_co= SkeletonAttack_Co();
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
                    ChangeState(Unit_state.Dash);
                }
                //10�ʰ� ���� �� Ž�� �Ǹ� ������ ���ݻ��·� ����
                currentTime = 0;
                P_DefaultHP = 0;
            }
        }
    }
    #endregion

    #region //BlackWolf �÷��̾� ���ݻ�Ÿ� Ž��
    private void Skeleton_PlayerCheck()
    {        
        float targetDistance = monsterMove.DistanceAndDirection();        
        if (targetDistance < 3f)
        {
            ChangeState(Unit_state.Attack);
        }
     
    }
    #endregion

    #region //BlackWolf �뽬_�ڷ�ƾ
    private IEnumerator SkeletonDash_Co()
    {        
        animator.SetTrigger("Dash");
        
        float elapsedTime = 0f;
        float attackDuration = 1f; 

        while (elapsedTime < attackDuration)
        {
            float step = 10f * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, step);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        animator.SetTrigger("Default");
        P_DefaultHP = 0;
        ChangeState(Unit_state.Move);
    }
    #endregion

    #region //BlackWolf ���������_�ڷ�ƾ
    private IEnumerator SkeletonAttack_Co()
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
            base.Die();
            GameObject ability_obj = Instantiate(Ability_Item_obj, transform.position, Quaternion.identity);
            ability_obj.GetComponent<AbilityItem>().itemidx = ability_Item;
        }
    }
    
}
