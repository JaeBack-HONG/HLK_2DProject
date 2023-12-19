using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State : MonoBehaviour
{
    public UnitData data;
    public Unit_state state;
    public int Health;
    private void Start()
    {
        MonsterDataSetting();
        state = Unit_state.Move;
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
