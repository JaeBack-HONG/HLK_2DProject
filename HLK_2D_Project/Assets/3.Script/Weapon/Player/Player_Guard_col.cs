using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Guard_col : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
        }
    }
}
