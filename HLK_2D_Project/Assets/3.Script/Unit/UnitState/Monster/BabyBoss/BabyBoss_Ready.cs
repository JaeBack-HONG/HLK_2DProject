using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyBoss_Ready : MonoBehaviour
{
    [SerializeField] private Animator babyboss_anime;
    [SerializeField] private Monster_State monstate;
    [SerializeField] private GameObject maplimit_left;
    private Rigidbody2D rigid;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.layer.Equals((int)Layer_Index.Player))
        {
            maplimit_left.SetActive(true);
            collision.gameObject.TryGetComponent<Rigidbody2D>(out rigid);

            rigid.constraints = RigidbodyConstraints2D.FreezeAll;

            StartCoroutine(BB_control());
        }
    }

    IEnumerator BB_control()
    {
        yield return new WaitForSeconds(1.5f);
        babyboss_anime.SetTrigger("Spot");

        yield return new WaitForSeconds(2f);
        babyboss_anime.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        yield return new WaitForSeconds(0.6f);
        babyboss_anime.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(0.6f);
        babyboss_anime.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        yield return new WaitForSeconds(1.5f);
        babyboss_anime.SetTrigger("Trans");
        yield return new WaitForSeconds(2.25f);
        babyboss_anime.SetTrigger("Walk");
        monstate.state = Unit_state.Move;
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        Destroy(gameObject);
        yield return null;
    }
}
