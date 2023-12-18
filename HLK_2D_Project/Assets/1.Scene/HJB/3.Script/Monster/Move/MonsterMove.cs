using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public float MoveSpeed;

    public Rigidbody2D rigidbody;

    public int nextMove;

    public float firstTime;
    public float lastTime = 3f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void TotalMove()
    {
        WallCheck();
        rigidbody.velocity = new Vector2(nextMove * MoveSpeed, rigidbody.velocity.y);
    }
    private void WallCheck()
    {
        int groundLayerMask = LayerMask.GetMask("Ground");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 1f, groundLayerMask);
        Debug.DrawRay(transform.position, Vector2.left * 1f);
        firstTime += Time.deltaTime;
        if (firstTime > lastTime)
        {
            nextMove = Random.Range(-1, 2);
            firstTime = 0;
        }
        if (hit.collider != null)
        {
            nextMove = -nextMove;
        }
    }
}
