using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackwolf_State : Unit
{
    private void Awake()
    {
        data._HP = 15;
        data._Detection = 10;
        data._Range = 2;
        data._AttackSpeed = 1;
        data._Strength = 2;
        data._MoveSpeed = 5;
        data._JumpForce = 1;
    }

    public override void Die()
    {
    }
}
