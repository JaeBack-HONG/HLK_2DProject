using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handrick_state : Monster_State
{
    WaitForSeconds cool = new WaitForSeconds(0.9f);

    private bool rush = false;

    private float direction;

    private GameObject targetPlayer;

    IEnumerator rushAttack_co;
    private void Start()
    {
        MonsterDataSetting();
        
    }
    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "Knight1", hp:5, detection: 5, range: 2, attackSpeed: 1,
                strength: 2, moveSpeed: 3, jumpForce: 0);
        Health = data.HP;
        Strength = data.Strength;
        state = Unit_state.Move;
        ability_Item = Ability_Item.Handrick;
        rushAttack_co = HandrickAttack_co();
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
                HandrickAttack_PlayerCheck();
                break;
            case Unit_state.Attack:
                rushAttack_co = HandrickAttack_co();
                StartCoroutine(rushAttack_co);
                break;
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Hit:
                break;            
            case Unit_state.Dash:
                RushGroundCheck();
                break;
            case Unit_state.Stun:
                
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
    private void HandrickAttack_PlayerCheck()
    {
        if (monsterMove.target)
        {            
            targetPlayer = Player.gameObject;
            direction = (transform.localPosition.x - targetPlayer.transform.localPosition.x);
            direction = (direction < 0) ? 1 : -1;            
            state = Unit_state.Attack;
        }
    }

    private IEnumerator HandrickAttack_co()
    {
        animator.SetTrigger("Charge");
        state = Unit_state.Dash;
        rush = true;
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("Rush");
        while (rush)
        {               
            rigidbody.velocity = new Vector2(direction * 10f, rigidbody.velocity.y);
            yield return null;
        }
        state = Unit_state.Default;
        animator.SetTrigger("Default");
        yield return new WaitForSeconds(0.5f);
        state = Unit_state.Move;
    }


    private void RushGroundCheck()
    {
        LayerMask ground = LayerMask.GetMask("Ground");
        Debug.DrawRay(transform.position, new Vector2(direction * 1f, 0) * 3f, Color.black);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(direction*1f,0), 4f,ground);
        if (hit.collider!=null)
        {
            rush = false;            
        }
    }    

    public override void Monster_HealthCheck()
    {
        if (Health <= 0)
        {
            StopCoroutine(rushAttack_co);            
            base.Die();
            GameObject ability_obj = Instantiate(Ability_Item_obj, transform.position, Quaternion.identity);
            ability_obj.GetComponent<AbilityItem>().itemidx = ability_Item;
        }
    }
}
