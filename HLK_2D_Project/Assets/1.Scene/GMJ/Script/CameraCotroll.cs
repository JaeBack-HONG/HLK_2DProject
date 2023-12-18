using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCotroll : MonoBehaviour
{
    private Camera camera;
    private GameObject player;

    private void Awake()
    {
        camera = GetComponent<Camera>();
        player = GameObject.Find("Player");
    }

    
    private void LateUpdate()
    {
        camera.transform.position = new Vector3(player.transform.position.x, 0,-10);
    }
}
