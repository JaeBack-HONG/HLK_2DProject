using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Portal : MonoBehaviour
{
    IEnumerator waitSet_co;

    private bool able = true;

    [SerializeField] GameObject portalArrowImage_obj;
    [Header("위치 설정")]
    [SerializeField] Vector2 teleportVectorSet;


    private void coroutineSet(Collider2D col)
    {
        waitSet_co = WaitSet_Co(col);
        StartCoroutine(waitSet_co);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            portalArrowImage_obj.SetActive(true);
        }                
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&
            Input.GetKey(KeyCode.UpArrow)&& able)
            
        {
            able = false;
            Debug.Log(able);
            coroutineSet(collision);            
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            portalArrowImage_obj.SetActive(false);
        }
    }

    private IEnumerator WaitSet_Co(Collider2D col)
    {

        float currentTime = 0f;
        while (currentTime < 0.1f)
        {
            currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        col.transform.position = teleportVectorSet;
        currentTime = 0f;

        while (currentTime<0.3f)
        {
            currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        able = true;        
    }

    

    



}
