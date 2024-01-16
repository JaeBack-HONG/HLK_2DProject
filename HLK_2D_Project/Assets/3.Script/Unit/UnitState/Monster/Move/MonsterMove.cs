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
    public bool isGrab = false;

    public bool groundCheck = false;

    private int FlipDirection = 1;

    public Transform targetPlayer;    

    private Monster_State monster_State;

    RaycastHit2D groundHit;

    public float jumpUnit;
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
                Debug.LogError("���� ���� Player �����ϴ�");

            }
            
        }
    }

    public void TotalMove()
    {
        //FollowPlayer();        
        if (target)
        {            
            monster_State.animator.SetBool("Move", true);
            Vector3 WaitDirection = (targetPlayer.localPosition - transform.localPosition);
            float WaitDirection_x = WaitDirection.x;
            
            if (WaitDirection_x > 2f || WaitDirection_x < -2f)
            {
                PlayerDirectionCheck();                
            }
            //direction = (WaitDirection_x<=0)?-1:1;
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

            if (hit.collider == null&&jumpUnit.Equals(0))
            {
                rigidbody.velocity = Vector2.zero;
                return;
            }               
           
            rigidbody.velocity = new Vector2(direction * MoveSpeed, rigidbody.velocity.y);
        }
        else
        {
            WallCheck();
            
            rigidbody.velocity = new Vector2(nextMove * MoveSpeed, rigidbody.velocity.y);
                
            //������ ����״�� �� �� ����
            if (nextMove == 0)
            {                
                monster_State.animator.SetBool("Move", false);
                return;
            }

            //������ �ƴ϶�� ���⿡ ���� �ຯ��
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
        //�̵� ����
        SelectMove();
        //����������
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
            //�ٴڿ� ������� �ʴٸ�
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
