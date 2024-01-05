using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lil_Wiz_State : Monster_State
{
    WaitForSeconds cool = new WaitForSeconds(1f);
    WaitForSeconds WaitCool = new WaitForSeconds(1f);
    [SerializeField] private GameObject lil_WizMagic_obj;
    [SerializeField] private GameObject shotPosi;

    IEnumerator lil_WizAttack_co;

    private bool isCasting = false;
    

    private void Start()
    {
        MonsterDataSetting();
    }


    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "Lil_Wiz", hp: 4, detection: 7, range: 5, attackSpeed: 0.5f,
                strength: 1, moveSpeed: 2, jumpForce: 0);
        Health = data.HP;
        Strength = data.Strength;
        state = Unit_state.Move;
        ability_Item = Ability_Item.LilWiz;
        lil_WizAttack_co = Lil_WizAttack_co();
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
                    if (isCasting)
                    {
                        state = Unit_state.Move;
                        return;
                    }
                    lil_WizAttack_co = Lil_WizAttack_co();
                    StartCoroutine(lil_WizAttack_co);
                break;
            case Unit_state.Grab:
                    StopCoroutine(lil_WizAttack_co);
                    IsGrab();
                break;
            case Unit_state.Stun:
                
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

    private IEnumerator Lil_WizAttack_co()
    {
        isCasting = true;
        state = Unit_state.Idle;
        monsterMove.PlayerDirectionCheck();
        animator.SetTrigger("Shot");

        yield return WaitCool;
        
        Vector3 direction = (monsterMove.direction < 1) ? Vector3.left : Vector3.right;
        
        rigidbody.velocity = Vector2.zero;

        CreateBubble(direction);        

        animator.SetTrigger("Default");
        state = Unit_state.Idle;
        animator.SetBool("Move", false);
        yield return cool;

        
        state = Unit_state.Move;
        isCasting = false;
        yield return null;
    }

    private void CreateBubble(Vector3 direction)
    {
        GameObject bubble = Instantiate(lil_WizMagic_obj, shotPosi.transform.position, Quaternion.identity);
        Lil_Wiz_MagicBubble bubble_C = bubble.GetComponent<Lil_Wiz_MagicBubble>();
        bubble_C.Start_Co(direction);
        
        
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
