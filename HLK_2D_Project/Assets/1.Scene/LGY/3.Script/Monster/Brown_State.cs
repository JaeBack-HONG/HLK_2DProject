using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brown_State : Unit
{
    private void Awake()
    {
        data._HP = 5;
        data._Detection = 3;
        data._Range = 0.5f;
        data._AttackSpeed = 0.5f;
        data._Strength = 2;
        data._MoveSpeed = 1;
        data._JumpForce = 0;
    }

    public override void Die()
    {
    }
}
