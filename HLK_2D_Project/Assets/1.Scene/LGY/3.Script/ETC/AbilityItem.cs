using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityItem : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;

    private SpriteRenderer spriterenderer;

    public Ability_Item itemidx;

    private void Awake()
    {
        transform.parent = null;
        TryGetComponent<SpriteRenderer>(out spriterenderer);

        spriterenderer.sprite = sprites[(int)itemidx];
    }
    private void Start()
    {

    }
}
