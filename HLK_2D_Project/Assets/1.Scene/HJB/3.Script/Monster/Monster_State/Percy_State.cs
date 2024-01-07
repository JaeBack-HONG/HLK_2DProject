using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Percy_State : Monster_State
{
    WaitForSeconds cool = new WaitForSeconds(1.2f);
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
        switch (state)
        {
            case Unit_state.Default:
                break;

            case Unit_state.Idle:
                break;
            case Unit_state.Move:
                Tracy_PlayerCheck();

                break;
            case Unit_state.Attack:
                PercyAttack_co = PercyAttack_Co();
                StartCoroutine(PercyAttack_co);
                break;
            case Unit_state.Grab:
                StopCoroutine(PercyAttack_co);
                IsGrab();
                break;
            case Unit_state.Stun:
                StopCoroutine(PercyAttack_co);
                break;
            case Unit_state.Hit:
                break;
            case Unit_state.Die:
                break;
            default:
                break;
        }
        if (!berserk)
        {
            BerserkCheck();
        }
        
        if (!state.Equals(Unit_state.Default))
        {
            Monster_HealthCheck();
        }
    }
    private void BerserkCheck()
    {
        if (Health<=2)
        {
            berserk = true;
        }

        if (berserk)
        {
            BerserkMod();
        }
    }
    private void BerserkMod()
    {
        renderer.color = new Color(255f, 155f, 155);
        Health += 2;
    }

    private IEnumerator PercyAttack_Co()
    {
        animator.SetBool("Move", false);
        state = Unit_state.Idle;
        monsterMove.PlayerDirectionCheck();
        animator.SetTrigger("Attack");

        yield return WaitCool;
        animator.SetTrigger("Default");

        Vector3 direction = (monsterMove.direction < 1) ? Vector3.left : Vector3.right;

        rigidbody.velocity = Vector2.zero;

        CreateBullet(direction);


        yield return cool;
        
        yield return cool;
        state = Unit_state.Move;

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
            state = Unit_state.Attack;
            return;
        }
        monsterMove.TotalMove();
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
