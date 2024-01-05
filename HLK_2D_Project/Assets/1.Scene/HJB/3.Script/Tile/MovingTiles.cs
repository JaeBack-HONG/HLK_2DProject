using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTiles : MonoBehaviour
{
    [Range(0, 10f)]
    [SerializeField] private float speed = 0f;

    [Range(0, 100f)]
    [SerializeField] private float distance = 0f;

    [Header("끝지점에서 머무를 시간")]
    [SerializeField] private float stopTime = 0f;

    [Header("움직이는 방향")]
    [SerializeField] private bool horizotal = false;
    [SerializeField] private bool vertical= false;

    [Header("어느 방향으로 먼저")]
    [SerializeField] private bool up = false;
    [SerializeField] private bool down = false;

    [SerializeField] private bool left = false;
    [SerializeField] private bool right = false;

    [Header("대각선 허용(생각중)")]
    [SerializeField] private bool cross = false;



    [Header("현재 상황판")]
    [SerializeField] private float currentTime;

    [SerializeField] private bool stop = false;

    Vector3 direction;

    float lastPoint;
    float startPoint;

    private void Start()
    {
        if (horizotal)
        {
            startPoint = transform.position.x;
            lastPoint = transform.position.x + distance;
            if (!right)
            {
                lastPoint = transform.position.x - distance;
            }
        }
        else if (vertical)
        {
            startPoint = transform.position.y;
            lastPoint = transform.position.y + distance;
            if (!up)
            {
                lastPoint = transform.position.y - distance;
            }
        }        
    }

    private void FixedUpdate()
    {
        if (!stop)
        {
            MovingTiles_Totls();
        }
    }

    private void MovingTiles_Totls()
    {
        if (horizotal)
        {
            Horizontal_MovigTiles();            
        }
        else if (vertical)
        {
            Vertical_MovigTiles();            
        }        
        else
        {
            return;
        }
        transform.position += direction * speed * Time.fixedDeltaTime;
        
    }

    private void Horizontal_MovigTiles()
    {

        if (right)
        {
            if (transform.position.x <= startPoint)
            {            
                direction = Vector3.right;
                StartCoroutine(Wall_WaitTime());
            }
            else if (transform.position.x >= lastPoint)
            {            
                direction = Vector3.left;
                StartCoroutine(Wall_WaitTime());
            }
        }
        else
        {            
            if (transform.position.x >= startPoint)
            {
                direction = Vector3.left;
                StartCoroutine(Wall_WaitTime());
            }
            else if (transform.position.x <= lastPoint)
            {
                direction = Vector3.right;
                StartCoroutine(Wall_WaitTime());
            }
        }        
    }

    private void Vertical_MovigTiles()
    {
        if (up)
        {
            if (transform.position.y <= startPoint)
            {
                direction = Vector3.up;
                StartCoroutine(Wall_WaitTime());
            }
            else if (transform.position.y >= lastPoint)
            {
                direction = Vector3.down;
                StartCoroutine(Wall_WaitTime());
            }
        }
        else
        {
            if (transform.position.y >= startPoint)
            {
                direction = Vector3.down;
                StartCoroutine(Wall_WaitTime());
            }
            else if (transform.position.y <= lastPoint)
            {
                direction = Vector3.up;
                StartCoroutine(Wall_WaitTime());
            }
        }       
    }
    
    private IEnumerator Wall_WaitTime()
    {
        stop = true;
        currentTime = 0;
        while(currentTime<=stopTime)
        {
            currentTime += Time.fixedDeltaTime/2;
            
            yield return null;
        }
        stop = false;
        yield return null;
    }
    
    private void Corss_MovingTiles()
    {
        transform.position += direction * speed * Time.fixedDeltaTime;
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals((int)Layer_Index.Player) )
        {
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals((int)Layer_Index.Player) )
        {
            collision.transform.SetParent(null);
        }
    }
}
