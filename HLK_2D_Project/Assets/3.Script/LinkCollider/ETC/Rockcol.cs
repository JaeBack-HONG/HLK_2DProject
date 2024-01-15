using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rockcol : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (rigidbody.velocity.y < -1)
        {
            if (collision.gameObject.layer.Equals((int)Layer_Index.Enemy))
            {
                Monster_State monstate = collision.gameObject.GetComponent<Monster_State>();
                monstate.Health -= 25;
                monstate.Stun(3f);
            }
            if (collision.gameObject.layer.Equals((int)Layer_Index.Player))
            {
                Player_State playerstate = collision.gameObject.GetComponent<Player_State>();
                playerstate.Health -= 5;
                playerstate.Stun(1f);
            }
        }
               
    }

}
