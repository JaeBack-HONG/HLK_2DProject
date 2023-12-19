using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brown_Attack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = new Vector2(transform.position.x, transform.position.y + 1.5f);

            collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D otherRigid);


            StartCoroutine(Brown_Ab(otherRigid));
        }
    }

    IEnumerator Brown_Ab(Rigidbody2D otherRigid)
    {
        otherRigid.velocity = Vector2.zero;
        otherRigid.gameObject.TryGetComponent<Player_Move>(out Player_Move playermove);
        playermove.isGrab = true;
        float random = Random.Range(0, 2).Equals(0) ? -1f : 1f;
        yield return new WaitForSeconds(2f);
        Vector2 randomvec = new Vector2(random, 1f).normalized;
        
        otherRigid.AddRelativeForce(randomvec * 20f, ForceMode2D.Impulse);

        yield return null;
    }


}
