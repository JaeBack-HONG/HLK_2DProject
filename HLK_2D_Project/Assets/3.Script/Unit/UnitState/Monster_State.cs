using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Monster_State : MonoBehaviour
{
    [Header("�⺻ : ü�� / ���� / �ӵ� ����")]
    [SerializeField] public int healthSet = 4;
    [SerializeField] public int damageSet = 2;
    [SerializeField] public int speedSet = 5;
    [Header("�Ʒø��")]
    public bool trainingMod;
    [Header("")]
    public Unit_state state;
    public Unit_state currentState;

    public Condition_state C_State;

    public UnitData data;
    public Unit_Hit UnitHit;
    public MonsterMove monsterMove;    
    public Animator animator;
    public Rigidbody2D rigidbody;
    public AbilityItem abilityItem;
    public SpriteRenderer renderer;


    [Header("�������ӽð�")]
    [SerializeField] private float stunTime = 0f;

    public int Health;
    public int Strength;
    public bool isAttack = false;
    public bool rolling = false;
    

    public bool Dash = false;

    public Player_State Player;

    public Ability_Item ability_Item;

    public GameObject Ability_Item_obj;
    public GameObject Stun_obj;

    IEnumerator Slow_co;
    IEnumerator Stun_co;
    IEnumerator Poison_co;
    IEnumerator Ignition_co;

    public virtual void MonsterDataSetting()
    {
        renderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        monsterMove = GetComponent<MonsterMove>();
        animator = GetComponent<Animator>();
        UnitHit = GetComponent<Unit_Hit>();
        monsterMove.MoveSpeed = data.MoveSpeed;
        monsterMove.Detection = data.Detection;
        monsterMove.jumpUnit = data.JumpForce;
    }

    public abstract void Monster_HealthCheck();

    public virtual void Die()
    {               
        animator.SetTrigger("Death");
        rigidbody.velocity = Vector2.zero;
        renderer.color = Color.white;
        Debug.Log("����");
        Destroy(gameObject, 0.5f);
    }

    #region//�÷��̾�� �������ִ� �޼���(Player_State other)
    public void Attack(Player_State other)
    {
        if (other.Health > 0)
        {
            other.Health -= data.Strength;
            other.unithit.Hit((int)Layer_Index.Player, transform.position, Condition_state.Default);
        }
    }
    #endregion

    #region //�÷��̾ ���Ϳ��� �⺻������ ����� ��(CollisionEnter)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {            
            Player_State Player = collision.gameObject.GetComponent<Player_State>();            
            
            if (Player != null)
            {
                Attack(Player);
            }
        }

        if (collision.gameObject.layer.Equals((int)Layer_Index.Player) ||
            collision.gameObject.layer.Equals((int)Layer_Index.Ground))
        { 
            Dash = false;
        }
    }
    #endregion

    #region//�÷��̾� Ž�� (OnTriggerStay)
    //�÷��̾ Ž�� Ʈ���žȿ� �ӹ��� �ִٸ�
    private void OnTriggerStay2D(Collider2D collision)
    {
        //�÷��̾� ���̾��̸�
        if (collision.gameObject.layer.Equals((int)Layer_Index.Player))
        {
            Player = collision.gameObject.transform.GetComponent<Player_State>();
            monsterMove.target = true;
        }        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //�÷��̾� ���̾��̸�
        if (collision.gameObject.layer.Equals((int)Layer_Index.Player) || collision.gameObject.CompareTag("Player"))
        {
            monsterMove.target = false;            
        }
    }
    #endregion
        

    public void IsGrab()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.gravityScale = 0f;
    }

    #region // ���ο� ���� ����
    public void Slow(float speed,float cool)
    {
        Slow_co = Slow_Co(speed, cool);
        StartCoroutine(Slow_co);
    }
    private IEnumerator Slow_Co(float speed , float cool)
    {
        float currentTime = 0;
        monsterMove.MoveSpeed *= speed;

        while (currentTime<cool)
        {
            currentTime += Time.fixedDeltaTime;           

            yield return new WaitForFixedUpdate();
        }
        monsterMove.MoveSpeed = data.MoveSpeed;
    }
    #endregion

    #region //���� ���� ����
    public void Stun(float cool)
    {
        Stun_co = Stun_Co(cool);
        StartCoroutine(Stun_co);
    }
    private IEnumerator Stun_Co(float cool)
    {
        state = Unit_state.Stun;
        //������Ʈ Ű��
        Stun_obj.SetActive(true);
        animator.SetBool("Move", false);
        float currentTime = 0f;
        while(currentTime<cool)
        {            
            currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        //������Ʈ ����
        Stun_obj.SetActive(false);
        animator.SetTrigger("Default");
        state = Unit_state.Move;
    }
    #endregion

    public void Poison(float cool, int damge)
    {
        Poison_co = Poison_Co(cool, damge);
        StartCoroutine(Poison_co);
    }
    private IEnumerator Poison_Co(float cool, int damage)
    {
        UnitHit.Hit(gameObject.layer, transform.position, Condition_state.Poison);
        Health -= damage;
        yield return new WaitForSeconds(cool);
        UnitHit.Hit(gameObject.layer, transform.position, Condition_state.Poison);
        Health -= damage;
        yield return new WaitForSeconds(cool);
        UnitHit.Hit(gameObject.layer, transform.position, Condition_state.Poison);
        Health -= damage;

        yield return null;
    }
    public void Ignition(float cool, int damge)
    {
        Ignition_co = Ignition_Co(cool, damge);
        StartCoroutine(Ignition_co);
    }
    private IEnumerator Ignition_Co(float cool, int damage)
    {
        UnitHit.Hit(gameObject.layer, transform.position, Condition_state.Ignition);
        Health -= damage;
        yield return new WaitForSeconds(cool);
        UnitHit.Hit(gameObject.layer, transform.position, Condition_state.Ignition);
        Health -= damage;
        yield return new WaitForSeconds(cool);
        UnitHit.Hit(gameObject.layer, transform.position, Condition_state.Ignition);
        Health -= damage;

        yield return null;
    }
}
