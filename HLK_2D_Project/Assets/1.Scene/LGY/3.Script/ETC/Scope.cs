using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    [SerializeField] private Transform player;
    float currentTime =0;
    bool test = false;
    public Gradient gradient;
    SpriteRenderer spriterenderer;

    private void Awake()
    {
        TryGetComponent<Rigidbody2D>(out rigidbody);
        TryGetComponent<SpriteRenderer>(out spriterenderer);
    }

    private void FixedUpdate()
    {
        if(!test)
        {
            StartCoroutine(Target());
        }
    }
    
    IEnumerator Target()
    {
        
        test = true;
        while (currentTime < 3f) 
        {
            spriterenderer.color = gradient.Evaluate(currentTime * 0.2f);
            currentTime += Time.deltaTime;
            Vector2 direction = (player.position - transform.position);
            rigidbody.AddForce(direction * 2f, ForceMode2D.Force);
            yield return null;
        }
        currentTime = 0f;
        while (currentTime < 1.2f)
        {
            currentTime += Time.deltaTime;
            Vector2 direction = (player.position - transform.position);
            rigidbody.AddForce(direction * 0.8f, ForceMode2D.Force);
            yield return null;
        }
        spriterenderer.color = gradient.Evaluate(1f);
        rigidbody.velocity = Vector2.zero;
        currentTime = 0f;
        Debug.Log("»§");
        yield return new WaitForSeconds(0.5f);
        spriterenderer.color = gradient.Evaluate(0f);

        test = false; 
        yield return null;
    }
}
