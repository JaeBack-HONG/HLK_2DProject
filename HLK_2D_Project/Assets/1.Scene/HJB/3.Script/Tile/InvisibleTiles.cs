using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleTiles : MonoBehaviour
{

    [SerializeField] private float F_timeSet;

    [SerializeField] private float M_timeSet;

    [SerializeField] private float L_timeSet;

    [SerializeField] private bool check = false;

    Collider2D collider;

    SpriteRenderer sprite;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private IEnumerator InvisibleTileSet()
    {
        check = true;
        float currentTime = 0f;
        while (currentTime <= F_timeSet)
        {
            float alpha = Mathf.Lerp(1f, 0, currentTime/F_timeSet);
            sprite.color = new Color(sprite.color.r,sprite.color.g, sprite.color.b, alpha);
            currentTime += Time.fixedDeltaTime/2;

            yield return null;
        }

        currentTime = 0f;
        collider.enabled = false;
        yield return new WaitForSeconds(M_timeSet);


        while (currentTime <= L_timeSet)
        {
            float alpha = Mathf.Lerp(0, 1f, currentTime / L_timeSet);
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
            currentTime += Time.fixedDeltaTime / 2;

            yield return null;
        }
        collider.enabled = true;
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 255f);
        check = false;
        yield return null;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("진입");
        if (collision.gameObject.layer.Equals((int)Layer_Index.Player)&&!check)
        {            
            //또는 로직 메서드 만들기
            StartCoroutine(InvisibleTileSet());
        }
    }
}
