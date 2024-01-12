using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player_BabyBoss_Mod : Ability
{
    [SerializeField] private BoxCollider2D attackbox;

    public override void UseAbility()
    {
        StartCoroutine(BabyBossAttack_co());
    }

    private IEnumerator BabyBossAttack_co()
    {
        DefaulutSet("BBMod");

        gameObject.layer = (int)Layer_Index.Hit;
        PlayerManager.instance.UsedAb();
        yield return new WaitForSeconds(0.55f / anispeed);
        noise.m_AmplitudeGain = 10;
        attackbox.enabled = true;
        yield return new WaitForSeconds(0.2f / anispeed);
        noise.m_AmplitudeGain = 0;
        yield return new WaitForSeconds(0.2f / anispeed);
        attackbox.enabled = false;
        EndSet();
        gameObject.layer = (int)Layer_Index.Player;
        if (P_state.isFairy && PlayerManager.instance.count_List[PlayerManager.instance.current_Count] >= 2)
        {
            P_state.isFairy = false;
            P_state.actState = Unit_state.Attack;
        }
    }

}
