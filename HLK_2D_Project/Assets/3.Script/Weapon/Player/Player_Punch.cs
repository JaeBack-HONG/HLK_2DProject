using UnityEngine;

public class Player_Punch : MonoBehaviour
{
    [Range(2, 5)]
    [SerializeField] private int damage = 4;

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals((int)Layer_Index.Enemy))
        {
            Monster_State monstate = collision.gameObject.GetComponent<Monster_State>();

            if (monstate != null)
            {
                monstate.Health -= damage;
                monstate.UnitHit.Hit(monstate.gameObject.layer, transform.position);
            }
        }
    }
}
