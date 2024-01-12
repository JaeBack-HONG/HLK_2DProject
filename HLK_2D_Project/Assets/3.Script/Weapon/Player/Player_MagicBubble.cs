using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MagicBubble : Player_Projectile
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
    

    [SerializeField] private float slowSpeedSet = 0.6f;
    [SerializeField] private float slowCoolSet = 1.5f;

    [SerializeField] private float downDamage = 2f;

    private IEnumerator shot_co;

    public void Start_Co(Vector3 directiono)
    {
        shot_co = Shot(directiono);
        StartCoroutine(shot_co);
    }



    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer.Equals((int)Layer_Index.Enemy))
    //    {
    //        Monster_State monsterState = collision.gameObject.GetComponent<Monster_State>();
    //        monsterState.Health -= damage;
    //        monsterState.UnitHit.Hit(monsterState.gameObject.layer, transform.position);
    //        monsterState.Slow(slowSpeedSet, slowSpeedSet);
    //        //여기에 플레이어 슬로우 메서드 실행시키기

    //        StopCoroutine(shot_co);
    //        Destroy(this.gameObject);
    //    }

    //}
}
