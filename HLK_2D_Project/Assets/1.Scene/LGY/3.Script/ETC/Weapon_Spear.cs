using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Spear : MonoBehaviour
{
    private Player_State P_state;

    private void Awake()
    {
        transform.root.TryGetComponent<Player_State>(out P_state);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && collision.gameObject.layer.Equals((int)Layer_Index.Enemy))
        {
            Monster_State monstate = collision.gameObject.GetComponent<Monster_State>();
            monstate.state = Unit_state.Stun;
            P_state.Attack(monstate);
        }

    }
}
