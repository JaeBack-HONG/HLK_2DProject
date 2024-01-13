using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Holly_State : Monster_State
{
    private bool ChangeCheck = false;

    [Header("직접참조")]
    [SerializeField] private GameObject effect;
    [SerializeField] private GameObject attackboxobj;
    [SerializeField] private BoxCollider2D attackboxcol;
    [SerializeField] private CinemachineVirtualCamera cinemachinevir;
    
    private CinemachineBasicMultiChannelPerlin noise;

    [Header("수치조정")]
    [SerializeField] private int noiseScale = 10;
    [SerializeField] private int gimmickcount = 0;
    [SerializeField] private int maxcount = 5;


    IEnumerator holly_GimickAttack_co;
    IEnumerator holly_Attack_co;
    private void Start()
    {
        MonsterDataSetting();
        noise = cinemachinevir.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "BabyBoss", hp: 10, detection: 5, range: 2, attackSpeed: 1,
                strength: 2, moveSpeed: 5, jumpForce: 0);
        Health = data.HP;
        Strength = data.Strength;
        state = Unit_state.Move;
        ability_Item = Ability_Item.Holly;
        holly_Attack_co = HollyAttack_Co(noiseScale);
        holly_GimickAttack_co = SpecialAttack_Co();
        base.MonsterDataSetting();
        animator.SetTrigger("Default");
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
                BabyBossAttack_PlayerCheck();
                break;
            case Unit_state.Attack:                
                break;
            case Unit_state.Grab:                
                StopCoroutine();
                break;
            case Unit_state.Hit:
                break;
            case Unit_state.Stun:
                StopCoroutine();
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
                if (!gimmickcount.Equals(maxcount))
                {
                    holly_Attack_co = HollyAttack_Co(noiseScale);
                    StartCoroutine(holly_Attack_co);
                }
                else
                {
                    holly_GimickAttack_co = SpecialAttack_Co();
                    StartCoroutine(holly_GimickAttack_co);
                }
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
        StopCoroutine(holly_Attack_co);
        StopCoroutine(holly_GimickAttack_co);
        holly_Attack_co = HollyAttack_Co(noiseScale);
        holly_GimickAttack_co = SpecialAttack_Co();
    }

    private void BabyBossAttack_PlayerCheck()
    {
        float targetDistance = monsterMove.DistanceAndDirection();

        if (targetDistance < 2.5f)
        {
            ChangeState(Unit_state.Attack);
        }
    }

    #region //베이비 공격(Co)
    private IEnumerator HollyAttack_Co(int noisescale)
    {
        animator.SetBool("Move", false);
        rigidbody.velocity = Vector2.zero;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.6f/2);
        attackboxobj.SetActive(true);
        noise.m_AmplitudeGain = 0;
        animator.SetTrigger("Default");
        yield return new WaitForSeconds(0.2f);
        attackboxobj.SetActive(false);
        noise.m_AmplitudeGain = 0;
        yield return new WaitForSeconds(0.5f);
        ChangeState(Unit_state.Move);
        gimmickcount++;
    }
    #endregion

    #region 베이비 기믹
    private IEnumerator SpecialAttack_Co()
    {
        float currentTime = 0f;
        noise.m_AmplitudeGain = 0;        
        rigidbody.velocity = Vector2.zero;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.6f/2);
        attackboxobj.SetActive(true);
        
        yield return new WaitForSeconds(0.2f);
        attackboxobj.SetActive(false);
        
        yield return new WaitForSeconds(0.2f);
        animator.SetTrigger("Default");
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.65f/2);
        attackboxobj.SetActive(true);
        
        yield return new WaitForSeconds(0.25f);
        attackboxobj.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        animator.SetTrigger("Default");
        animator.SetTrigger("Gimick");
        
        rigidbody.AddForce(Vector2.up*12f, ForceMode2D.Impulse);
        Vector3 playerDirection = (Player.transform.position - transform.position).normalized;
        monsterMove.PlayerDirectionCheck();
        while (currentTime<0.8f)
        {            
            currentTime += Time.deltaTime;
            
            rigidbody.velocity = new Vector2(playerDirection.x*8f, rigidbody.velocity.y);           
            
            yield return null;
        }
        rigidbody.velocity = Vector2.zero;
        attackboxobj.SetActive(true);
        attackboxcol.size = new Vector2(5f, attackboxcol.size.y);
        effect.SetActive(true);
        noise.m_AmplitudeGain = 23;
        animator.SetBool("Move", false);
        yield return new WaitForSeconds(0.15f);
        attackboxobj.SetActive(false);
        attackboxcol.size = new Vector2(1.4f, attackboxcol.size.y);
        yield return new WaitForSeconds(0.15f);
        noise.m_AmplitudeGain = 0;
        yield return new WaitForSeconds(0.35f);
        effect.SetActive(false);
        ChangeState(Unit_state.Move);
        gimmickcount = 0;
    }
    #endregion
    

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
