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

    [SerializeField] private CircleCollider2D circlecol;

    private ScopeState scopestate;

    private float currentTime = 0;    // ÇöÀç½Ã°£
    private SpriteRenderer spriterenderer;

    private void Awake()
    {
        TryGetComponent<Rigidbody2D>(out rigidbody);
        TryGetComponent<SpriteRenderer>(out spriterenderer);
        scopestate = ScopeState.Aiming;
        circlecol.enabled = false;
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
            currentTime += Time.fixedDeltaTime;
            Vector2 direction = (target.position - transform.position);
            rigidbody.AddForce(direction * 4.5f, ForceMode2D.Force);
            yield return new WaitForFixedUpdate();
        }
        while (currentTime < 4.2f)
        {
            currentTime += Time.fixedDeltaTime;
            Vector2 direction = (target.position - transform.position);
            rigidbody.AddForce(direction * 2.5f, ForceMode2D.Force);
            yield return new WaitForFixedUpdate();
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

        circlecol.enabled = true;
        Debug.Log("»§");
        yield return new WaitForSeconds(0.1f);
        circlecol.enabled = false;
        yield return new WaitForSeconds(0.7f);
        spriterenderer.color = gradient.Evaluate(0f);
        scopestate = ScopeState.Aiming;
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
        }
    }
}
