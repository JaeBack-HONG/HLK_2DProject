using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armand_State : Monster_State
{
    WaitForSeconds cool = new WaitForSeconds(0.25f);

    IEnumerator armandAttack_co;
    IEnumerator specialAttack_co;
    
    private void Start()
    {
        MonsterDataSetting();
    }

    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "Armand", hp: healthSet, detection: 10, range: 2, attackSpeed: 1,
                strength:damageSet, moveSpeed: speedSet, jumpForce: 0);
        Health = data.HP;
        Strength = data.Strength;
        ability_Item = Ability_Item.Armand;
        armandAttack_co = ArmandAttack_Co();
        specialAttack_co = SpecialAttack_Co();
        ChangeState(Unit_state.Move);
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
                Armand_PlayerCheck();
                break;
            case Unit_state.Attack:
                break;
            case Unit_state.Grab:                
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

        StopCoroutine();

        state = newState;

        switch (state)
        {

            case Unit_state.Idle:
                break;
            case Unit_state.Move:
                break;
            case Unit_state.Attack:
                ArmandSelectAttack();
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
        StopCoroutine(armandAttack_co);
        StopCoroutine(specialAttack_co);
        specialAttack_co = SpecialAttack_Co();
        armandAttack_co = ArmandAttack_Co();
    }
    private void ArmandSelectAttack()
    {

        int randomAttack = Random.Range(1, 11);
        rigidbody.velocity = Vector2.zero;
        animator.SetBool("Move", false);
        if (randomAttack.Equals(5))
        {
            StartCoroutine(specialAttack_co);
        }
        else
        {
            StartCoroutine(armandAttack_co);
        }

    }

    #region //Mr.Chopms �÷��̾� ���ݻ�Ÿ� Ž��
    private void Armand_PlayerCheck()
    {
        float targetDistance = monsterMove.DistanceAndDirection();
        if (targetDistance < 2.5f)
        {
            ChangeState(Unit_state.Attack);
        }
    }
    #endregion



    #region //BigRed Attack ����_�ڷ�ƾ
    private IEnumerator ArmandAttack_Co()
    {
        float currentTime = 0f;
        while (currentTime < 0.3f)
        {
            currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        currentTime = 0f;
        animator.SetTrigger("Attack");
        yield return cool;
        animator.SetTrigger("Default");
        yield return new WaitForSeconds(1f);
        ChangeState(Unit_state.Move);
        yield return null;
    }
    #endregion

    #region //BigRed Special ����_�ڷ�ƾ
    private IEnumerator SpecialAttack_Co()
    {
        float currentTime = 0f;
        while (currentTime < 0.3f)
        {
            currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        currentTime = 0f;
        animator.SetTrigger("Special");
        while (currentTime < 3f)
        {
            currentTime += Time.fixedDeltaTime;
            monsterMove.PlayerDirectionCheck();
            rigidbody.velocity = new Vector2(monsterMove.direction * data.MoveSpeed, rigidbody.velocity.y);
            
            yield return new WaitForFixedUpdate();
        }
        rigidbody.velocity = Vector2.zero;
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
