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
    public float lastTime = 2f;

    public bool target = false;
    
    public float direction;
    private float distance;

    private bool Flip;
    private int FlipDirection = 1;
    public bool isGrab = false;

    public bool groundCheck = false;

    public Transform targetPlayer;    

    private Monster_State monster_State;

    RaycastHit2D groundHit;
    private void Awake()
    {
        
        rigidbody = GetComponent<Rigidbody2D>();        
        monster_State = GetComponent<Monster_State>();
        if (targetPlayer == null)
        {
            try
            {
                targetPlayer = GameObject.FindWithTag("Player").GetComponent<Transform>();

            }
            catch
            {
                Debug.LogError("현재 씬에 Player 없습니다");

            }
            
        }
    }

    public void TotalMove()
    {
        //FollowPlayer();        
        if (target)
        {            
            monster_State.animator.SetBool("Move", true);
            
            PlayerDirectionCheck();
            Vector2 rayTrans;
            LayerMask groundMarsk = LayerMask.GetMask("Ground");
            if (transform.eulerAngles.y.Equals(0))
            {
                rayTrans = new Vector2(transform.position.x + 1f, transform.position.y - 0.5f);
                Debug.DrawRay(rayTrans, Vector2.down * 0.5f, Color.black);
            }
            else
            {
                rayTrans = new Vector2(transform.position.x - 1f, transform.position.y - 0.5f);
                Debug.DrawRay(rayTrans, Vector2.down * 0.5f, Color.black);
            }


            RaycastHit2D hit = Physics2D.Raycast(rayTrans, Vector2.down, 2.5f, groundMarsk);

            if (hit.collider == null)
            {
                rigidbody.velocity = Vector2.zero;
                return;
            }
            
            Debug.Log("진입");
            rigidbody.velocity = new Vector2(direction* MoveSpeed, rigidbody.velocity.y);                
                        
        }
        else
        {
            WallCheck();
            
            rigidbody.velocity = new Vector2(nextMove * MoveSpeed, rigidbody.velocity.y);
                
            //정지면 방향그대로 한 후 리턴
            if (nextMove == 0)
            {                
                monster_State.animator.SetBool("Move", false);
                return;
            }

            //정지가 아니라면 방향에 맞춰 축변경
            Flip = (nextMove < 0) ? true : false;
            monster_State.animator.SetBool("Move", true);

            if (Flip)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                FlipDirection = -1;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                FlipDirection = 1;
            }
        }
        
    }
    public void PlayerDirectionCheck()
    {
        direction = (targetPlayer.localPosition.x - transform.localPosition.x);
        direction = (direction >= 0) ? 1 : -1;
        if (direction < 1)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void WallCheck()
    {
        //이동 결정
        SelectMove();
        //낭떨어지면
        GroundCheck_Ray();
        
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
   
    private void GroundCheck_Ray()
    {
        if (!groundCheck)
        {
            //바닥에 닿아있지 않다면
            return;
        }
        LayerMask groundMarsk = LayerMask.GetMask("Ground");
        Vector2 rayTrans;
        if (nextMove>0)
        {
            rayTrans = new Vector2(transform.position.x + 1f, transform.position.y-0.5f);
            Debug.DrawRay(rayTrans, Vector2.down * 2.5f, Color.red);
        }
        else 
        {
            rayTrans = new Vector2(transform.position.x - 1f, transform.position.y - 0.5f);
            Debug.DrawRay(rayTrans, Vector2.down * 2.5f, Color.red);
        }


        RaycastHit2D hit = Physics2D.Raycast(rayTrans, Vector2.down, 2.5f, groundMarsk);
        if (!hit)
        {
            nextMove = -nextMove;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals((int)Layer_Index.Ground))
        {
            groundCheck = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals((int)Layer_Index.Ground))
        {
            groundCheck = false;
        }
    }

    public float DistanceAndDirection()
    {
        distance = Vector2.Distance(transform.position, targetPlayer.position);

        return distance;             
    }
}
