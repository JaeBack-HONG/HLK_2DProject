using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight1_State : Unit
{
    private void Awake()
    {
        data._HP = 5;
        data._Detection = 5;
        data._Range = 2;
        data._AttackSpeed = 1;
        data._Strength = 2;
        data._MoveSpeed = 1;
        data._JumpForce = 0;
    }

    public override void Die()
    {
    }
}
