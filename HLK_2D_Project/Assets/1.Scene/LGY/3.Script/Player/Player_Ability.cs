using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player_Ability : MonoBehaviour
{
    public Player_State P_state;
    public Rigidbody2D rigidbody;
    public Animator animator;

    private void Start()
    {
        TryGetComponent<Player_State>(out P_state);
        TryGetComponent<Rigidbody2D>(out rigidbody);
        TryGetComponent<Animator>(out animator);
    }
    public abstract void UseAbility();

}
