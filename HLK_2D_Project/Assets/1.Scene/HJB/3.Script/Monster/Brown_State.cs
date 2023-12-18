using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brown_State : MonoBehaviour
{
    public UnitData data;
    public MonsterMove monsterMove;

    private void Start()
    {
        data = new UnitData
            (name: "Brown", hp: 5, detection: 3, range: 0.5f, attackSpeed: 2,
            strength: 2, moveSpeed: 1, jumpForce: 0);
        monsterMove = GetComponent<MonsterMove>();
        monsterMove.MoveSpeed = data.MoveSpeed;
    }

    public void Die()
    {

    }
}
