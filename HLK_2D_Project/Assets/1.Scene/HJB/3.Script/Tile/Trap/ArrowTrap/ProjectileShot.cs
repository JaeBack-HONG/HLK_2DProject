using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShot : MonoBehaviour
{

    [Range(1f, 100f)]
    public float Speed = 2f;

    [SerializeField] private int damage = 2;

    [Header("대기 시간")]
    [SerializeField] private float timeSet = 3f;
    [Header("사거리")]
    [SerializeField] private float distanceSet = 10f;
    [Header("탐지 범위")]
    [SerializeField] private float detection = 10f;
    [Header("날라가능 방향 설정")]
    [SerializeField] private bool up;
    [SerializeField] private bool down;
    [SerializeField] private bool left;
    [SerializeField] private bool right;

    [SerializeField] private Vector3 direction;

    private float currentTime = 0f;

    private bool Shot = false;

    private bool isMove = false;

    Vector3 startPoint;

    IEnumerator corutine;
    IEnumerator corutine_w;
    private void Start()
    {
        if (up)
        {
            direction = Vector3.up;
            transform.rotation = Quaternion.Euler(0, 0, 45);
        }
        else if (down)
        {
            direction = Vector3.down;
            transform.rotation = Quaternion.Euler(0, 0, 225);
        }
        else if (left)
        {
            direction = Vector3.left;
            transform.rotation = Quaternion.Euler(0, 0, 135);
        }
        else
        {
            direction = Vector3.right;
            transform.rotation = Quaternion.Euler(0, 0, 315);
        }

        startPoint = transform.position;

        corutine = Arrow_Shot();
        corutine_w = Arrow_Wait();
    }


    private void FixedUpdate()
    {
        if (!isMove)
        {
            Player_Check();
        }
        if (Shot)
        {
            StartCoroutine(corutine);
        }
        DistanceCheck();
    }


    private void Player_Check()
    {
        LayerMask playerMarsk = LayerMask.GetMask("Player");
        Debug.DrawRay(transform.position, direction * detection, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detection, playerMarsk);

        if (hit.collider != null && !isMove)
        {
            Shot = true;
        }
    }

    private IEnumerator Arrow_Shot()
    {
        isMove = true;
        Shot = false;
        while (true)
        {
            transform.position += direction * Time.fixedDeltaTime * Speed;
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator Arrow_Wait()
    {
        while (currentTime <= timeSet)
        {
            currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        currentTime = 0;
        isMove = false;
    }

    private void DistanceCheck()
    {
        Vector3 distance = startPoint - transform.position;

        if (distanceSet < Vector3.Magnitude(distance))
        {
            corutine_w = Arrow_Wait();
            StopCoroutine(corutine);
            corutine = Arrow_Shot();
            transform.position = startPoint;
            StartCoroutine(corutine_w);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals((int)Layer_Index.Player))
        {
            Player_State playerState = collision.gameObject.GetComponent<Player_State>();
            playerState.Health -= damage;
            playerState.unithit.Hit(playerState.gameObject.layer, transform.position);

            corutine_w = Arrow_Wait();
            StopCoroutine(corutine);
            corutine = Arrow_Shot();
            transform.position = startPoint;
            StartCoroutine(corutine_w);
        }


    }
}
