using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Hit : MonoBehaviour
{
    private SpriteRenderer sprite;

    private void Awake()
    {
        TryGetComponent<SpriteRenderer>(out sprite);
    }

    public void Hit(int mylayer)
    {
        StartCoroutine(OnDamage(mylayer));
    }

    private IEnumerator OnDamage(int mylayer)
    {
        gameObject.layer = (int)Layer_Index.Hit;
        sprite.color = Color.red;

        yield return new WaitForSeconds(1f);
        sprite.color = Color.white;
        gameObject.layer = mylayer;

        yield return null;
    }
}
