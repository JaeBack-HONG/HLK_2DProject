using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight1_State : MonoBehaviour
{
    
    public UnitData data;
    public MonsterMove monsterMove;
    private void Start()
    {
        data = new UnitData
            (name: "Knight1", hp: 5, detection: 5, range: 2, attackSpeed: 1,
            strength: 2, moveSpeed: 1, jumpForce: 0);
        monsterMove = GetComponent<Knight1Move>();
        monsterMove.MoveSpeed = data.MoveSpeed;
    }

    public void Die()
    {

    }
}
