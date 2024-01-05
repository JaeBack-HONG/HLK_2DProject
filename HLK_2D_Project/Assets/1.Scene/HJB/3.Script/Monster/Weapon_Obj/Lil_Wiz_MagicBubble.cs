using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lil_Wiz_MagicBubble : MonoBehaviour
{
    [Range(1f, 100f)]
    public float Speed = 2f;

    [SerializeField] private int damage = 2;

    [Range(30f, 100f)]
    [SerializeField] private float removeDistanceSet = 35f;

    [SerializeField] private float slowCoolSet = 1.5f;
    [SerializeField] private float slowSpeedSet = 0.6f;

    

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
        if (collision.gameObject.layer.Equals((int)Layer_Index.Player))
        {
            Player_State playerState = collision.gameObject.GetComponent<Player_State>();
            playerState.Health -= damage;
            playerState.unithit.Hit(playerState.gameObject.layer, transform.position);
            //여기에 플레이어 슬로우 메서드 실행시키기
            playerState.Slow(slowSpeedSet, slowCoolSet);
            StopCoroutine(shot_co);
            Destroy(this.gameObject);
        }

    }
}
