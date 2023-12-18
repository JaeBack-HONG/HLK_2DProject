using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player_Ability : MonoBehaviour
{
    public virtual void UseAbility(Animator player_ani, int changeidx)
    {
        player_ani = GameManager.instance.animators[changeidx];
        Ability();
        player_ani = GameManager.instance.animators[(int)Animator_List.Player];
    }

    public abstract void Ability();
}
