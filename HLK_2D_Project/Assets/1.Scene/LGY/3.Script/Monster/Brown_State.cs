using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brown_State : MonoBehaviour
{
    public UnitData data;

    private void Start()
    {
        data = new UnitData
            (name: "Brown", hp: 5, detection: 3, range: 0.5f, attackSpeed: 2,
            strength: 2, moveSpeed: 1, jumpForce: 0);
    }

    public void Die()
    {

    }
}
