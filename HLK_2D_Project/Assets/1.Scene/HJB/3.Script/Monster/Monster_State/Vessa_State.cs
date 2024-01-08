using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vessa_State : Monster_State
{
    RaycastHit2D hit;

    private int revivalCount = 2;
    private void Start()
    {
        MonsterDataSetting();
    }

    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "Vessa", hp: 4, detection: 7, range: 5, attackSpeed: 0.5f,
                strength: 1, moveSpeed: 2, jumpForce: 0);
        Health = data.HP;
        Strength = data.Strength;
        state = Unit_state.Move;
        ability_Item = Ability_Item.Vessa;        
        base.MonsterDataSetting();
    }
    private void FixedUpdate()
    {

        switch (state)
        {
            case Unit_state.Default:
                monsterMove.TotalMove();
                break;

            case Unit_state.Idle:
                break;
            case Unit_state.Move:
                monsterMove.TotalMove();
                Vessa_GroundCheck();
                VessaJump_Check();                
                break;
            case Unit_state.Attack:
                
                break;
            case Unit_state.Grab:                
                IsGrab();
                break;
            case Unit_state.Stun:                
                break;
            case Unit_state.Hit:
                break;
            case Unit_state.Jump:
                monsterMove.TotalMove();
                Vessa_GroundCheck();
                break;
            default:
                break;
        }

        

        if (!state.Equals(Unit_state.Default))
        {
            Monster_HealthCheck();
        }
    }    

    private void Vessa_GroundCheck()
    {
        LayerMask groundMask = LayerMask.GetMask("Ground");

        Debug.DrawRay(transform.position, Vector2.down * 1.5f, Color.black);
        hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, groundMask);
        if (hit.collider != null)
        {
            state = Unit_state.Move;
        }
    }
    private void VessaJump_Check()
    {        
        if (monsterMove.target && hit.collider != null)
        {
            state = Unit_state.Jump;
            int randomJump = Random.Range(6, 8);
            rigidbody.AddForce(Vector2.up * randomJump, ForceMode2D.Impulse);            
        }        
    }
        

    public override void Monster_HealthCheck()
    {
        if (Health <= 0 && revivalCount > 0 )
        {
            Health = 4;

            switch (revivalCount)
            {
                case 2:
                    transform.localScale = new Vector3(0.7f, 0.7f, 1);
                    rigidbody.mass = 5f;
                    break;
                case 1:
                    transform.localScale = new Vector3(0.5f, 0.5f, 1);
                    rigidbody.mass = 10f;
                    break;                
            }
            revivalCount--;
        }

        if (Health <= 0 && revivalCount.Equals(0))
        {
            base.Die();
            GameObject ability_obj = Instantiate(Ability_Item_obj, transform.position, Quaternion.identity);
            ability_obj.GetComponent<AbilityItem>().itemidx = ability_Item;
        }
    }
}
