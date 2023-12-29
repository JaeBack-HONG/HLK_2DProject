using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ability : MonoBehaviour
{
    public Ability[] abilities;
    public Ability[] my_Abilities;

    public Ability current_Ab;

    public int current_idx = 0;

    private void Awake()
    {
        my_Abilities = new Ability[3];
        current_Ab = my_Abilities[current_idx];
    }

    public void Choice_Ab()
    {
        if (Input.GetKeyDown(KeyCode.Q)) AbilitySet(0);

        if (Input.GetKeyDown(KeyCode.W)) AbilitySet(1);

        if (Input.GetKeyDown(KeyCode.E)) AbilitySet(2);

    }

    private void AbilitySet(int idx)
    {
        current_idx = idx;
        current_Ab = my_Abilities[current_idx];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            
            my_Abilities[current_idx] = abilities[(int)collision.gameObject.GetComponent<AbilityItem>().itemidx];
            current_Ab = my_Abilities[current_idx];
            Destroy(collision.gameObject);
        }

    }





}
