using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State : MonoBehaviour
{
    public void Player_HealthCheck()
    {

        Die();
    }

    public void Die()
    {
        Destroy(gameObject, 1f);
    }
}
