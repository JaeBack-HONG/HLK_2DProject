using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant_State : MonoBehaviour,IUnit
{
    
    public UnitData data;
    private MonsterMove monsterMove;
    private void Start()
    {
        data = new UnitData
            (name:"Ant", hp: 1, detection: 4, range: 1, attackSpeed: 1,
            strength: 1, moveSpeed: 1, jumpForce: 0);

        monsterMove = GetComponent<MonsterMove>();
        monsterMove.MoveSpeed = data.MoveSpeed;
    }

    public void Die()
    {

    }
}
