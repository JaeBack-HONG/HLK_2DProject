using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diego_State : Monster_State
{
    WaitForSeconds tripleTime = new WaitForSeconds(0.1f);
    WaitForSeconds cool = new WaitForSeconds(0.3f);
    WaitForSeconds WaitCool = new WaitForSeconds(1.5f);
    [SerializeField] private GameObject diego_Bullet_obj;
    [SerializeField] private GameObject shotPosi;
    IEnumerator diegoAttack_co;

    private void Start()
    {
        MonsterDataSetting();
    }


    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "Hope1", hp: healthSet, detection: 7, range: 5, attackSpeed: 0.5f,
                strength: damageSet, moveSpeed: speedSet, jumpForce: 0);
        Health = data.HP;
        Strength = data.Strength;
        state = Unit_state.Move;
        ability_Item = Ability_Item.Diego;
        diegoAttack_co = DiegoAttack_co();
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
                Diego_PlayerCheck();
                break;
            case Unit_state.Attack:                
                break;
            case Unit_state.Grab:
                IsGrab();
                StopCoroutine();
                break;
            case Unit_state.Stun:
                StopCoroutine();
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
                StartCoroutine(diegoAttack_co);
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

    private void StopCoroutine()
    {
        StopCoroutine(diegoAttack_co);
        diegoAttack_co = DiegoAttack_co();
    }
    private IEnumerator DiegoAttack_co()
    {        
        monsterMove.PlayerDirectionCheck();
        animator.SetTrigger("Shot");
        rigidbody.velocity = Vector2.zero;
        float currentTime =0f;
        while (currentTime < 0.5f)
        {
            currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        currentTime = 0f;
        Vector3 direction = (monsterMove.direction < 1) ? Vector3.left : Vector3.right;


        CreateBullet(direction);
        yield return tripleTime;
        CreateBullet(direction);
        yield return tripleTime;
        CreateBullet(direction);

        yield return cool;

        animator.SetTrigger("Reload");
        yield return WaitCool;
        animator.SetTrigger("Default");
        ChangeState(Unit_state.Move);

        yield return null;
    }

    private void CreateBullet(Vector3 direction)
    {
        GameObject bullet = Instantiate(diego_Bullet_obj, shotPosi.transform.position, Quaternion.identity);
        Diego_Bullet bullet_C = bullet.GetComponent<Diego_Bullet>();
        bullet_C.damage = data.Strength;
        bullet_C.Start_Co(direction);
    }

    private void Diego_PlayerCheck()
    {
        float targetDistance = monsterMove.DistanceAndDirection();

        if (targetDistance < 10f)
        {
            ChangeState(Unit_state.Attack);
        }
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
