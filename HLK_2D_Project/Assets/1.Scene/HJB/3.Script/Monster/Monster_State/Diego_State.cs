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
    IEnumerator hopeAttack_co;

    private void Start()
    {
        MonsterDataSetting();
    }


    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "Hope1", hp: 4, detection: 7, range: 5, attackSpeed: 0.5f,
                strength: 1, moveSpeed: 2, jumpForce: 0);
        Health = data.HP;
        Strength = data.Strength;
        state = Unit_state.Move;
        ability_Item = Ability_Item.Diego;
        hopeAttack_co = HopeAttack_co();
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
                hopeAttack_co = HopeAttack_co();
                StartCoroutine(hopeAttack_co);
                break;
            case Unit_state.Grab:
                StopCoroutine(hopeAttack_co);
                IsGrab();
                break;
            case Unit_state.Stun:
                StopCoroutine(hopeAttack_co);
                break;
            case Unit_state.Hit:
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

    private IEnumerator HopeAttack_co()
    {
        state = Unit_state.Idle;
        monsterMove.PlayerDirectionCheck();
        animator.SetTrigger("Shot");

        Vector3 direction = (monsterMove.direction < 1) ? Vector3.left : Vector3.right;

        rigidbody.velocity = Vector2.zero;

        CreateBullet(direction);
        yield return tripleTime;
        CreateBullet(direction);
        yield return tripleTime;
        CreateBullet(direction);

        yield return cool;

        animator.SetTrigger("Reload");
        yield return WaitCool;
        animator.SetTrigger("Default");
        state = Unit_state.Move;

        yield return null;
    }

    private void CreateBullet(Vector3 direction)
    {
        GameObject bullet = Instantiate(diego_Bullet_obj, shotPosi.transform.position, Quaternion.identity);
        Diego_Bullet bullet_C = bullet.GetComponent<Diego_Bullet>();
        bullet_C.Start_Co(direction);
    }

    private void Hope_PlayerCheck()
    {
        float targetDistance = monsterMove.DistanceAndDirection();

        if (targetDistance < 10f)
        {
            state = Unit_state.Attack;
        }
    }

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
