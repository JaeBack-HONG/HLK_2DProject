using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hope_Bullet : MonoBehaviour
{
    [Range(1f, 100f)]
    public float Speed = 2f;

    [SerializeField] private int damage = 2;

    [Range(30f, 100f)]
    [SerializeField] private float removeDistanceSet = 35;

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
        Vector3 defaultDistance = transform.position;
        while (true)
        {
            Vector3 currentDistance = (transform.position - defaultDistance);
            if (Vector3.Magnitude(currentDistance)>removeDistanceSet)
            {
                Destroy(this.gameObject);                
            }
            transform.position += target * Time.deltaTime * Speed;
            yield return null;
        }
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&
            collision.gameObject.layer.Equals((int)Layer_Index.Player))
        {
            Player_State playerState = collision.gameObject.GetComponent<Player_State>();
            playerState.Health -= damage;
            playerState.unithit.Hit(playerState.gameObject.layer,transform.position);
            Destroy(this.gameObject);
        }
        
    }
}
