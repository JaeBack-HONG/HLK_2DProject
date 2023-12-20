using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State : MonoBehaviour
{
    public UnitData data;
    public Unit_state state;
    public int Health;
    public Player_Ability P_Ability;
    private void Start()
    {
        P_Ability = GetComponent<Player_Brown_Mod>();
        MonsterDataSetting();
        state = Unit_state.Idle;
    }

    public void MonsterDataSetting()
    {
        data = new UnitData
            (name: "Player", hp: 10, detection: 5, range: 1, attackSpeed: 1,
                strength: 2, moveSpeed: 2, jumpForce: 100);
        Health = data.HP;

    }
    private void FixedUpdate()
    {
        Player_HealthCheck();
    }

    private void State_Check()
    {
        switch(state)
        {
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
        }
    }

    public void Player_HealthCheck()
    {

        if (Health<=0)
        {

            Die();
        }

    }

    
    public void Die()
    {
        Destroy(gameObject, 1f);
    }
    public void Attack(Monster_State other)
    {
        other.Health -= data.Strength;
    }
}
