using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    Animator animator;

    [SerializeField] private GameObject Item;

    private bool create = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&!create)
        {
            animator.SetTrigger("Open");
            StartCoroutine(ItemCreate_Co());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Close");
            animator.SetTrigger("Default");
        }
    }

    private IEnumerator ItemCreate_Co()
    {
        create = true;
        float currentTime = 0f;

        while (currentTime<0.5f)
        {
            currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        
        Instantiate(Item, transform.position + new Vector3(0, 1f), Quaternion.identity);
        while (currentTime < 5f)
        {
            currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        Destroy(gameObject);
    }
}
