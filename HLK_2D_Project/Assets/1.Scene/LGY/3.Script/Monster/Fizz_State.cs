using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fizz_State : Unit
{
    private void Awake()
    {
        data._HP = 2;
        data._Detection = 2;
        data._Range = 1;
        data._AttackSpeed = 1;
        data._Strength = 1;
        data._MoveSpeed = 2;
        data._JumpForce = 0;
    }

    public override void Die()
    {
    }
}
