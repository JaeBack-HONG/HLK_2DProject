using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet : MonoBehaviour
{
    [Range(1f, 100f)]
    [SerializeField] float Speed = 15f;

    [SerializeField] private int damage = 2;
    private Vector3 Direction;

    private void Start()
    {
        transform.parent.gameObject.TryGetComponent<Player_Hope_Mod>(out Player_Hope_Mod hopemod);
        
        Direction = hopemod.P_state.direction;
        transform.parent = null;
    }

    private void FixedUpdate()
    {
        transform.position += Direction * Time.fixedDeltaTime * Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Monster_State mon_state = collision.gameObject.GetComponent<Monster_State>();
            mon_state.Health -= damage;
            mon_state.UnitHit.Hit(mon_state.gameObject.layer);
        }
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.layer.Equals((int)Layer_Index.Ground))
        {
            Destroy(this.gameObject);
        }
    }
}
