using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownMove : MonsterMove
{
    public override void Awake()
    {
        base.Awake();
    }
    private void FixedUpdate()
    {
        TotalMove();
    }

    public override void TotalMove()
    {
        base.TotalMove();
    }
    public override void WallCheck()
    {
        base.WallCheck();
    }
}
