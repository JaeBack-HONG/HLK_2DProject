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
            // �÷��̾ ���� ��ü�� ������� �̵�
            collision.gameObject.transform.position = new Vector2(transform.position.x, transform.position.y + 1.5f);

            // �÷��̾��� Rigidbody2D ��������
            Rigidbody2D otherRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            if (otherRigidbody != null)
            {
                // ���� ���� ���
                Vector2 throwDirection = (otherRigidbody.position - new Vector2(transform.position.x, transform.position.y)).normalized;

                // �÷��̾ ������
                otherRigidbody.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
            }
        }
    }

}
