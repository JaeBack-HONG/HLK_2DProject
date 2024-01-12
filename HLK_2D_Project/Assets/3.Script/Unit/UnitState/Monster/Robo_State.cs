using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robo_State : Monster_State
{
    WaitForSeconds cool = new WaitForSeconds(1f);

    IEnumerator roboTransForm_co;

    [SerializeField] private GameObject armand_obj;

    private void Start()
    {
        MonsterDataSetting();
    }

    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "Robo", hp: healthSet, detection: 10, range: 2, attackSpeed: 1,
                strength: damageSet, moveSpeed: speedSet, jumpForce: 0);
        Health = data.HP;
        Strength = data.Strength;
        ability_Item = Ability_Item.BigRed;
        roboTransForm_co = RoboTransForm_Co();        
        base.MonsterDataSetting();
        ChangeState(Unit_state.Move);
    }
    private void FixedUpdate()
    {
        if (!state.Equals(Unit_state.Die))
        {
            Monster_HealthCheck();
        }

        switch (state)
        {
            case Unit_state.Idle:
                break;
            case Unit_state.Move:
                monsterMove.TotalMove();                
                break;
            case Unit_state.Attack:
                break;
            case Unit_state.Grab:
                break;
            case Unit_state.Stun:
                break;
            case Unit_state.Die:
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
                break;
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Stun:
                break;
            case Unit_state.Die:
                StartCoroutine(roboTransForm_co);
                break;
        }
    }

    private void StopCoroutine()
    {
        StopCoroutine(roboTransForm_co);        
        roboTransForm_co = RoboTransForm_Co();
        
    }
       



    #region //BigRed Attack 공격_코루틴
    private IEnumerator RoboTransForm_Co()
    {
        animator.SetTrigger("Transform");
        yield return cool;        
        Instantiate(armand_obj, transform.position, Quaternion.identity);
        base.Die();
        yield return null;
    }
    #endregion

    
    public override void Monster_HealthCheck()
    {
        if (Health <= 0)
        {
            ChangeState(Unit_state.Die);            
                        
        }
    }
}
