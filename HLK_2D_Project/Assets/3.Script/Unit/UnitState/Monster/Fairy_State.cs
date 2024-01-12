using System.Collections;
using UnityEngine;

public class Fairy_State : Monster_State
{

    [Header("탐지 시간")]
    [SerializeField] private float detectionSet = 0;

    [SerializeField] float currentTime = 0f;

    [SerializeField] private Collider2D collider2;


    private void Start()
    {
        MonsterDataSetting();
    }
    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "Blankey", hp: healthSet, detection: 7, range: 5, attackSpeed: 0.5f,
                strength: damageSet, moveSpeed: speedSet, jumpForce: 1);
        Health = data.HP;
        Strength = data.Strength;
        state = Unit_state.Idle;
        ability_Item = Ability_Item.Fairy;

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
                TargetPlayerCheck();
                break;
            case Unit_state.Move:
                Blankey_PlayerDetection();
                break;
            case Unit_state.Attack:

                break;
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Stun:
                //여기서 모든 행동 및 코루틴 중지
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
                StartCoroutine(FairyDie());
                break;
        }
    }
    private void TargetPlayerCheck()
    {
        if (monsterMove.target)
        {
            state = Unit_state.Move;
        }
    }
    private void Blankey_PlayerDetection()
    {
        Transform player = monsterMove.targetPlayer;
        monsterMove.PlayerDirectionCheck();
        Vector2 playerDirection = (player.position - transform.position).normalized;

        rigidbody.velocity = playerDirection * data.MoveSpeed;

        currentTime += Time.deltaTime;

        if (currentTime >= detectionSet)
        {
            ChangeState(Unit_state.Die);
        }
    }
    private IEnumerator FairyDie()
    {
        collider2.enabled = false;
        currentTime = 0f;
        rigidbody.velocity = Vector2.zero;
        Color fairyColor = renderer.color;//
        while (currentTime < 3f)
        {
            currentTime += Time.fixedDeltaTime;

            fairyColor.a = Mathf.Lerp(1f, 0, currentTime / 3f);
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, fairyColor.a);
            yield return new WaitForFixedUpdate();
        }
        Health = 0;
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
