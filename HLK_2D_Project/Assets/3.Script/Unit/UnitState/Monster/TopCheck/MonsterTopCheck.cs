using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTopCheck : MonoBehaviour
{
    [SerializeField] private GameObject topCheck_obj;
    [SerializeField] private float coolTime=3f;
    IEnumerator waitTopCheck_co;
    string currentTag;
    private void Awake()
    {
        topCheck_obj = gameObject;
    }
    private void Start()
    {
        currentTag = gameObject.tag;
        waitTopCheck_co = WaitTopCheck_Co();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&gameObject.CompareTag("Top"))
        {            
            StartCoroutine(waitTopCheck_co);
        }
    }
    
    private IEnumerator WaitTopCheck_Co()
    {
        
        yield return new WaitForSeconds(0.3f);
        topCheck_obj.tag = "Untagged";
        yield return new WaitForSeconds(coolTime);
        topCheck_obj.tag = currentTag;
        waitTopCheck_co = WaitTopCheck_Co();
    }



}
