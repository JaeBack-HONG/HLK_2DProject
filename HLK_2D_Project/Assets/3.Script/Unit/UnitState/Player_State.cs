using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum Jump_State
{
    Default = 0,
    Idle,
    Jumping,
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
    public int MaxHealth;
    public float JumpForce;
    public float Stuncool = 2f;

    public Animator animator;
    private Player_Ability P_Ability;
    private Player_Move P_Move;
    public Rigidbody2D rigidbody;
    [SerializeField] private GameObject Stun_obj;

    public Vector2 direction;

    private IEnumerator Slow_Co;
    private IEnumerator Stun_Co;
    private IEnumerator Poison_Co;
    private IEnumerator Ignition_Co;

    public bool isAttack = false;
    public bool isGround = true;
    public bool isFairy = false;
    public bool isArmand = false;

    [SerializeField] CinemachineVirtualCamera V_camera;



    private void Awake()
    {
        GameManager.instance.SceneDataSave();
    }

    private void Start()
    {
        TryGetComponent<Rigidbody2D>(out rigidbody);
        TryGetComponent<Animator>(out animator);
        TryGetComponent<Player_Move>(out P_Move);
        PlayerDataSetting();
        actState = Unit_state.Idle;
        JumState = Jump_State.Idle;
        TryGetComponent<Unit_Hit>(out unithit);
        TryGetComponent<Player_Ability>(out P_Ability);
        Health = GameManager.instance.PlayerData.currentHealth;
        MaxHealth = GameManager.instance.PlayerData.maxHealth;
    }


    private void Update()
    {
        P_Ability.Choice_Ab();

        State_Check();

        direction = (transform.rotation.y.Equals(0)) ? Vector2.right : Vector2.left;
    }
    private void FixedUpdate()
    {

        IsFalling();
        GroundRayCheck();
        animator.SetFloat("YSpeed", P_Move.rigidbody.velocity.y);

    }

    private void PlayerDataSetting()
    {
        data = new UnitData(name: "Player", hp: 10, detection: 5, range: 1, attackSpeed: 1, strength: 2, moveSpeed: 8.5f, jumpForce: 15.5f);
        Health = data.HP;
        JumpForce = data.JumpForce;
    }

    private void State_Check()
    {

        Player_HealthCheck();

        //Debug.Log(actState)
        switch (actState)
        {
            case Unit_state.Default:
                break;
            case Unit_state.Idle:
                //P_Ability.UseAbsorb();
                P_Move.MoveCheck();
                break;
            case Unit_state.Move:
                //P_Ability.UseAbsorb();
                P_Move.MoveCheck();
                break;
            case Unit_state.Attack:
                if (P_Ability.current_Ab != null && !PlayerManager.instance.count_List[PlayerManager.instance.Select_Idx].Equals(0))
                {
                    P_Ability.current_Ab.UseAbility();
                }
                else actState = Unit_state.Idle;

                break;
            case Unit_state.Grab:
                IsGrab();
                break;
            case Unit_state.Stun:
                break;
            case Unit_state.Hit:
                unithit.Hit(gameObject.layer, transform.position,Condition_state.Default);
                break;
            case Unit_state.Die:
                if (!gameObject.layer.Equals((int)Layer_Index.Hit)) gameObject.layer = (int)Layer_Index.Hit;
                break;
        }
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
                if (isAttack) return;

                ChangeState(Jump_State.Idle);
                P_Move.jumpCount = P_Move.maxJumps;

                if (!actState.Equals(Unit_state.Idle)) actState = Unit_state.Idle;
            }
        }
    }

    public void IsFalling()
    {
        if (P_Move.rigidbody.velocity.y < -0.01f)
        {
            if (P_Move.jumpCount.Equals(P_Move.maxJumps)) P_Move.jumpCount--;

            if (!JumState.Equals(Jump_State.Falling)) ChangeState(Jump_State.Falling);
        }
    }

    public void Player_HealthCheck()
    {
        if (Health <= 0 && !actState.Equals(Unit_state.Die))
        {
            ActChangeState(Unit_state.Die);
            rigidbody.velocity = Vector2.zero;
        }
    }
    public void ActChangeState(Unit_state unitstate)
    {
        if (JumState.Equals(unitstate))
        {
            return;
        }
        actState = unitstate;

        switch (unitstate)
        {
            case Unit_state.Default:
                break;
            case Unit_state.Idle:
                break;
            case Unit_state.Move:
                break;
            case Unit_state.Attack:
                break;
            case Unit_state.Grab:
                break;
            case Unit_state.Hit:
                break;
            case Unit_state.Jump:
                break;
            case Unit_state.Die:
                StartCoroutine(PlayerDieEvent());
                
                break;
            case Unit_state.Stun:
                break;
            case Unit_state.Dash:
                break;
            case Unit_state.DashAttack:
                break;
            case Unit_state.Wait:
                break;
            default:
                break;
        }
    }

    public void ChangeState(Jump_State jumpstate)
    {
        if (JumState.Equals(jumpstate))
        {
            return;
        }
        JumState = jumpstate;

        switch (JumState)
        {
            case Jump_State.Idle:
                if (isArmand)
                {
                    animator.SetTrigger("Idle");
                }
                break;
            case Jump_State.Jumping:

                if (isArmand)
                {
                    animator.SetTrigger("ArmandJump");
                }

                break;
            case Jump_State.Falling:

                break;
            default:
                break;
        }
    }

    

    public void Attack(Monster_State other)
    {
        other.Health -= data.Strength;
        other.UnitHit.Hit(other.gameObject.layer, transform.position, Condition_state.Default);
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
        actState = Unit_state.Default;
        //오브젝트 키기
        Stun_obj.SetActive(true);

        yield return new WaitForSeconds(cool);

        Stun_obj.SetActive(false);
        actState = Unit_state.Idle;
        yield return null;
    }

    public void Poison(float cool,int damge)
    {
        Poison_Co = Poison_co(cool,damge);
        StartCoroutine(Poison_Co);
    }
    private IEnumerator Poison_co(float cool,int damage)
    {
        unithit.Hit(gameObject.layer, transform.position, Condition_state.Poison);
        Health -= damage;
        yield return new WaitForSeconds(cool);
        unithit.Hit(gameObject.layer, transform.position, Condition_state.Poison);
        Health -= damage;
        yield return new WaitForSeconds(cool);
        unithit.Hit(gameObject.layer, transform.position, Condition_state.Poison);
        Health -= damage;

        yield return null;
    }
    public void Ignition(float cool, int damge)
    {
        Ignition_Co = Ignition_co(cool, damge);
        StartCoroutine(Ignition_Co);
    }
    private IEnumerator Ignition_co(float cool, int damage)
    {
        unithit.Hit(gameObject.layer, transform.position, Condition_state.Ignition);
        Health -= damage;
        yield return new WaitForSeconds(cool);
        unithit.Hit(gameObject.layer, transform.position, Condition_state.Ignition);
        Health -= damage;
        yield return new WaitForSeconds(cool);
        unithit.Hit(gameObject.layer, transform.position, Condition_state.Ignition);
        Health -= damage;

        yield return null;
    }
    private IEnumerator PlayerDieEvent()
    {        
        V_camera.Priority = 30;
        gameObject.layer = (int)Layer_Index.Hit;
        yield return new WaitForSeconds(0.3f);
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(1.5f);
        GameManager.instance.PlayerDieUI();
    }
}
