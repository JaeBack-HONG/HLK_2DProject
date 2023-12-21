using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rigidbody;

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
        //P_State.actState = horizontalInput.Equals(0) ? Unit_state.Idle : Unit_state.Move;        
    }

    public void GroundRayCheck()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1, LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, Vector2.down * 1f, Color.red);
        if (hit.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
    }

    public void IsFalling()
    {
        if (rigidbody.velocity.y < -0.01f)
        {
            P_State.actState = Unit_state.Falling;
        }
        else if (rigidbody.velocity.y > 0.1f && P_State.actState.Equals(Unit_state.Move))
        {
            P_State.actState = Unit_state.Jump;
        }
        else
        {
            P_State.actState = Unit_state.Move;
        }
    }


    public void Jump()
    {
        P_State.actState = Unit_state.Jump;
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
        rigidbody.AddForce(Vector2.up * 15f, ForceMode2D.Impulse);
    }

}
