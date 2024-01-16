using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class AttackBox : MonoBehaviour
{
    
    
    [SerializeField] private Monster_State mon_state;
    [SerializeField] private Condition_state state;

    [Header("Poison ����")]
    [SerializeField] private float poison_Cool;
    [SerializeField] private int poison_Damge;

    private void Awake()
    {
        mon_state = transform.root.GetComponent<Monster_State>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.layer.Equals((int)Layer_Index.Player))
        {
            
            Player_State player = collision.gameObject.GetComponent<Player_State>();
            
            //���� ���ؼ� �߰��ؼ� ���� �� �˾Ƽ�
            switch (state)
            {
                case Condition_state.Default:
                    mon_state.Attack(player);
                    break;
                case Condition_state.Poison:
                    player.Poison(poison_Cool, poison_Damge);
                    break;
                case Condition_state.Groggy:
                    mon_state.Attack(player);
                    player.actState = Unit_state.Stun;
                    break;
                
            }

        }
    }
}
