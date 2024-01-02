using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleTiles : MonoBehaviour
{

    [SerializeField] private float F_timeSet;

    [SerializeField] private float M_timeSet;

    [SerializeField] private float L_timeSet;

    [SerializeField] private bool multiple;

    [SerializeField] private bool destroy;

    [SerializeField] private bool check = false;

    Collider2D collider;

    SpriteRenderer sprite;    

    [SerializeField] private Collider2D[] collider_m;
    [SerializeField] private SpriteRenderer[] sprite_m;

    private void Awake()
    {
        if (!multiple)
        {                    
            collider = GetComponent<Collider2D>();
            sprite = GetComponent<SpriteRenderer>();
        }

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

    private IEnumerator InvisibleTileSet_M()
    {
        check = true;
        float currentTime = 0f;
        while (currentTime <= F_timeSet)
        {
            float alpha = Mathf.Lerp(1f, 0, currentTime / F_timeSet);
            for (int i = 0; i < sprite_m.Length; i++)
            {
                sprite_m[i].color = new Color(sprite_m[i].color.r, sprite_m[i].color.g, sprite_m[i].color.b, alpha);
            }
            currentTime += Time.fixedDeltaTime / 2;

            yield return null;
        }

        currentTime = 0f;
        
        for (int i = 0; i < collider_m.Length; i++)
        {
            collider_m[i].enabled = false;
        }
        
        if (destroy)
        {
            Destroy(gameObject);
        }

        yield return new WaitForSeconds(M_timeSet);


        while (currentTime <= L_timeSet)
        {
            float alpha = Mathf.Lerp(0, 1f, currentTime / L_timeSet);
            for (int i = 0; i < sprite_m.Length; i++)
            {
                sprite_m[i].color = new Color(sprite_m[i].color.r, sprite_m[i].color.g, sprite_m[i].color.b, alpha);

            }
            currentTime += Time.fixedDeltaTime / 2;

            yield return null;
        }
        for (int i = 0; i < collider_m.Length; i++)
        {
            collider_m[i].enabled = true;
            sprite_m[i].color = new Color(sprite_m[i].color.r, sprite_m[i].color.g, sprite_m[i].color.b, 255f);
        }
        check = false;
        yield return null;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.gameObject.layer.Equals((int)Layer_Index.Player)&&!check)
        {
            //또는 로직 메서드 만들기
            
            if (multiple)
            {                
                StartCoroutine(InvisibleTileSet_M());
            }
            else
            {
                StartCoroutine(InvisibleTileSet());
            }
        }
    }
}
