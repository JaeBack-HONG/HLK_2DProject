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
                StopCoroutine(holly_Attack_co);
                StopCoroutine(holly_GimickAttack_co);
                break;
            case Unit_state.Hit:
                break;
            case Unit_state.Stun:
                StopCoroutine(holly_Attack_co);
                StopCoroutine(holly_GimickAttack_co);
                break;
            case Unit_state.Jump://변신상태로 일단                 
                break;
            default:
                break;
        }

        if (!state.Equals(Unit_state.Default)) Monster_HealthCheck();
    }

    private void BabyBossAttack_PlayerCheck()
    {
        float targetDistance = monsterMove.DistanceAndDirection();

        if (targetDistance < 2.5f)
        {
            state = Unit_state.Attack;
        }
    }

    #region //베이비 공격(Co)
    private IEnumerator HollyAttack_Co(int noisescale)
    {
        state = Unit_state.Default;
        rigidbody.velocity = Vector2.zero;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.6f/2);
        attackboxobj.SetActive(true);
        noise.m_AmplitudeGain = 0;
        yield return new WaitForSeconds(0.2f);
        attackboxobj.SetActive(false);
        noise.m_AmplitudeGain = 0;
        animator.SetTrigger("Default");
        yield return new WaitForSeconds(0.5f);
        state = Unit_state.Move;
        gimmickcount++;
    }
    #endregion

    #region 베이비 기믹
    private IEnumerator SpecialAttack_Co()
    {
        float currentTime = 0f;
        noise.m_AmplitudeGain = 0;
        state = Unit_state.Default;
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
        
        rigidbody.AddForce(Vector2.up*7f, ForceMode2D.Impulse);
        while (currentTime<0.8f)
        {            
            currentTime += Time.deltaTime;
            float step = 10f * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, step);
            
            yield return null;
        }
        rigidbody.velocity = Vector2.zero;
        attackboxobj.SetActive(true);
        attackboxcol.size = new Vector2(5f, attackboxcol.size.y);
        effect.SetActive(true);
        noise.m_AmplitudeGain = 23;
        yield return new WaitForSeconds(0.15f);
        attackboxobj.SetActive(false);
        attackboxcol.size = new Vector2(1.4f, attackboxcol.size.y);
        yield return new WaitForSeconds(0.15f);
        noise.m_AmplitudeGain = 0;
        yield return new WaitForSeconds(0.35f);
        animator.SetTrigger("Default");
        effect.SetActive(false);
        state = Unit_state.Move;
        gimmickcount = 0;
    }
    #endregion
    

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
