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
        StartCoroutine(Heal());
    }

    private IEnumerator Heal()
    {

        P_state.actState = Unit_state.Default;

        PlayerManager.instance.UsedAb();
        P_state.Health += HealValue;
        P_state.Health = P_state.Health > P_state.data.HP ? P_state.data.HP : P_state.Health;
        PlayerManager.instance.HeartCheck(P_state.Health);
        P_state.actState = Unit_state.Idle;

        if (P_state.isFairy && PlayerManager.instance.count_List[PlayerManager.instance.current_Count] >= 2)
        {
            P_state.isFairy = false;
            P_state.actState = Unit_state.Attack;
        }

        yield return null;
    }

}
