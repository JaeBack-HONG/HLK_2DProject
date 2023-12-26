using UnityEngine;


public abstract class Monster_State : MonoBehaviour
{
    public Unit_state state;
    public Condition_state C_State;

    public UnitData data;
    public Unit_Hit UnitHit;
    public MonsterMove monsterMove;
    public Monster_State monster_State;
    public Animator animator;
    public Rigidbody2D rigidbody;

    private SpriteRenderer renderer;

    public int Health;
    public int Strength;
    public bool isAttack = false;


    public Player_State Player;

    public virtual void MonsterDataSetting()
    {
        renderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        monsterMove = GetComponent<MonsterMove>();
        animator = GetComponent<Animator>();
        UnitHit = GetComponent<Unit_Hit>();
        monsterMove.MoveSpeed = data.MoveSpeed;
        monsterMove.Detection = data.Detection;
    }


    public abstract void Monster_HealthCheck();

    public virtual void Die()
    {
        renderer.color = Color.white;
        Debug.Log("죽음");
        state = Unit_state.Default;
        animator.SetTrigger("Death");
        Destroy(gameObject, 0.7f);
    }
    #region//플레이어에게 데미지주는 메서드(Player_State other)
    public void Attack(Player_State other)
    {
        other.Health -= data.Strength;
        other.unithit.Hit(other.gameObject.layer);
    }
    #endregion

    #region //플레이어가 몬스터에게 기본적으로 닿았을 때(CollisionEnter)
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
    }
    #endregion

    #region//플레이어 탐지 (OnTriggerStay)
    //플레이어가 탐지 트리거안에 머물러 있다면
    private void OnTriggerStay2D(Collider2D collision)
    {
        //플레이어 레이어이면
        if (collision.gameObject.layer.Equals((int)Layer_Index.Player))
        {
            Player = collision.gameObject.transform.GetComponent<Player_State>();
            monsterMove.target = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //플레이어 레이어이면
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
}
