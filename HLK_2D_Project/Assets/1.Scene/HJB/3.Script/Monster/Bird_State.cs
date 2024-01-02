using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird_State : Monster_State
{
    WaitForSeconds cool = new WaitForSeconds(0.9f);
    IEnumerator attack_co;
    

    private float direction;

    
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
        state = Unit_state.Idle;
        ability_Item = Ability_Item.Bird;
        attack_co = BirdkAttack_co();

        base.MonsterDataSetting();
    }
    private void FixedUpdate()
    {
        switch (state)
        {

            case Unit_state.Default:
                break;
            case Unit_state.Idle:
                BirdAttack_PlayerCheck();
                break;
            case Unit_state.Move:                
                break;
            case Unit_state.Attack:
                attack_co = BirdkAttack_co();
                StartCoroutine(attack_co);
                break;
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Hit:
                break;
            case Unit_state.Stun:
                break;
            case Unit_state.Dash:                
                break;
            case Unit_state.Die:
                break;
            default:
                break;
        }

        if (!state.Equals(Unit_state.Default))
        {
            Monster_HealthCheck();

        }
    }
    private void BirdAttack_PlayerCheck()
    {
        if (monsterMove.target)
        {            
            state = Unit_state.Attack;
        }
    }

    #region// Bird 공격_코루틴
    private IEnumerator BirdkAttack_co()
    {

        float currentTime = 0f;
        float attackTime = 2f;

        state = Unit_state.Dash;
        animator.SetTrigger("Attack");
        Vector3 currentTrans = transform.position;        
        while (currentTime < 0.5f)
        {
            currentTime += Time.deltaTime;
            float randomX = Random.Range(-0.5f, 0.5f);
            float randomY = Random.Range(-0.5f, 0.5f);
            transform.position = new Vector3(currentTrans.x + randomX, currentTrans.y + randomY);

            yield return null;
        }

        currentTime = 0f;

        direction = ( monsterMove.targetPlayer.localPosition.x - transform.localPosition.x);
        direction = (direction >= 0) ? 1 : -1;        
        
        if (direction < 1)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }


        Dash = true;
        animator.SetTrigger("Default");
        while (Dash && currentTime <0.5f)
        {
            currentTime += Time.deltaTime;
            rigidbody.velocity = (monsterMove.targetPlayer.transform.localPosition - transform.localPosition).normalized * 16f;
            yield return null;
        }

        Vector2 targetDirection = (monsterMove.targetPlayer.transform.localPosition - transform.localPosition).normalized;        

        while (Dash&&currentTime<attackTime)
        {
            currentTime += Time.deltaTime;
            rigidbody.velocity = targetDirection * 10f;
            yield return null;            
        }
        
        rigidbody.velocity = Vector2.zero;

        currentTime = 0f;

        while (currentTime < 1f&&transform.position.y<10f)
        {
            
            currentTime += Time.deltaTime;
            rigidbody.velocity = Vector2.up * 2f;
            yield return null;

        }
                
        while (transform.position.y > 6f)
        {
            
            
            rigidbody.velocity = Vector2.down * 5f;
            yield return null;

        }

        Dash = false;
        rigidbody.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        state = Unit_state.Idle;
        yield return null;
    }
    #endregion

    public override void Monster_HealthCheck()
    {
        if (Health <= 0)
        {
            StopCoroutine(attack_co);
            state = Unit_state.Hit;
            rigidbody.velocity = Vector2.zero;
            base.Die();
            GameObject ability_obj = Instantiate(Ability_Item_obj, transform.position, Quaternion.identity);
            ability_obj.GetComponent<AbilityItem>().itemidx = ability_Item;
        }
    }
}
