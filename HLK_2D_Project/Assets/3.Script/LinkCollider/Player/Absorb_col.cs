using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorb_col : MonoBehaviour
{
    [SerializeField] private Player_Ability P_Ab;
    [SerializeField] private Player_State P_state;

    private void Start()
    {
        transform.root.gameObject.TryGetComponent<Player_Ability>(out P_Ab);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("MaxHealthPotion"))
        {
            P_state.MaxHealth += 2;
            P_state.Health += 2;

            PlayerManager.instance.MaxHealthCheck(P_state.MaxHealth);
            PlayerManager.instance.HeartCheck(P_state.Health);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Item"))
        {
            AbilityItem abitem = collision.gameObject.GetComponent<AbilityItem>();
            for (int i = 0; i < P_Ab.my_Abilities.Length; i++)
            {
                if (abitem.itemidx.Equals((int)Ability_Item.Skeleton))
                {
                    transform.root.gameObject.TryGetComponent<Player_Move>(out Player_Move P_Move);
                    P_Move.isUpgradeDash = true;
                    return;
                }
                if (abitem.itemidx.Equals((int)Ability_Item.Bird))
                {
                    transform.root.gameObject.TryGetComponent<Player_Move>(out Player_Move P_Move);
                    P_Move.maxJumps = 2;
                    return;
                }

                if (PlayerManager.instance.AbIdx[i] == abitem.itemidx)
                {
                    PlayerManager.instance.Select_Idx = i;
                    P_Ab.LinkUI(i, collision);
                    return;
                }
            }
            for (int i = 0; i < P_Ab.my_Abilities.Length; i++)
            {
                if (P_Ab.my_Abilities[i] == P_Ab.abilities[0])
                {
                    PlayerManager.instance.Select_Idx = i;
                    break;
                }
            }



            P_Ab.TriggerEvent(PlayerManager.instance.Select_Idx, collision);

        }

    }
}
