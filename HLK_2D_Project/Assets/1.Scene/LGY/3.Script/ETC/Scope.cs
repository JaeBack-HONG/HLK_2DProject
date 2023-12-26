using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    private enum ScopeState
    {
        Default = 0,
        Aiming,
        Shot
    }
    private Rigidbody2D rigidbody;
    [SerializeField] private Transform target;
    [SerializeField] private Gradient gradient;

    [SerializeField] private ScopeState scopestate;

    private float currentTime =0;    // ÇöÀç½Ã°£
    private SpriteRenderer spriterenderer;

    private void Awake()
    {
        TryGetComponent<Rigidbody2D>(out rigidbody);
        TryGetComponent<SpriteRenderer>(out spriterenderer);
        scopestate = ScopeState.Aiming;
    }

    private void FixedUpdate()
    {
        Sniping();
    }

    private void Sniping()
    {
        switch (scopestate)
        {
            case ScopeState.Default:
                break;
            case ScopeState.Aiming:
                StartCoroutine(Aiming_Co());
                break;
            case ScopeState.Shot:
                StartCoroutine(Shot_Co());
                break;
            default:
                break;
        }
    }
    
    private IEnumerator Aiming_Co()
    {
        scopestate = ScopeState.Default;
        while (currentTime < 3f) 
        {
            spriterenderer.color = gradient.Evaluate(currentTime * 0.2f);
            currentTime += Time.deltaTime;
            Vector2 direction = (target.position - transform.position);
            rigidbody.AddForce(direction * 2f, ForceMode2D.Force);
            yield return null;
        }
        currentTime = 0f;
        while (currentTime < 1.2f)
        {
            currentTime += Time.deltaTime;
            Vector2 direction = (target.position - transform.position);
            rigidbody.AddForce(direction * 1.3f, ForceMode2D.Force);
            yield return null;
        }
        currentTime = 0f;
        scopestate = ScopeState.Shot;
        yield return null;

    }
    private IEnumerator Shot_Co()
    {
        scopestate = ScopeState.Default;
        spriterenderer.color = gradient.Evaluate(1f);
        rigidbody.velocity = Vector2.zero;
        Debug.Log("»§");
        yield return new WaitForSeconds(1.5f);
        spriterenderer.color = gradient.Evaluate(0f);
        scopestate = ScopeState.Aiming;
        yield return null;
    }
}
