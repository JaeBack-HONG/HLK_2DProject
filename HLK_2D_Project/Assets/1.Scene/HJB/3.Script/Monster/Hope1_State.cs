using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hope1_State : Monster_State
{
    WaitForSeconds cool = new WaitForSeconds(2.5f);
    [SerializeField] private GameObject hope_Bullet;
    private void Start()
    {
        MonsterDataSetting();
    }


    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "Hope1", hp: 3, detection: 7, range: 5, attackSpeed: 0.5f,
                strength: 1, moveSpeed: 2, jumpForce: 0);
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
                Hope_PlayerCheck();
                break;
            case Unit_state.Attack:
                StartCoroutine(HopeAttack_co());
                break;
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Hit:
                break;
            case Unit_state.Jump:
                break;
            default:
                break;
        }

        Monster_HealthCheck();
    }

    private IEnumerator HopeAttack_co()
    {
        state = Unit_state.Idle;
        rigidbody.velocity = Vector2.zero;
        CreateBullet();
        //animator.SetTrigger("Attack");
        yield return cool;
        //animator.SetTrigger("Default");
        state = Unit_state.Move;
        yield return null;
    }

    private void CreateBullet()
    {
        GameObject bullet = GameObject.Instantiate(hope_Bullet, transform.position, Quaternion.identity);
        bullet.SetActive(true);
    }

    private void Hope_PlayerCheck()
    {
        float targetDistance = monsterMove.DistanceAndDirection();

        if (targetDistance > 2f && targetDistance < 5f)
        {
            state = Unit_state.Attack;
        }
    }

    public override void Monster_HealthCheck()
    {
        if (Health <= 0)
        {
            base.Die();
        }
    }
}
