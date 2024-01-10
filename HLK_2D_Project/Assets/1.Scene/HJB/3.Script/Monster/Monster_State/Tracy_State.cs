using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracy_State : Monster_State
{
    WaitForSeconds cool = new WaitForSeconds(1.2f);
    WaitForSeconds WaitCool = new WaitForSeconds(1f);
    [SerializeField] private GameObject Tracy_Bow_obj;
    [SerializeField] private GameObject shotPosi;
    IEnumerator TracyAttack_co;

    private void Start()
    {
        MonsterDataSetting();
    }


    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "Tracy", hp: 4, detection: 7, range: 5, attackSpeed: 0.5f,
                strength: 1, moveSpeed: 2, jumpForce: 0);
        Health = data.HP;
        Strength = data.Strength;
        state = Unit_state.Move;
        ability_Item = Ability_Item.Tracy;
        TracyAttack_co = TracyAttack_Co();
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
                Tracy_PlayerCheck();
                monsterMove.TotalMove();
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
                StartCoroutine(TracyAttack_co);
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
        StopCoroutine(TracyAttack_co);
        TracyAttack_co = TracyAttack_Co();
    }

private IEnumerator TracyAttack_Co()
    {        
        rigidbody.velocity = Vector2.zero;
        monsterMove.PlayerDirectionCheck();
        animator.SetTrigger("Shot");

        yield return WaitCool;

        Vector3 direction = (monsterMove.direction < 1) ? Vector3.left : Vector3.right;


        CreateBullet(direction);
        
        animator.SetTrigger("Reload");
        yield return cool;

        animator.SetTrigger("Default");
        ChangeState(Unit_state.Move);

        yield return null;
    }

    private void CreateBullet(Vector3 direction)
    {
        GameObject bullet = Instantiate(Tracy_Bow_obj, shotPosi.transform.position, Quaternion.identity);
        Mon_Tracy_Arrow bullet_C = bullet.GetComponent<Mon_Tracy_Arrow>();
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
            GameObject ability_obj = Instantiate(Ability_Item_obj, transform.position, Quaternion.identity);
            ability_obj.GetComponent<AbilityItem>().itemidx = ability_Item;
            base.Die();
        }
    }
}
