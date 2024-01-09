using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = transform.position;
        
        newPosition.x = player.position.x;
        newPosition.y = player.position.y;
    
        transform.position = newPosition;
    }
}
