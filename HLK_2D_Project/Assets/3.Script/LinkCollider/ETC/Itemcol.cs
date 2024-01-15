using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemcol : MonoBehaviour
{
    [SerializeField] private GameObject absorbGuide;
    private Rigidbody2D rigidbody;

    private void Start()
    {
        TryGetComponent<Rigidbody2D>(out rigidbody);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            absorbGuide.SetActive(true);
        }
        if (collision.gameObject.layer.Equals((int)Layer_Index.Ground))
        {
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            absorbGuide.SetActive(false);
        }
    }
}
