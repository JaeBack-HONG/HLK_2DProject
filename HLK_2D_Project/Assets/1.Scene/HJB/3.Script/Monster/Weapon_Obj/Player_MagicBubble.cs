using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MagicBubble : MonoBehaviour
{
    [Range(1f, 100f)]
    public float Speed = 2f;

    [SerializeField] private int damage = 2;

    [Range(30f, 100f)]
    [SerializeField] private float removeDistanceSet = 35f;

    [SerializeField] private float slowSpeedSet = 0.6f;
    [SerializeField] private float slowCoolSet = 1.5f;

    [SerializeField] private float downDamage = 2f;

    private IEnumerator shot_co;

    public void Start_Co(Vector3 directiono)
    {
        shot_co = Shot(directiono);
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
        if (collision.gameObject.layer.Equals((int)Layer_Index.Enemy))
        {
            Monster_State monsterState = collision.gameObject.GetComponent<Monster_State>();
            monsterState.Health -= damage;
            monsterState.UnitHit.Hit(monsterState.gameObject.layer, transform.position);
            monsterState.Slow(slowSpeedSet, slowSpeedSet);
            //���⿡ �÷��̾� ���ο� �޼��� �����Ű��

            StopCoroutine(shot_co);
            Destroy(this.gameObject);
        }

    }
}
