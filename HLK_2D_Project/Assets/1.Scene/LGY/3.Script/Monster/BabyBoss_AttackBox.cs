using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyBoss_AttackBox : MonoBehaviour
{
    [SerializeField] private BabyBoss_State bb_state;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player_State player = collision.gameObject.GetComponent<Player_State>();
            
             bb_state.Attack(player);
            
        }
    }
}
