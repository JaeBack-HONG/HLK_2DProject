using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rigidbody;
    private Animator animator;

    public Monster_State mon;
    private Player_State P_State;

    public int jumpCount;
    public int maxJumps = 2;

    private void Awake()
    {
        TryGetComponent<Animator>(out animator);
        jumpCount = maxJumps;
        TryGetComponent<Rigidbody2D>(out rigidbody);
        TryGetComponent<Player_State>(out P_State);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Top") && gameObject.layer.Equals((int)Layer_Index.Player))
        {
            mon = collision.gameObject.transform.root.GetComponent<Monster_State>();
            Jump(P_State.JumpForce);
            jumpCount++;
            P_State.Attack(mon);
        }
        else if (collision.gameObject.CompareTag("DTop") && gameObject.layer.Equals((int)Layer_Index.Player))
        {
            Jump(P_State.JumpForce * 0.7f);
            jumpCount++;
        }
    }

    public void MoveCheck()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        animator.SetFloat("XSpeed", Mathf.Abs(horizontalInput));
        rigidbody.velocity = new Vector2(horizontalInput * moveSpeed, rigidbody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && !jumpCount.Equals(0)) Jump(P_State.JumpForce);

        if (Input.GetKeyDown(KeyCode.Z) && !P_State.isArmand) P_State.actState = Unit_state.Attack;

    }

    public void Jump(float jumpforce)
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
        rigidbody.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);

        if (!P_State.JumState.Equals(Jump_State.Jumping))
        {
            P_State.ChangeState(Jump_State.Jumping);
        }
        jumpCount--;
    }
}
