using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hope_Bullet : MonoBehaviour
{
    [Range(1f, 100f)]
    public float Speed = 2f;

    [SerializeField] private int damage = 2;

    [Range(30f, 100f)]
    [SerializeField] private float removeDistanceSet = 35f;

    public IEnumerator shot_co;

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
        if (collision.gameObject.CompareTag("Player") &&
            collision.gameObject.layer.Equals((int)Layer_Index.Player))
        {
            Player_State playerState = collision.gameObject.GetComponent<Player_State>();
            playerState.Health -= damage;
            playerState.unithit.Hit(playerState.gameObject.layer, transform.position);
            StopCoroutine(shot_co);
            Destroy(this.gameObject);
        }

    }
}
