using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player_Projectile : MonoBehaviour
{
    [Range(1f, 100f)]
    public float Speed = 0;

    public int damage = 1;
    
    [Range(30f, 100f)]
    public float removeDistanceSet = 35f;

    public IEnumerator Shot(Vector3 direction)
    {
        transform.rotation = direction.x.Equals(1) ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 180, 0, 0);        
        Vector3 startPos = transform.position;
        while (Vector3.Magnitude(startPos - transform.position) < removeDistanceSet)
        {
            transform.position += direction * Time.fixedDeltaTime * Speed;
            yield return new WaitForFixedUpdate();
        }
        Destroy(gameObject);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals((int)Layer_Index.Enemy))
        {
            Monster_State monstate = collision.gameObject.GetComponent<Monster_State>();

            if (monstate != null)
            {
                monstate.Health -= damage;
                monstate.UnitHit.Hit((int)Layer_Index.Enemy, transform.position, Condition_state.Default);
            }
            Destroy(gameObject);
        }
    }
}
