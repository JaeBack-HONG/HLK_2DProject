using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(1f, 100f)]
    public float Speed = 2f;
    private void FixedUpdate()
    {
        transform.position += gameObject.transform.position* Time.deltaTime * Speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.layer.Equals((int)Layer_Index.Enemy))
        {
            Destroy(this.gameObject);
        }
    }
}
