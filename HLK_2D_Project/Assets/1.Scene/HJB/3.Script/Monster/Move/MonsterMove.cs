using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public float MoveSpeed;
    public float Detection;

    public int nextMove;

    public Rigidbody2D rigidbody;

    public float firstTime;
    public float lastTime = 3f;

    public bool target = false;
    
    private float direction;
    private float distance;

    private bool Flip;
    private int FlipDirection = 1;

   [SerializeField] private Transform targetPlayer;

    SpriteRenderer renderer;

    
    

    private void Awake()
    {
        
        rigidbody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        TotalMove();

        
    }

    private void TotalMove()
    {
        
        //FollowPlayer();
        
        if (target)
        {
            rigidbody.velocity = new Vector2(FlipDirection*MoveSpeed, rigidbody.velocity.y);
            
        }
        else
        {
            WallCheck();
            rigidbody.velocity = new Vector2(nextMove * MoveSpeed, rigidbody.velocity.y);
        }
        //정지면 방향그대로 한 후 리턴
        if (nextMove==0)
        {
            return;
        }        

        //정지가 아니라면 방향에 맞춰 축변경
        Flip = (nextMove<0) ? true : false;

        if (Flip)
        {
            transform.rotation = Quaternion.Euler(0, 180,0);
            FlipDirection = -1;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            FlipDirection = 1;
        }
        
    }
    private void WallCheck()
    {
        //벽 체크후 벽이면 전환
        int groundLayerMask = LayerMask.GetMask("Ground");
        RaycastHit2D left_hit = Physics2D.Raycast(transform.position, Vector2.left, 1f, groundLayerMask);
        RaycastHit2D right_hit = Physics2D.Raycast(transform.position, Vector2.right, 1f, groundLayerMask);
        Debug.DrawRay(transform.position, Vector2.left * 1f);
        Debug.DrawRay(transform.position, Vector2.right * 1f);

        //이동 결정
        SelectMove();

        //벽에 닿았을 경우 전환
        if (right_hit.collider != null || left_hit.collider != null)
        {
            nextMove = -nextMove;
        }
    }
    private void SelectMove()
    {
        firstTime += Time.deltaTime;
        if (firstTime > lastTime)
        {
            nextMove = Random.Range(-1, 2);
            
            firstTime = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //플레이어 레이어이면
        if (collision.gameObject.layer.Equals(8))
        {
            target = true;
        }
        else
        {
            target = false;
        }
    }
    //private void DistanceAndDirection()
    //{
    //    distance = Vector2.Distance(transform.position, targetPlayer.position);        
    //
    //    direction = (targetPlayer.position.x - transform.position.x);
    //
    //    if (!Flip)
    //    {
    //        direction = (direction > 0) ? 1 : -1;
    //
    //    }
    //    else
    //    {
    //        direction = (direction > 0) ? -1 : 1;
    //    }        
    //}
    
    //private void FollowPlayer()
    //{
    //    DistanceAndDirection();
    //
    //    if ( distance < Detection && direction>0)
    //    {
    //        target = true;
    //        
    //    }
    //    else
    //    {
    //        target = false;
    //    }
    //}




}
