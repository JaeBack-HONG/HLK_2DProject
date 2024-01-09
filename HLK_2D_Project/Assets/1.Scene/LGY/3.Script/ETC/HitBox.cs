using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitBox : MonoBehaviour
{
    public Player_State P_state;

    [Range(1, 5)]
    [SerializeField] private int dmg = 2;

    private void Start()
    {
        transform.root.TryGetComponent<Player_State>(out P_state);
    }

    public void HitDmg(Monster_State monstate)
    {
        monstate.Health -= dmg;
        P_state.Attack(monstate);
    }

}
