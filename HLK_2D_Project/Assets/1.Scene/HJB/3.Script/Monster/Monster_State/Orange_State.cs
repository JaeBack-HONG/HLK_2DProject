using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orange_State : Monster_State
{
      

    private float direction;

    private GameObject targetPlayer;

    IEnumerator orangeAttack_co;

    [Header("구르기 속도")]
    [SerializeField] private float rollingSpeed = 0f;

    [Header("시작과 끝 애니메이션 시간")]
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
                OrangeAttack_PlayerCheck();
                break;
            case Unit_state.Attack:
                orangeAttack_co = Orange_Attack_Co();
                StartCoroutine(orangeAttack_co);
                break;
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Dash:
                break;
            case Unit_state.Hit:
                break;
            case Unit_state.Stun:
                StopCoroutine(orangeAttack_co);
                animator.SetTrigger("Default");
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
    private void OrangeAttack_PlayerCheck()
    {
        if (monsterMove.target)
        {
            targetPlayer = Player.gameObject;
            direction = (transform.localPosition.x - targetPlayer.transform.localPosition.x);
            direction = (direction < 0) ? 1 : -1;
            state = Unit_state.Attack;
        }
    }
    private IEnumerator Orange_Attack_Co()
    {
        state = Unit_state.Dash;
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
        state = Unit_state.Idle;
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
        state = Unit_state.Move;
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
            //던지기 로직
            Debug.Log("던지기!!!");

            rolling = false;
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
