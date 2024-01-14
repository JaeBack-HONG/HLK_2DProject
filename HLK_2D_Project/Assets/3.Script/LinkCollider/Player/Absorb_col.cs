using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorb_col : MonoBehaviour
{
    [SerializeField] private Player_Ability P_Ab;

    private void Start()
    {
        transform.root.gameObject.TryGetComponent<Player_Ability>(out P_Ab);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        P_Ab.Absorbcol(collision);
    }
}
