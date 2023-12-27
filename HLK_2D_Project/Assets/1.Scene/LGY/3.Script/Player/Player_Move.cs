using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    public Rigidbody2D rigidbody;

    public Monster_State mon;
    private Player_State P_State;

    public int jumpCount;
    public int maxJumps = 2;

    private void Awake()
    {
        TryGetComponent<Rigidbody2D>(out rigidbody);
        TryGetComponent<Player_State>(out P_State);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Top"))
        {
            mon = collision.gameObject.transform.root.GetComponent<Monster_State>();
            Jump();
            jumpCount++;
            if (mon != null)
            {
                P_State.Attack(mon);
            }
        }
    }

    public void MoveCheck()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rigidbody.velocity = new Vector2(horizontalInput * moveSpeed, rigidbody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && !jumpCount.Equals(0)) Jump();

        if (rigidbody.velocity.x.Equals(0)) return;

        transform.rotation = rigidbody.velocity.x <= 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
    }

    public void Jump()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
        rigidbody.AddForce(Vector2.up * 15f, ForceMode2D.Impulse);
        if (P_State.JumState.Equals(Jump_State.Idle))
        {
            P_State.JumState = Jump_State.Jumping;
        }
        jumpCount--;
    }

}
