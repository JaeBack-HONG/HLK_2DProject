using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TIleTrap : MonoBehaviour
{
    [SerializeField] private int damage = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals((int)Layer_Index.Player))
        {
            Player_State playerState = collision.gameObject.GetComponent<Player_State>();
            playerState.Health -= damage;
            playerState.unithit.Hit(playerState.gameObject.layer, transform.position);
        }

        if (collision.gameObject.layer.Equals((int)Layer_Index.Enemy))
        {
            Monster_State monsterState = collision.gameObject.GetComponent<Monster_State>();

            monsterState.UnitHit.Hit(monsterState.gameObject.layer,transform.position);
            monsterState.Health -= 100;
        }
    }
}
