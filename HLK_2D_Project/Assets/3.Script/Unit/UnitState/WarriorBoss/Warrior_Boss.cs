using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior_Boss : Monster_State
{
    [Header("세부 설정")]
    [SerializeField] private float DashSpeed = 10f;
    [Range(5f, 50f)]
    [SerializeField] private float DashDistanceCheck = 10f;
    [SerializeField] private float detectionTime = 5f;
    [Header("화살 오브젝트")]
    [SerializeField] private GameObject arrow_R;
    [SerializeField] private GameObject arrow_L;
    [Header("타일 오브젝트")]
    [SerializeField] private GameObject Tile_obj;
    private float currentTime;
    private int P_DefaultHP = 0;
    private bool berserkMod = false;
    IEnumerator warriorDash_co;
    IEnumerator warriorAttack_co;
    IEnumerator warriorDashAttack_co;
    IEnumerator warriorArrowGimmick_co;
    private void Start()
    {
        MonsterDataSetting();
    }
    public override void MonsterDataSetting()
    {
        data = new UnitData
            (name: "Warrior", hp: healthSet, detection: 10, range: 2, attackSpeed: 1,
                strength: damageSet, moveSpeed: speedSet, jumpForce: 0);
        Health = data.HP;
        warriorDash_co = WarriorDash_Co();
        warriorAttack_co = WarriorAttack_Co();
        warriorDashAttack_co = WarriorDashAttack_Co();
        warriorArrowGimmick_co = WarriorArrowGimmick_Co();
        state = Unit_state.Move;
        base.MonsterDataSetting();
    }
    private void FixedUpdate()
    {
        if (!CameraControll_Warrior.Instance.start)
        {
            return;
        }
        if (!state.Equals(Unit_state.Die))
        {
            Monster_HealthCheck();
        }
        if (Health <= 18&&!berserkMod)
        {
            berserkMod = true;
            ChangeState(Unit_state.Jump);
        }
        switch (state)
        {
            case Unit_state.Default:
                break;
            case Unit_state.Idle:
                break;
            case Unit_state.Move:
                WarriorMove();
                Warrior_PlayerCheck();
                DetectPlayer();                
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

            case Unit_state.Default:
                break;
            case Unit_state.Idle:
                break;
            case Unit_state.Move:                
                break;
            case Unit_state.Attack:
                StartCoroutine(warriorAttack_co);
                break;
            case Unit_state.Grab:
                break;
            case Unit_state.Hit:
                break;
            case Unit_state.Jump:
                StartCoroutine(warriorArrowGimmick_co);
                break;
            case Unit_state.Die:
                break;
            case Unit_state.Stun:
                break;
            case Unit_state.Dash:
                StartCoroutine(warriorDash_co);
                break;
            case Unit_state.DashAttack:
                StartCoroutine(warriorDashAttack_co);
                break;
            case Unit_state.Wait:
                break;
            default:
                break;
        }
    }
    private void WarriorMove()
    {
        float direction = (Player.transform.localPosition.x - transform.localPosition.x);
        direction = (direction >= 0) ? 1 : -1;
        animator.SetBool("Move", true);
        if (direction < 1)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        rigidbody.velocity = new Vector2(direction * speedSet, rigidbody.velocity.y);
    }
    private void StopCoroutine()
    {
        StopCoroutine(warriorDashAttack_co);
        StopCoroutine(warriorAttack_co);
        StopCoroutine(warriorDash_co);
        warriorDashAttack_co = WarriorDashAttack_Co();
        warriorAttack_co = WarriorAttack_Co();
        warriorDash_co = WarriorDash_Co();
    }

    public void DetectPlayer()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);
        if (Player == null)
        {
            return;
        }
        //타겟이 True가 아니라면 현재 플레이어의 체력을 계속 담기
        if (monsterMove.target && P_DefaultHP.Equals(0))
        {
            P_DefaultHP = Player.Health;

        }
        //타겟이 True라면 플레이어 현재 체력과 이전 체력 비교 10초간 확인
        if (monsterMove.target)
        {
            currentTime += Time.deltaTime;
            int P_CurrentHP = Player.Health;
            //Debug.Log($"디폴 체력 : {P_DefaultHP}, 현재체력 : {P_CurrentHP}, 탐지 : {currentTime} ");
            if (currentTime > detectionTime)
            {
                if (P_CurrentHP.Equals(P_DefaultHP)&&distance>DashDistanceCheck)
                {
                    int randomGimmick = Random.Range(0, 3);
                    if (berserkMod&&randomGimmick.Equals(1))
                    {
                        ChangeState(Unit_state.Jump);
                    }
                    else
                    {
                        ChangeState(Unit_state.Dash);
                    }
                }
                //10초간 유지 후 탐지 되면 울프를 공격상태로 변경
                currentTime = 0;
                P_DefaultHP = 0;
            }
        }
    }
    private void Warrior_PlayerCheck()
    {
        float targetDistance = monsterMove.DistanceAndDirection();        
        if (targetDistance < 3f)
        {
            ChangeState(Unit_state.Attack);
        }

    }
    private IEnumerator WarriorAttack_Co()
    {
        animator.SetTrigger("Attack");
        rigidbody.velocity = Vector2.zero;
        animator.SetTrigger("Default");        
        animator.SetBool("Move",false);
        yield return new WaitForSeconds(1.2f);
        
        ChangeState(Unit_state.Move);
    }
    
    private IEnumerator WarriorDashAttack_Co()
    {
        animator.SetTrigger("DashAttack");
        rigidbody.velocity = Vector2.zero;
        animator.SetTrigger("Default");
        yield return new WaitForSeconds(1.5f);        
        ChangeState(Unit_state.Move);
        yield return null;
    }
    private IEnumerator WarriorDash_Co()
    {
        animator.SetBool("Move", false);
        animator.SetTrigger("Dash");

        float elapsedTime = 0f;
        float attackDuration = 2f;

        
        
        Vector3 playerDirection = (Player.transform.position - transform.position).normalized;
        monsterMove.PlayerDirectionCheck();

        float maxSpeedMultiplier = 1.5f;//최대 속도 배수
        float minSpeedMultiplier = 0.5f;//최소 속도 배수

        Vector3 initialPosition = transform.position;
        float initialDistance = Vector3.Distance(initialPosition, Player.transform.position);
        float maxInitialDistance = 10f; //플레이어와의 초기 최대 허용 거리
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        //초기 속도 계산
        float initialSpeedMultiplier = Mathf.Clamp01(initialDistance / maxInitialDistance);
        initialSpeedMultiplier = Mathf.Lerp(minSpeedMultiplier, maxSpeedMultiplier, initialSpeedMultiplier);
        float initialSpeed = DashSpeed * initialSpeedMultiplier;

        while (elapsedTime < attackDuration)
        {
            if (distance < 4f)
            {
                break;
            }
            distance = Vector3.Distance(transform.position, Player.transform.position);            
            elapsedTime += Time.deltaTime;
            rigidbody.velocity = new Vector2(playerDirection.x * initialSpeed, rigidbody.velocity.y);            
            yield return null;
        }
        rigidbody.velocity = Vector2.zero;
        animator.SetTrigger("DashEnd");
        elapsedTime = 0f;
        while (elapsedTime < 0.5f)
        {            
            elapsedTime += Time.deltaTime;
            float deceleration = 1.5f; //조절 가능한 감속 
            float currentVelocityX = DashSpeed * Mathf.Exp(-deceleration * elapsedTime);
            rigidbody.velocity = new Vector2(playerDirection.x * currentVelocityX, rigidbody.velocity.y);

            yield return null;
        }
        P_DefaultHP = 0;
        ChangeState(Unit_state.DashAttack);
    }
    public IEnumerator WarriorIntroAni()
    {
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Intro_1");
        yield return new WaitForSeconds(2f);
        animator.SetTrigger("Default");
        yield return new WaitForSeconds(2f);
        CameraControll_Warrior.Instance.start = true;
    }
    private IEnumerator WarriorArrowGimmick_Co()
    {
        int randomDirection = Random.Range(0, 2);
        bool right = randomDirection.Equals(1) ? true : false;


        int random_Y = Random.Range(0, 4);
        float slide_Y = 0f;
        switch (random_Y)
        {
            case 0:
                slide_Y = 5.51f;
                break;
            case 1:
                slide_Y = 8.93f;
                break;
            case 2:
                slide_Y = 10.22f;
                break;
            case 3:
                slide_Y = 11.42f;
                break;            
        }
        animator.SetTrigger("GimmickRun");
        float currentTime = 0f;
        float distance;
        if (right)
        {
            distance = transform.position.x - 45f;
        }
        else
        {
            distance = transform.position.x - 11f;
        }
        float direction = (distance <= 0) ? 1 : -1;
        if (direction < 1)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        while (currentTime<10f)
        {
            currentTime += Time.deltaTime;
            if (right)
            {
                if (transform.position.x > 45f&& transform.position.x < 45.5f)
                {                
                    rigidbody.velocity = Vector2.zero;                
                    rigidbody.AddForce(Vector2.up * 190f, ForceMode2D.Impulse);
                    break;
                }
            }
            else
            {
                if (transform.position.x > 11f && transform.position.x < 11.5f)
                {
                    rigidbody.velocity = Vector2.zero;
                    rigidbody.AddForce(Vector2.up * 190f, ForceMode2D.Impulse);
                    break;
                }
            }
            rigidbody.velocity = new Vector2(direction * speedSet*2f, rigidbody.velocity.y);
            yield return null;
        }
        animator.SetTrigger("Jump");
        currentTime = 0f;
        while (currentTime < 1.3f)
        {                      
            currentTime += Time.deltaTime;
            rigidbody.velocity = new Vector2(direction * 6f, rigidbody.velocity.y);
            yield return null;
        }

        Tile_obj.SetActive(true);
        currentTime = 0f;
        rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
        animator.SetTrigger("WallSlide");
        while (currentTime < 4f)
        {
            if (transform.position.y < slide_Y)
            {
                animator.SetTrigger("WallSlideFreeze");
                rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            currentTime += Time.deltaTime;            
            yield return null;
        }        
        currentTime = 0f;
        if (right)
        {
            arrow_R.SetActive(true);
        }
        else
        {
            arrow_L.SetActive(true);
        }
        while (currentTime < 4f)
        {            
            currentTime += Time.deltaTime;
            yield return null;
        }
        arrow_L.SetActive(false);
        arrow_R.SetActive(false);
        rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
        currentTime = 0f;
        animator.SetTrigger("WallSlide");
        while (currentTime < 4f)
        {
            LayerMask groundMask = LayerMask.GetMask("Ground");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 5f, groundMask);
            Debug.DrawRay(transform.position, Vector2.down * 5f, Color.red);
            if (hit.collider != null)
            {
                Tile_obj.SetActive(false);
                animator.SetTrigger("Out");
                rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                if (right)
                {
                    rigidbody.AddForce(Vector2.left * 100f, ForceMode2D.Impulse);
                }
                else
                {
                    rigidbody.AddForce(Vector2.right * 100f, ForceMode2D.Impulse);
                }
                ChangeState(Unit_state.Move);
            }
            currentTime += Time.deltaTime;
            yield return null;
        }

    }
    public override void Monster_HealthCheck()
    {
        if (Health <= 0)
        {
            ChangeState(Unit_state.Die);
        }
    }
    public void WarriorJump()
    {
        if (state.Equals(Unit_state.Move))
        {
            rigidbody.AddForce(Vector2.up * 70f, ForceMode2D.Impulse);
        }
    }
}
