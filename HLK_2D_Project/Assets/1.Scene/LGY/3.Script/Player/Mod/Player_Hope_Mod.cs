using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Hope_Mod : Player_Ability
{
    public override void UseAbility()
    {

        animator = GameManager.instance.animators[1];

        // ��ɱ���


        animator = GameManager.instance.animators[(int)Animator_List.Player];
    }
}
