using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Jump_State
{
    Idle = 0,
    Jumping = 1,
    Falling,
}

public class Player_State : MonoBehaviour
{
    public UnitData data;

    public Unit_state actState;
    public Condition_state conState;
    public Jump_State JumState;

    public Unit_Hit unithit;

    public int Health;
    public float JumpForce;

    private Player_Ability P_Ability;
    private Player_Move P_Move;

    public Vector2 direction;
    public bool isAttack = false;

    private IEnumerator Slow_Co;
    private IEnumerator Stun_Co;

    private void Start()
    {
        TryGetComponent<Player_Move>(out P_Move);
        PlayerDataSetting();
        actState = Unit_state.Idle;
        JumState = Jump_State.Idle;
        TryGetComponent<Unit_Hit>(out unithit);
        TryGetComponent<Player_Ability>(out P_Ability);
    }


    private void Update()
    {

        P_Ability.Choice_Ab();
        State_Check();

        Player_HealthCheck();

        direction = (transform.rotation.y.Equals(0)) ? Vector2.right : Vector2.left;
    }

    private void PlayerDataSetting()
    {
        data = new UnitData(name: "Player", hp: 10, detection: 5, range: 1, attackSpeed: 1, strength: 2, moveSpeed: 10, jumpForce: 18);
        Health = data.HP;
        JumpForce = data.JumpForce;
    }

    private void State_Check()
    {
        switch (actState)
        {
            case Unit_state.Default:
                break;
            case Unit_state.Idle:
                P_Move.MoveCheck();
                if (Input.GetKeyDown(KeyCode.Z)) actState = Unit_state.Attack;
                break;
            case Unit_state.Move:
                P_Move.MoveCheck();
                if (Input.GetKeyDown(KeyCode.Z)) actState = Unit_state.Attack;
                break;
            case Unit_state.Attack:
                if (P_Ability.current_Ab != null && P_Ability &&
                    !PlayerManager.instance.count_List[PlayerManager.instance.current_Count].Equals(0))
                {
                    P_Ability.current_Ab.UseAbility();
                }
                else actState = Unit_state.Idle;

                break;
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Stun:
                //여기에 모든 행동 스탑 ex)실행중인 코루틴 등 정지
                break;
            case Unit_state.Hit:
                unithit.Hit(gameObject.layer, transform.position);
                break;
        }
        IsFalling();
        GroundRayCheck();
    }

    public void IsGrab()
    {
        P_Move.rigidbody.velocity = Vector2.zero;
        P_Move.rigidbody.gravityScale = 0f;
    }

    public void GroundRayCheck()
    {
        if (JumState.Equals(Jump_State.Falling))
        {
            RaycastHit2D hit1 = Physics2D.Raycast(new Vector3(transform.position.x + 0.45f, transform.position.y),
                Vector2.down, 0.75f, LayerMask.GetMask("Ground"));
            RaycastHit2D hit2 = Physics2D.Raycast(new Vector3(transform.position.x - 0.45f, transform.position.y),
                Vector2.down, 0.75f, LayerMask.GetMask("Ground"));

            if (hit1.collider != null || hit2.collider != null)
            {
                P_Move.jumpCount = P_Move.maxJumps;
                JumState = Jump_State.Idle;

                if (!actState.Equals(Unit_state.Idle)) actState = Unit_state.Idle;
            }
        }
    }

    public void IsFalling()
    {
        if (P_Move.rigidbody.velocity.y < -0.01f)
        {
            if (P_Move.jumpCount.Equals(P_Move.maxJumps)) P_Move.jumpCount--;

            if (!JumState.Equals(Jump_State.Falling) && !isAttack) JumState = Jump_State.Falling;
        }
    }

    public void Player_HealthCheck()
    {
        if (Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    public void Attack(Monster_State other)
    {
        other.Health -= data.Strength;
        other.UnitHit.Hit(other.gameObject.layer, transform.position);
    }

    public void Slow(float speed, float cool)
    {
        Slow_Co = Slow_co(speed, cool);
        StartCoroutine(Slow_Co);
    }


    private IEnumerator Slow_co(float speed, float cool)
    {
        float currentTime = 0;
        P_Move.moveSpeed *= speed;
        while (currentTime < cool)
        {
            currentTime += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }
        P_Move.moveSpeed = data.MoveSpeed;
    }
    public void Stun(float cool)
    {
        Stun_Co = Stun_co(cool);
        StartCoroutine(Stun_Co);
    }
    private IEnumerator Stun_co(float cool)
    {
        actState = Unit_state.Stun;
        float currentTime = 0f;
        while (currentTime < cool)
        {
            currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        actState = Unit_state.Move;
    }
}
