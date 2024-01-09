using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun_Attack_col : HitBox
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && collision.gameObject.layer.Equals((int)Layer_Index.Enemy))
        {
            Monster_State monstate = collision.gameObject.GetComponent<Monster_State>();
            monstate.state = Unit_state.Stun;
            HitDmg(monstate);
        }

    }
}
