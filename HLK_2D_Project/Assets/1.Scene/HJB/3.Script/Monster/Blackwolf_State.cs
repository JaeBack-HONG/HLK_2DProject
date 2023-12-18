using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackwolf_State : Monster_State, IUnit
{
    private void Start()
    {
        MonsterDataSetting();
    }

    public override void MonsterDataSetting()
    {
        data = new UnitData
    (name: "Blackwolf", hp: 15, detection: 10, range: 2, attackSpeed: 1,
    strength: 2, moveSpeed: 5, jumpForce: 1);
        base.MonsterDataSetting();
    }

    public void Die()
    {

    }
}
