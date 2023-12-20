using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Monster_State : MonoBehaviour
{
    public Unit_state state;
    public Condition_state C_State;

    public UnitData data;
    public MonsterMove monsterMove;
    public Monster_State monster_State;
    public Animator animator;
    public Rigidbody2D rigidbody;
    public int Health;
    public int Strength;
    public bool isAttack = false;
    public virtual void MonsterDataSetting()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        monsterMove = GetComponent<MonsterMove>();
        animator = GetComponent<Animator>();
        monsterMove.MoveSpeed = data.MoveSpeed;
        monsterMove.Detection = data.Detection;
    }
    

    public abstract void Monster_HealthCheck();

    public virtual void Die()
    {
        Destroy(gameObject, 1f);
    }
    public void Attack(Player_State other)
    {        
        Debug.Log("플레이어 공격 당함");
        other.Health -= data.Strength;
        //플레이어 State를 Hit으로 변경해주는 메서드 불러오기
    }
    
    public void IsGrab()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.gravityScale = 0f;
    }
}
