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
                    Hope_PlayerCheck();
                break;
            case Unit_state.Attack:                                       
                break;
            case Unit_state.Grab:                    
                    IsGrab();
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

        StopCoroutine();

        state = newState;

        switch (state)
        {

            case Unit_state.Idle:
                break;
            case Unit_state.Move:
                break;
            case Unit_state.Attack:
                StartCoroutine(lil_WizAttack_co);
                break;
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Stun:
                break;
            case Unit_state.Die:
                break;
        }
    }
    private void StopCoroutine()
    {
        StopCoroutine(lil_WizAttack_co);
        lil_WizAttack_co = Lil_WizAttack_co();
    }
    private IEnumerator Lil_WizAttack_co()
    {              
        monsterMove.PlayerDirectionCheck();
        animator.SetTrigger("Shot");

        yield return WaitCool;
        
        Vector3 direction = (monsterMove.direction < 1) ? Vector3.left : Vector3.right;
        
        rigidbody.velocity = Vector2.zero;

        CreateBubble(direction);        

        animator.SetBool("Move", false);
        animator.SetTrigger("Default");
        
        yield return cool;
        ChangeState(Unit_state.Move);        
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
