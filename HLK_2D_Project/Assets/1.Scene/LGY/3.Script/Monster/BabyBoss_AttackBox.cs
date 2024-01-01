using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyBoss_AttackBox : MonoBehaviour
{
    [SerializeField] Monster_State bb_state;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Player_State Player = collision.gameObject.GetComponent<Player_State>();
            if (Player != null)
            {
                bb_state.Attack(Player);
            }
        }


    }
}
