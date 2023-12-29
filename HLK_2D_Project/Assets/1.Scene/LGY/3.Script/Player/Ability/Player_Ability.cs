using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ability : MonoBehaviour
{
    public Ability[] ability_Temp;
    public List<Ability> abilities;

    public Ability current_Ab;

    private void Awake()
    {
        for (int i = 0; i < ability_Temp.Length; i++)
        {
            abilities.Add(ability_Temp[i]);
        }
        current_Ab = ability_Temp[0];

    }

    public void AbilitySetting()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            current_Ab = abilities[0];
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            current_Ab = abilities[1];
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            current_Ab = abilities[2];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Item"))
        {
        }

    }





}
