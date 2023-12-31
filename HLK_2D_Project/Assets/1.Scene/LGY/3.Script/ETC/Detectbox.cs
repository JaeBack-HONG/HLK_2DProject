using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detectbox : MonoBehaviour
{
    public List<Monster_State> monstates;
    [SerializeField] private float stuncool = 5f;
    [SerializeField] private Player_State P_state;

    private void OnEnable()
    {
        monstates = new List<Monster_State>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals((int)Layer_Index.Enemy))
        {
            monstates.Add(collision.gameObject.GetComponent<Monster_State>());

            for (int i = 0; i < monstates.Count; i++)
            {
                if (!transform.root.rotation.y.Equals(monstates[i].transform.rotation.y))
                {
                    monstates[i].Stun(stuncool);
                }
            }
        }
    }
}
