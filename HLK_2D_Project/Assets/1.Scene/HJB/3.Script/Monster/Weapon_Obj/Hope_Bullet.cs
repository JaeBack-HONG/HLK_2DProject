using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hope_Bullet : MonoBehaviour
{
    [Range(1f, 100f)]
    public float Speed = 2f;

    [SerializeField] private int damage = 2;

    private GameObject target;    

    private void Awake()
    {
        target = GameObject.FindWithTag("Player");
    }

    private void Start()
    {

        Vector3 targetDirection = (target.transform.position - transform.position ).normalized;
       
        StartCoroutine(Shot(targetDirection));
    }

    private IEnumerator Shot(Vector3 target)
    {        
        while (true)
        {
            transform.position += target * Time.deltaTime * Speed;
            yield return null;
        }
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player_State playerState = collision.gameObject.GetComponent<Player_State>();
            playerState.Health -= damage;
            playerState.unithit.Hit(playerState.gameObject.layer);
        }
        if (collision.gameObject.layer.Equals((int)Layer_Index.Player) || 
            collision.gameObject.layer.Equals((int)Layer_Index.Ground))
        {
            Destroy(this.gameObject);
        }
    }
}
