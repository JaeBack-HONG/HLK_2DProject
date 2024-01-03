using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTile : MonoBehaviour
{
    [SerializeField] private int damage = 2;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Player_State playerState = col.gameObject.GetComponent<Player_State>();
            playerState.Health -= damage;
            playerState.unithit.Hit(playerState.gameObject.layer, transform.position);
        }

        if (col.gameObject.layer.Equals((int)Layer_Index.Enemy))
        {
            Monster_State monsterState = col.gameObject.GetComponent<Monster_State>();

            monsterState.UnitHit.Hit(monsterState.gameObject.layer, transform.position);
            monsterState.Health -= 100;
        }
    }
}
