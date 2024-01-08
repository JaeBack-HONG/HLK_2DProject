using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [HideInInspector] public Player_State P_state;
    [HideInInspector] public Rigidbody2D rigidbody;
    [HideInInspector] public Animator animator;
    public float anispeed = 1f;

    private void Start()
    {
        TryGetComponent<Player_State>(out P_state);
        TryGetComponent<Rigidbody2D>(out rigidbody);
        TryGetComponent<Animator>(out animator);
    }
    public abstract void UseAbility();

}
