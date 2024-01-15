using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Hit : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Player_State p_state;
    private Rigidbody2D rigidbody;
    private bool hit = false;
    Color defaultColor;

    private void Awake()
    {
        TryGetComponent<Player_State>(out p_state);
        TryGetComponent<Rigidbody2D>(out rigidbody);
    }


    public void Hit(int mylayer, Vector3 hitpos)
    {
        if (!hit)
        {
            hit = true;
            TryGetComponent<SpriteRenderer>(out sprite);
            defaultColor = sprite.color;

            StartCoroutine(OnDamage_Co(mylayer, hitpos));
        }

    }

    private IEnumerator OnDamage_Co(int mylayer, Vector3 hitpos)
    {
        
        sprite.color = Color.red;
        if (gameObject.CompareTag("Player"))
        {
            PlayerManager.instance.HeartCheck(p_state.Health);
            p_state.actState = p_state.Health > 0 ? Unit_state.Default : Unit_state.Die;
            Vector2 direction = transform.position.x < hitpos.x ? Vector2.left : Vector2.right;
            Vector2 knockbackdir = new Vector2(direction.x, 2f).normalized;
            rigidbody.velocity = Vector2.zero;
            rigidbody.AddForce(knockbackdir * 10f, ForceMode2D.Impulse);
        }

        gameObject.layer = (int)Layer_Index.Hit;
        yield return new WaitForSeconds(1f);
        sprite.color = defaultColor;

        gameObject.layer = mylayer;
        hit = false;
        yield return null;
    }
}
