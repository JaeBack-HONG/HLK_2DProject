using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityItem : MonoBehaviour
{
    [SerializeField] private Ability[] abilities;
    [SerializeField] private Sprite[] sprites;

    private SpriteRenderer spriterenderer;
    public Ability ability;

    public Ability_Item itemidx;

    private void Awake()
    {
        transform.parent = null;
    }
    private void Start()
    {
        TryGetComponent<SpriteRenderer>(out spriterenderer);
         
        spriterenderer.sprite = sprites[(int)itemidx];
        ability = abilities[(int)itemidx];
    }
}
