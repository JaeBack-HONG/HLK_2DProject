using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Monster_State : MonoBehaviour
{
    public Unit_state state;

    public UnitData data;
    public MonsterMove monsterMove;
    public Monster_State monster_State;
    public Animator animator;
    public int Health;
    public int Strength;
    public bool isAttack = false;
    public virtual void MonsterDataSetting()
    {
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
    }
}
