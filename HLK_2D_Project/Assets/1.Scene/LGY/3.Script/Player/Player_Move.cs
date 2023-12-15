using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rigidbody;
    public float jumpForce = 5f;

    private void Awake()
    {
        TryGetComponent<Rigidbody2D>(out rigidbody);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody.velocity = new Vector2(moveSpeed, rigidbody.velocity.y);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody.velocity = new Vector2( -moveSpeed,rigidbody.velocity.y);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //rigidbody.AddForce(Vector2.up * jumpForce , ForceMode2D.Impulse);
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
        }

    }
}
