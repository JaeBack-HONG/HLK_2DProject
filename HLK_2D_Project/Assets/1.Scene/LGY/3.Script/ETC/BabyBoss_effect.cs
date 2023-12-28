using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyBoss_effect : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        animator.SetTrigger("Effect");
    }

    public void GameObjOff()
    {
        gameObject.SetActive(false);
    }
}
