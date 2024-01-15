using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [SerializeField] private GameObject rock1;
    [SerializeField] private GameObject rock2;
    [SerializeField] private Rigidbody2D rigid1;
    [SerializeField] private Rigidbody2D rigid2;
    [SerializeField] private Transform maplimit_right;
    [SerializeField] private Transform maplimit_left;

    private void Start()
    {
        RandomCreate();
        Objtrue();
    }

    public void FallRock()
    {
        rigid1.gravityScale = 4;
        rigid2.gravityScale = 4;
        Invoke("Objfalse", 1.5f);
        Invoke("Objtrue", 4f);
    }

    public void RandomCreate()
    {

        float randomx1 = Random.Range(maplimit_left.position.x + 1f, maplimit_right.position.x - 1f);
        float randomx2 = Random.Range(maplimit_left.position.x + 1f, maplimit_right.position.x - 1f);
        rock1.transform.position = new Vector2(randomx1, transform.position.y);
        rock2.transform.position = new Vector2(randomx2, transform.position.y);


    }

    private void Objtrue()
    {
        rock1.SetActive(true);
        rock2.SetActive(true);
    }

    public void Objfalse()
    {
        rock1.SetActive(false);
        rock2.SetActive(false);
        rigid1.gravityScale = 0;
        rigid2.gravityScale = 0;
        RandomCreate();
    }

}
