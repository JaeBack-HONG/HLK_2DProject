using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Hope_Mod : Ability
{
    [SerializeField] private GameObject player_Bullet;

    public override void UseAbility()
    {
        P_state.actState = Unit_state.Default;
        Instantiate(player_Bullet, transform);
        P_state.actState = Unit_state.Idle;
    }
}
