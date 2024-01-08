using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Tracy_Arrow : MonoBehaviour
{
    [Range(1f, 100f)]
    public float Speed = 2f;

    [SerializeField] private int damage = 2;

    [Range(30f, 100f)]
    [SerializeField] private float removeDistanceSet = 35f;

    public IEnumerator Shot(Vector3 direction)
    {

        Vector3 defaultDistance = transform.position;
        while (true)
        {
            Vector3 currentDistance = (transform.position - defaultDistance);
            if (Vector3.Magnitude(currentDistance) > removeDistanceSet)
            {
                break;
            }
            transform.position += direction * Time.deltaTime * Speed;
            yield return null;
        }
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") &&
            collision.gameObject.layer.Equals((int)Layer_Index.Enemy))
        {
            Monster_State playerState = collision.gameObject.GetComponent<Monster_State>();
            //여기에 플레이어 독 데미지 불러오기

            Destroy(this.gameObject);
        }

    }
}
