using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mon_Tracy_Arrow : Monster_Projectile
{
    [Range(1f, 100f)]
    public float Speed = 2f;

    [Range(30f, 100f)]
    [SerializeField] private float removeDistanceSet = 35f;

    public IEnumerator shot_co;

    public void Start_Co(Vector3 direction)
    {
        transform.rotation = direction.x.Equals(1) ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 180, 0, 0);
        shot_co = Shot(direction);
        StartCoroutine(shot_co);
    }

    public IEnumerator Shot(Vector3 target)
    {

        Vector3 defaultDistance = transform.position;
        while (true)
        {
            Vector3 currentDistance = (transform.position - defaultDistance);
            if (Vector3.Magnitude(currentDistance) > removeDistanceSet)
            {
                break;
            }
            transform.position += target * Time.deltaTime * Speed;
            yield return null;
        }
        Destroy(this.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") &&
            collision.gameObject.layer.Equals((int)Layer_Index.Player))
        {
            Player_State playerState = collision.gameObject.GetComponent<Player_State>();
            //여기에 플레이어 독 데미지 불러오기

            StopCoroutine(shot_co);
            Destroy(this.gameObject);
        }

    }
}
