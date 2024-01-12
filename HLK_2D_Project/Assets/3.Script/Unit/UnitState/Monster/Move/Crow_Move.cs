using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow_Move : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        TryGetComponent<Animator>(out animator);
    }

    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger("Attack");
        }
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            animator.SetTrigger("Death1");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            animator.SetTrigger("Death2");
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            animator.SetTrigger("Hit");
        }
    }
}
