using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hope1_State : MonoBehaviour
{
    public UnitData data;
    public MonsterMove monsterMove;
    private void Start()
    {
        data = new UnitData
            (name: "Hope1", hp: 3, detection: 7, range: 5, attackSpeed: 0.5f,
            strength: 1, moveSpeed: 2, jumpForce: 0);
        monsterMove = GetComponent<Hope1Move>();
        monsterMove.MoveSpeed = data.MoveSpeed;
    }

    

    public void Die()
    {

    }
}
