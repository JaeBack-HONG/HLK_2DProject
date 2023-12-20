using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rigidbody;
    public float jumpForce = 100f;

    private bool isJumping = false;
    public bool isGrab = false;

    public Monster_State mon;
    private Player_State P_State;

    private float horizontalInput;

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
            if (mon != null)
            {
                P_State.Attack(mon);
            }
        }
    }

    public void IsGrab()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.gravityScale = 0f;
    }

    public void MoveCheck()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        rigidbody.velocity = new Vector2(horizontalInput * moveSpeed, rigidbody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            P_State.actState = Unit_state.Jump;
        }

        P_State.actState = horizontalInput.Equals(0) ? Unit_state.Idle : Unit_state.Move;
    }

    private void Jumping()
    {
        if (rigidbody.velocity.y < 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody.position, Vector2.down, 1, LayerMask.GetMask("Ground"));
            if (hit.collider != null)
            {
                if (hit.distance < 0.5f)
                {
                    P_State.actState = Unit_state.Idle;
                    isJumping = false;
                }
            }
        }
    }


    private void Jump()
    {
        rigidbody.velocity = rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
        rigidbody.AddForce(Vector2.up * P_State.data.JumpForce, ForceMode2D.Impulse);

        if (!isJumping)
        {
            P_State.actState = Unit_state.Jump;
        }
    }

}
