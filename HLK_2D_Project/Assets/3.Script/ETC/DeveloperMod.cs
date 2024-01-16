using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperMod : MonoBehaviour
{
    [SerializeField]GameObject[] activeSelf_obj;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ObjectActiveTrue();
        }
    }

    private void ObjectActiveTrue()
    {
        for (int i = 0; i < activeSelf_obj.Length; i++)
        {
            activeSelf_obj[i].SetActive(true);
        }
    }
}
