using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Portal : MonoBehaviour
{
    IEnumerator waitSet_co;

    private bool able = true;

    [SerializeField] GameObject portalArrowImage_obj;

    [Header("¿Ãµø æ¿ º≥¡§")]
    [SerializeField] private string sceneName;
    


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
        if (collision.gameObject.CompareTag("Player") &&
            Input.GetKey(KeyCode.UpArrow) && able)

        {
            able = false;
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
        //æ¿ ¿Ãµø       
        Debug.Log("æ¿ ¿Ãµø");
        SceneManager.LoadScene(sceneName);
        
    }
}
