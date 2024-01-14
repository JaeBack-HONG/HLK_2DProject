using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCheck : MonoBehaviour
{
    [SerializeField] private Warrior_Boss warrior_State;
        
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Projectile"))
        {            
            warrior_State.WarriorJump();
        }
    }
}
