using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class AttackBox : MonoBehaviour
{
    
    [SerializeField] private Monster_State mon_state;
    [SerializeField] private Unit_state state;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            Player_State player = collision.gameObject.GetComponent<Player_State>();
            
             mon_state.Attack(player);
            
        }
    }
}
