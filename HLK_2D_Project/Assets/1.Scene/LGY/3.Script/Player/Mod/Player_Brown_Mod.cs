using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Brown_Mod : Player_Ability
{
    Vector2 randomvec;

    public override void UseAbility(Animator player_ani, int changeidx)
    {

        player_ani = GameManager.instance.animators[changeidx];




        player_ani = GameManager.instance.animators[(int)Animator_List.Player];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals((int)Layer_Index.Enemy) && collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.transform.position = new Vector2(transform.position.x, transform.position.y + 3f);


            collision.gameObject.TryGetComponent<Monster_State>(out Monster_State M_state);
            collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D otherRigid);
            M_state.state = Unit_state.Grab;
            StartCoroutine(Brown_Ab(otherRigid, M_state));
        }
    }

    IEnumerator Brown_Ab(Rigidbody2D otherRigid, Monster_State M_state)
    {

        while (true)
        {
            rigidbody.velocity = Vector2.zero;
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                randomvec = new Vector2(-1f, 1f).normalized;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                randomvec = new Vector2(1f, 1f).normalized;
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                M_state.state = Unit_state.Idle;
                otherRigid.gravityScale = 4f;
                otherRigid.AddRelativeForce(randomvec * 20f, ForceMode2D.Impulse);
                break;
            }
            yield return null;
        }

        yield return null;
    }
}
