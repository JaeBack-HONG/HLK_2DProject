using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brown_Attack : MonoBehaviour
{
    public float throwForce = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어를 현재 객체의 상단으로 이동
            collision.gameObject.transform.position = new Vector2(transform.position.x, transform.position.y + 1.5f);

            // 플레이어의 Rigidbody2D 가져오기
            Rigidbody2D otherRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            if (otherRigidbody != null)
            {
                // 던질 방향 계산
                Vector2 throwDirection = (otherRigidbody.position - new Vector2(transform.position.x, transform.position.y)).normalized;

                // 플레이어를 던지기
                otherRigidbody.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
            }
        }
    }

}
