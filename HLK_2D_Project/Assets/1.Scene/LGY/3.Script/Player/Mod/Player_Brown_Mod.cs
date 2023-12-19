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
            collision.gameObject.transform.position = new Vector2(transform.position.x, transform.position.y + 1.5f);

            Time.timeScale = 0.2f;

            collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D otherRigid);
            otherRigid.constraints = RigidbodyConstraints2D.FreezeAll;



            StartCoroutine(Brown_Ab(otherRigid));
        }
    }

    IEnumerator Brown_Ab(Rigidbody2D otherRigid)
    {
        otherRigid.velocity = Vector2.zero;
        

        while (true)
        {
            if( Input.GetKeyDown(KeyCode.LeftArrow))
            {
                randomvec = new Vector2(-1f, 1f).normalized;
            }
            if( Input.GetKeyDown(KeyCode.RightArrow))
            {
                randomvec = new Vector2(1f, 1f).normalized;
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                otherRigid.constraints = RigidbodyConstraints2D.None;
                otherRigid.AddRelativeForce(randomvec * 20f, ForceMode2D.Impulse);
                Time.timeScale = 1f;
                break;
            }
            yield return null;
        }

        yield return null;
    }
}
