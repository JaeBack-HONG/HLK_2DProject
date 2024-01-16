using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_FireBall : Player_Projectile
{
    public int Ignitiondamage = 1;
    public float IgnitionCool = 0;
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals((int)Layer_Index.Enemy))
        {
            Monster_State monstate = collision.gameObject.GetComponent<Monster_State>();

            if (monstate != null)
            {
                monstate.Ignition(IgnitionCool, Ignitiondamage);
            }
            Destroy(gameObject);
        }

    }
}
