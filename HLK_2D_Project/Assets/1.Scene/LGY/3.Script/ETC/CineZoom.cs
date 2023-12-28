using Cinemachine;
using UnityEngine;

public class CineZoom : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinevir;

    [SerializeField] private int priority;

    [SerializeField] private bool remove;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.layer.Equals((int)Layer_Index.Player))
        {
            cinevir.Priority = priority;
            if (remove) Destroy(gameObject);
        }
    }
}
