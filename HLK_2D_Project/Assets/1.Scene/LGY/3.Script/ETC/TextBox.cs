using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    [SerializeField] private GameObject textObj;
    [SerializeField] private Text textUI;

    [Header("출력시킬 Text")]
    [SerializeField] private string text_Message;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            textUI.text = text_Message;
            textObj.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            textObj.SetActive(false);
            textUI.text = string.Empty;
        }
    }

}
