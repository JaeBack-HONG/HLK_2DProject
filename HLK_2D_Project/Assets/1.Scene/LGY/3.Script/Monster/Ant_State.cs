using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant_State : Unit
{
    private void Awake()
    {
        data._HP = 1;
        data._Detection = 4;
        data._Range = 1;
        data._AttackSpeed = 1;
        data._Strength = 1;
        data._MoveSpeed = 1;
        data._JumpForce = 0;
    }

    public override void Die()
    {
    }
}
