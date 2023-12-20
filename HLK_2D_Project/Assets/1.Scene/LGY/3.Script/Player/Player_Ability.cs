using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player_Ability : MonoBehaviour
{
    public Player_State P_state;
    public Rigidbody2D rigidbody;

    private void Awake()
    {
        TryGetComponent<Player_State>(out P_state);
        TryGetComponent<Rigidbody2D>(out rigidbody);
    }
    public abstract void UseAbility(Animator player_ani, int changeidx);

}
