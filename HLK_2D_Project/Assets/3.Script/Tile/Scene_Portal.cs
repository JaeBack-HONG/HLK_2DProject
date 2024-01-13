using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene_Name
{
    MainMenu,
    tutorial,
    Stage1,
    Stage2_1,
    Stage2_2,
    Stage3,
    WarriorBoss,
}

public class Scene_Portal : MonoBehaviour
{


    IEnumerator waitSet_co;

    private bool able = true;

    [SerializeField] GameObject portalArrowImage_obj;

    [Header("�̵� �� ����")]
    [SerializeField] private Scene_Name scenename;
    


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
        //�� �̵�       
        Debug.Log("�� �̵�");

        GameManager.instance.DataSave();
        AudioManager.Instance.BGM_Play((int)scenename);
        SceneManager.LoadScene((int)scenename);
        
    }
}
