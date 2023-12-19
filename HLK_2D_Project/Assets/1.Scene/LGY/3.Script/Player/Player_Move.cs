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

    private void Awake()
    {
        TryGetComponent<Rigidbody2D>(out rigidbody);
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (!isGrab)
        {
            rigidbody.velocity = new Vector2(horizontalInput * moveSpeed, rigidbody.velocity.y);
        }

        Jump();


    }
    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, Vector2.down, Color.red, 1f);
        if (rigidbody.velocity.y < 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody.position, Vector2.down, 1, LayerMask.GetMask("Ground"));
            if (hit.collider != null)
            {
                if (hit.distance < 0.5f)
                {
                    if (isGrab)
                    {
                        isGrab = false;
                    }
                    isJumping = false;
                }
            }
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }
    }

}
