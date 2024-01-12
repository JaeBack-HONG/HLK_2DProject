using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet : Player_Projectile
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
