using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hope1_State : Unit
{
    private void Awake()
    {
        data._HP = 3;
        data._Detection = 7;
        data._Range = 5;
        data._AttackSpeed = 0.5f;
        data._Strength = 1;
        data._MoveSpeed = 2;
        data._JumpForce = 0;
    }

    public override void Die()
    {
    }
}
