using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Percy_State : Monster_State
{
    
    WaitForSeconds WaitCool = new WaitForSeconds(1f);
    [SerializeField] private GameObject Percy_FireBall_obj;
    [SerializeField] private GameObject shotPosi;
    IEnumerator PercyAttack_co;
    private bool berserk = false;
    private void Start()
    {
        MonsterDataSetting();
    }


    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "Percy", hp: 4, detection: 7, range: 5, attackSpeed: 0.5f,
                strength: 1, moveSpeed: 2, jumpForce: 0);
        Health = data.HP;
        Strength = data.Strength;
        state = Unit_state.Move;
        ability_Item = Ability_Item.Percy;
        PercyAttack_co = PercyAttack_Co();
        base.MonsterDataSetting();
    }
    private void FixedUpdate()
    {        
        if (!state.Equals(Unit_state.Die))
        {
            Monster_HealthCheck();            
            BerserkCheck();
        }

        switch (state)
        {            
            case Unit_state.Idle:
                break;
            case Unit_state.Move:
                Tracy_PlayerCheck();
                monsterMove.TotalMove();
                break;
            case Unit_state.Attack:                                
                break;
            case Unit_state.Grab:
                StopCoroutine();
                IsGrab();
                break;
            case Unit_state.Stun:
                StopCoroutine();
                break;
            case Unit_state.Hit:
                break;
            case Unit_state.Dash:
                ChangeState(Unit_state.Move);
                break;
            case Unit_state.Die:
                break;
            default:
                break;
        }        
    }
    private void ChangeState(Unit_state newState)
    {
        rigidbody.velocity = Vector2.zero;

        if (berserk)
        {            
            renderer.color = new Color(1f, 0.5f, 0.5f);
        }

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
                StartCoroutine(PercyAttack_co);
                break;
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Stun:
                break;
            case Unit_state.Dash:                
                BerserkMod();
                break;
            case Unit_state.Die:
                break;
        }
        
    }
    private void StopCoroutine()
    {
        StopCoroutine(PercyAttack_co);
        PercyAttack_co = PercyAttack_Co();
    }
    private void BerserkCheck()
    {
        if (Health<=2&&!berserk)
        {
            berserk = true;
            ChangeState(Unit_state.Dash);
        }
        
    }
    private void BerserkMod()
    {        
        Health += 2;
        renderer.color = new Color(1f, 0.5f, 0.5f);
        monsterMove.MoveSpeed += 2f;
    }

    private IEnumerator PercyAttack_Co()
    {
        rigidbody.velocity = Vector2.zero;
        float currentTime = 0f;
        monsterMove.PlayerDirectionCheck();
        animator.SetTrigger("Attack");

        while (currentTime < 0.8f)
        {            
            currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        currentTime = 0f;

        animator.SetTrigger("Default");

        Vector3 direction = (monsterMove.direction < 1) ? Vector3.left : Vector3.right;

        CreateBullet(direction);

        while (currentTime <2f)
        {
            monsterMove.TotalMove();
            currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        
        ChangeState(Unit_state.Move);
        rigidbody.velocity = Vector2.zero;
        yield return null;
    }

    private void CreateBullet(Vector3 direction)
    {
        GameObject bullet = Instantiate(Percy_FireBall_obj, shotPosi.transform.position, Quaternion.identity);
        Percy_FireBall bullet_C = bullet.GetComponent<Percy_FireBall>();
        if (berserk)
        {
            bullet_C.Speed = bullet_C.Speed * 1.5f;
            bullet_C.damage = bullet_C.damage + 2;
        }
        bullet_C.Start_Co(direction);
    }

    private void Tracy_PlayerCheck()
    {
        float targetDistance = monsterMove.DistanceAndDirection();

        if (targetDistance < 10f)
        {
            ChangeState(Unit_state.Attack);
            return;
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
