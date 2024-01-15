using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Guard_col : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Monster_Projectile mon_Projectile = collision.gameObject.GetComponent<Monster_Projectile>();
            mon_Projectile.damage = 1;

        }
    }
}
