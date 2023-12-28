using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BabyBoss_State : Monster_State
{
    private bool ChangeCheck = false;

    [SerializeField] private GameObject effect;

    [SerializeField] private CinemachineVirtualCamera cinemachinevir;
    private CinemachineBasicMultiChannelPerlin noise;

    public int gimmickcount = 0;

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
        state = Unit_state.Default;
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
                monsterMove.target = true;
                monsterMove.TotalMove();
                BabyBossAttack_PlayerCheck();
                break;
            case Unit_state.Attack:
                if (!gimmickcount.Equals(5))
                {
                    StartCoroutine(BabyBossAttack_co(10));
                }
                else
                {
                    StartCoroutine(SpecialAttack_co());
                }
                break;
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Hit:
                break;
            case Unit_state.Jump://변신상태로 일단 
                StartCoroutine(Transformation_co());
                break;
            default:
                break;
        }

        if (!state.Equals(Unit_state.Default))
        {
            Monster_HealthCheck();
        }
    }

    private void BabyBossAttack_PlayerCheck()
    {
        float targetDistance = monsterMove.DistanceAndDirection();

        if (targetDistance < 3f)
        {
            state = Unit_state.Attack;
        }
    }

    #region //베이비 공격(Co)
    private IEnumerator BabyBossAttack_co(int noisescale)
    {
        state = Unit_state.Default;
        rigidbody.velocity = Vector2.zero;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.6f);
        noise.m_AmplitudeGain = noisescale;
        yield return new WaitForSeconds(0.2f);
        noise.m_AmplitudeGain = 0;
        animator.SetTrigger("Walk");
        yield return new WaitForSeconds(0.5f);
        state = Unit_state.Move;
        gimmickcount++;
    }
    #endregion
    #region 베이비 기믹
    private IEnumerator SpecialAttack_co()
    {
        state = Unit_state.Default;
        rigidbody.velocity = Vector2.zero;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.6f);
        noise.m_AmplitudeGain = 10;
        yield return new WaitForSeconds(0.2f);
        noise.m_AmplitudeGain = 0;
        yield return new WaitForSeconds(0.2f);
        animator.SetTrigger("Walk");
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.65f);
        noise.m_AmplitudeGain = 12;
        yield return new WaitForSeconds(0.25f);
        noise.m_AmplitudeGain = 0;
        yield return new WaitForSeconds(0.1f);
        animator.SetTrigger("Walk");
        animator.SetTrigger("Gimmick");
        yield return new WaitForSeconds(0.95f);
        effect.SetActive(false);
        effect.SetActive(true);
        noise.m_AmplitudeGain = 23;
        yield return new WaitForSeconds(0.35f);

        noise.m_AmplitudeGain = 0;
        yield return new WaitForSeconds(0.2f);
        animator.SetTrigger("Walk");
        state = Unit_state.Move;
        gimmickcount = 0;
    }
    #endregion

    #region// 베이비 변신(Co)
    private IEnumerator Transformation_co()
    {

        state = Unit_state.Default;
        animator.SetTrigger("Transformation");
        yield return new WaitForSeconds(2f);
        ChangeCheck = true;
        animator.SetBool("Change", ChangeCheck);
        state = Unit_state.Attack;
    }
    #endregion


    public override void Monster_HealthCheck()
    {
        if (Health <= 0)
        {
            base.Die();
        }
    }
}
