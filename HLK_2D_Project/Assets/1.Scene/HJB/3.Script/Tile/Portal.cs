using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] GameObject portalArrowImage_obj;
    [Header("위치 설정")]
    [SerializeField] Vector2 teleportVectorSet;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            portalArrowImage_obj.SetActive(true);
        }                
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&Input.GetKey(KeyCode.UpArrow))
        { 
            collision.transform.position = teleportVectorSet;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            portalArrowImage_obj.SetActive(false);
        }
    }



}
