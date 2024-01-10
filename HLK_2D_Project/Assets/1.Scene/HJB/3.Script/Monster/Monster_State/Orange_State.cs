using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orange_State : Monster_State
{
      

    private float direction;

    private GameObject targetPlayer;

    IEnumerator orangeAttack_co;

    [Header("������ �ӵ�")]
    [SerializeField] private float rollingSpeed = 0f;

    [Header("���۰� �� �ִϸ��̼� �ð�")]
    [SerializeField] private float startTimeSet = 0f;
    [SerializeField] private float stopTimeSet = 0f;

    private void Start()
    {
        MonsterDataSetting();        
    }
    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "Orange", hp: 5, detection: 5, range: 2, attackSpeed: 1,
                strength: 2, moveSpeed: 3, jumpForce: 0);
        Health = data.HP;
        Strength = data.Strength;
        state = Unit_state.Move;
        ability_Item = Ability_Item.Orange;
        orangeAttack_co = Orange_Attack_Co();
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
            case Unit_state.Idle:
                break;
            case Unit_state.Move:                
                monsterMove.TotalMove();
                OrangeAttack_PlayerCheck();
                break;
            case Unit_state.Attack:                
                break;
            case Unit_state.Grab:
                IsGrab();
                StopCoroutine();
                break;            
            case Unit_state.Hit:
                break;
            case Unit_state.Stun:
                StopCoroutine();
                rigidbody.velocity = Vector2.zero;
                animator.SetTrigger("Stop");
                animator.SetTrigger("Default");
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

        state = newState;

        StopCoroutine();

        switch (state)
        {

            case Unit_state.Idle:
                break;
            case Unit_state.Move:
                break;
            case Unit_state.Attack:
                StartCoroutine(orangeAttack_co);
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
        StopCoroutine(orangeAttack_co);
        orangeAttack_co = Orange_Attack_Co();
    }
    private void OrangeAttack_PlayerCheck()
    {
        if (monsterMove.target)
        {
            targetPlayer = Player.gameObject;
            direction = (transform.localPosition.x - targetPlayer.transform.localPosition.x);
            direction = (direction < 0) ? 1 : -1;
            ChangeState(Unit_state.Attack);
        }
    }
    private IEnumerator Orange_Attack_Co()
    {        
        float currentTime = 0f;
        rolling = true;
        animator.SetTrigger("Start");
        animator.SetBool("Move", false);
        while (currentTime<startTimeSet)
        {
            currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        currentTime = 0f;

        animator.SetTrigger("Attack");
        while (rolling)
        {
            OrangeRollingStopCheck();
            rigidbody.velocity = new Vector2(direction * rollingSpeed, rigidbody.velocity.y);
            yield return new WaitForFixedUpdate();
        }        
        animator.SetTrigger("Stop");
        while (currentTime < stopTimeSet)
        {
            currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        currentTime = 0f;
        
        while (currentTime < 1f)
        {
            currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        ChangeState(Unit_state.Move);
        yield return new WaitForFixedUpdate();
    }
    
    private void OrangeRollingStopCheck()
    {
        LayerMask StopCheckMask = LayerMask.GetMask("Player","Ground");

        Debug.DrawRay(new Vector3(transform.position.x,transform.position.y-0.2f),
            new Vector3(direction, 0) * 1f, Color.green);

        RaycastHit2D hit =
            Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - 0.2f), new Vector2(direction, 0), 1f, StopCheckMask);

        if (hit.collider != null)
        {
            Player_State p_state = hit.collider.gameObject.GetComponent<Player_State>();
            //������ ����
            Debug.Log("������!!!");

            rolling = false;
        }


    }
    
    public override void Monster_HealthCheck()
    {
        if (Health <= 0)
        {
            ChangeState(Unit_state.Die);
            base.Die();
            GameObject ability_obj = Instantiate(Ability_Item_obj, transform.position, Quaternion.identity);
            ability_obj.GetComponent<AbilityItem>().itemidx = ability_Item;
        }
    }
}
