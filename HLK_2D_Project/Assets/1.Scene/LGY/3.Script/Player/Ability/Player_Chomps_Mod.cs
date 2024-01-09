using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Chomps_Mod : Ability
{
    [Range(1, 4)]
    [SerializeField] private int HealValue = 1;

    public override void UseAbility()
    {
        if (P_state.Health.Equals(P_state.data.HP))
        {
            P_state.actState = Unit_state.Idle;
            return;
        }
        P_state.actState = Unit_state.Default;

        PlayerManager.instance.UsedAb();
        P_state.Health += HealValue;
        P_state.Health = P_state.Health > P_state.data.HP ? P_state.data.HP : P_state.Health;
        PlayerManager.instance.HeartCheck(P_state.Health);
        P_state.actState = Unit_state.Idle;
    }
}
