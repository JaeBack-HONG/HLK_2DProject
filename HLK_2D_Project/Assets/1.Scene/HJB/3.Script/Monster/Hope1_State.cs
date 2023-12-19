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
        monsterMove.TotalMove();
        Hope_PlayerCheck();
        Monster_HealthCheck();
        HopeAttack();
    }
    public void HopeAttack()
    {
        if (state.Equals(Unit_state.Attack)&&!isAttack)
        {
            isAttack = true;
            StartCoroutine(HopeAttack_co());
        }
    }
    private IEnumerator HopeAttack_co()
    {
        GameObject bullet = GameObject.Instantiate(hope_Bullet, transform.position,Quaternion.identity);
        bullet.SetActive(true);
        //animator.SetTrigger("Attack");
        yield return cool;
        //animator.SetTrigger("Default");
        state = Unit_state.Move;
        isAttack = false;
        yield return null;
    }

    private void Hope_PlayerCheck()
    {
        float targetDistance = monsterMove.DistanceAndDirection();

        if (targetDistance > 2f && targetDistance <5f)
        {
            state = Unit_state.Attack;
        }
    }

    public override void Monster_HealthCheck()
    {
        if (data.HP <= 0)
        {
            base.Die();
        }
    }
}
