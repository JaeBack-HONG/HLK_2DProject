using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player_Projectile : MonoBehaviour
{
    [Range(1f, 100f)]
    public float Speed = 0;

    public int damage = 0;

    [Range(30f, 100f)]
    public float removeDistanceSet = 35f;

    public IEnumerator Shot(Vector3 direction)
    {
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


            //여기에 플레이어 독 데미지 불러오기

            if (monstate != null)
            {
                monstate.Health -= damage;
                monstate.UnitHit.Hit(monstate.gameObject.layer, transform.position);
            }
            Destroy(this.gameObject);
        }
    }
}
