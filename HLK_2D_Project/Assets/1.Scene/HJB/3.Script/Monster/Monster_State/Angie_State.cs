using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angie_State : Monster_State
{    

    WaitForSeconds cool = new WaitForSeconds(0.25f);

    IEnumerator AngieAttack_co;
    IEnumerator AngieRun_co;

    [SerializeField] private GameObject Angie_Bullet_obj;
    [SerializeField] private GameObject shotPosi;

    [SerializeField] private float runSetTime = 0f;

    private void Start()
    {
        MonsterDataSetting();
    }

    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "Angie", hp: healthSet, detection: 10, range: 2, attackSpeed: 1,
                strength: damageSet, moveSpeed: speedSet, jumpForce: 1);
        Health = data.HP;
        Strength = data.Strength;
        ability_Item = Ability_Item.Angie;
        AngieAttack_co = AngieAttack_Co();
        AngieRun_co = AngieRun_Co();
        base.MonsterDataSetting();
        ChangeState(Unit_state.Move);
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
                Angie_RunCheck();
                break;
            case Unit_state.Attack:
                break;
            case Unit_state.Grab:
                break;
            case Unit_state.Stun:                
                break;
            case Unit_state.Dash:
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
                StartCoroutine(AngieAttack_co);
                break;
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Stun:                
                break;
            case Unit_state.Dash:
                StartCoroutine(AngieRun_co);
                break;
            case Unit_state.Die:
                break;
        }
    }

    private void StopCoroutine()
    {
        StopCoroutine(AngieRun_co);
        StopCoroutine(AngieAttack_co);
        AngieRun_co = AngieRun_Co();
        AngieAttack_co = AngieAttack_Co();
    }


    private void Angie_RunCheck()
    {        
        if (monsterMove.target)
        {
            ChangeState(Unit_state.Dash);
        }
    }

    private IEnumerator AngieRun_Co()
    {
        float currentTime = 0f;
        while (currentTime < runSetTime)
        {
            
            currentTime += Time.fixedDeltaTime;
            monsterMove.direction = (monsterMove.targetPlayer.localPosition.x - transform.localPosition.x);
            monsterMove.direction = (monsterMove.direction >= 0) ? -1 : 1;
            if (monsterMove.direction < 1)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            rigidbody.velocity = new Vector2(monsterMove.direction*data.MoveSpeed-1f, rigidbody.velocity.y);
            yield return new WaitForFixedUpdate();
        }
        float targetDistance = monsterMove.DistanceAndDirection();


        Debug.Log(targetDistance);
        if (targetDistance > 4f)
        {
            ChangeState(Unit_state.Attack);
        }
        else
        {
            ChangeState(Unit_state.Move);
        }
        //Angie_PlayerCheck();
        yield return new WaitForFixedUpdate();
    }

    #region //Angie 플레이어 공격사거리 탐지
    private void Angie_PlayerCheck()
    {
        float targetDistance = monsterMove.DistanceAndDirection();


        Debug.Log(targetDistance);
        if (targetDistance > 4f)
        {
            ChangeState(Unit_state.Attack);
        }
        else
        {
            ChangeState(Unit_state.Move);
        }
    }
    #endregion



    #region //BigRed Attack 공격_코루틴
    private IEnumerator AngieAttack_Co()
    {
        monsterMove.PlayerDirectionCheck();
        rigidbody.velocity = Vector2.zero;
        animator.SetTrigger("Attack");
        Vector3 direction = (monsterMove.direction < 1) ? Vector3.left : Vector3.right;
        
        CreateBullet(direction);
        yield return cool;
        animator.SetTrigger("Default");
        yield return new WaitForSeconds(1f);
        ChangeState(Unit_state.Move);
        yield return null;
    }
    #endregion
    private void CreateBullet(Vector3 direction)
    {

        GameObject bullet = Instantiate(Angie_Bullet_obj, shotPosi.transform.position, Quaternion.identity);
        Angie_Bullet bullet_C = bullet.GetComponent<Angie_Bullet>();
        bullet_C.damage = data.Strength;
        bullet_C.Start_Co(direction);
    }

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
