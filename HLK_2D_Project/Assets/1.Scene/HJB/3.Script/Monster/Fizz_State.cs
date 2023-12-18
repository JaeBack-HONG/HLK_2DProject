using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fizz_State : Monster_State, IUnit
{
    private void Start()
    {
        MonsterDataSetting();
    }

    public override void MonsterDataSetting()
    {
        data = new UnitData
    (name: "Fizz", hp: 2, detection: 2, range: 1, attackSpeed: 1,
    strength: 2, moveSpeed: 2, jumpForce: 0);
        base.MonsterDataSetting();
    }

    public void Die()
    {

    }
}
