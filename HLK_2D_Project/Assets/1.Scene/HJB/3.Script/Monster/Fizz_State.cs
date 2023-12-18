using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fizz_State : MonoBehaviour,IUnit
{
    public UnitData data;
    public MonsterMove monsterMove;
    private void Start()
    {
        data = new UnitData
            (name: "Fizz", hp: 2, detection: 2, range: 1, attackSpeed: 1,
            strength: 2, moveSpeed: 2, jumpForce: 0);

        monsterMove = GetComponent<MonsterMove>();
        monsterMove.MoveSpeed = data.MoveSpeed;        
    }

    public void Die()
    {
        
    }
}
