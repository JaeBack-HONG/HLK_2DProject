using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRed_col : HitBox
{
    [Range(3f, 15f)]
    [SerializeField] private float knockbackValue = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.gameObject.layer.Equals((int)Layer_Index.Enemy))
        {
            Monster_State monstate = collision.gameObject.GetComponent<Monster_State>();
            monstate.rigidbody.AddForce(P_state.direction * 10f, ForceMode2D.Impulse);
            HitDmg(monstate);
        }
    }
}
