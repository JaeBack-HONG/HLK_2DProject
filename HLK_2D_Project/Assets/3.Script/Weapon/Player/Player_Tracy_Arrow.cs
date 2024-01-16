using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Tracy_Arrow : Player_Projectile
{
    public int Poisondamage = 1;
    public float PoisonCool = 0;
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals((int)Layer_Index.Enemy))
        {
            Monster_State monstate = collision.gameObject.GetComponent<Monster_State>();

            if (monstate != null)
            {
                monstate.Poison(PoisonCool, Poisondamage);
            }
            Destroy(gameObject);
        }
    }
}
