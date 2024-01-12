using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MoeScotty_Mod : Ability
{
    [SerializeField] float glideSpeed = 5f;
    [SerializeField] int dmg = 1;

    public override void UseAbility()
    {
        StartCoroutine(MoeAttack());
    }

    private IEnumerator MoeAttack()
    {
        gameObject.layer = (int)Layer_Index.Hit;
        rigidbody.velocity = Vector2.zero;
        P_state.actState = Unit_state.Idle;
        animator.SetTrigger("MoeScotty");
        Input.ResetInputAxes();
        PlayerManager.instance.UsedAb();
        while (P_state.JumState.Equals(Jump_State.Jumping) || P_state.JumState.Equals(Jump_State.Falling))
        {
            rigidbody.velocity = new Vector2(P_state.direction.x * glideSpeed, -0.5f);
            if(Input.GetKeyDown(KeyCode.Z))
            {
                break;
            }
            yield return null;
        }
        
        gameObject.layer = (int)Layer_Index.Player;
        animator.SetTrigger("Idle");
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer.Equals((int)Layer_Index.Enemy))
        {
            rigidbody.velocity = Vector2.zero;
            Monster_State monstate = collision.gameObject.GetComponent<Monster_State>();
            monstate.Health -= dmg; 
            monstate.UnitHit.Hit(monstate.gameObject.layer, transform.position);
            P_state.Health += dmg;
            P_state.Health = P_state.Health > P_state.data.HP ? P_state.data.HP : P_state.Health;
        }
    }

}
