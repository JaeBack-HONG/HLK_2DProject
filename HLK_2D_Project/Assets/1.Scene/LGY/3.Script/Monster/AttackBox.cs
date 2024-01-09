using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class HitCollider : MonoBehaviour
{
    
    [SerializeField] private Monster_State mon_state;
    [SerializeField] private Unit_state state;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            Player_State player = collision.gameObject.GetComponent<Player_State>();
            
            //상태 정해서 추가해서 넣을 것 알아서
            switch (state)
            {
                case Unit_state.Default:
                    mon_state.Attack(player);
                    break;
                case Unit_state.Hit:
                    //넉백 추가 해줄 것.
                    mon_state.Attack(player);
                    break;
                case Unit_state.Stun:
                    mon_state.Attack(player);
                    player.Stun(2f);
                    break;
                
            }

        }
    }
}
