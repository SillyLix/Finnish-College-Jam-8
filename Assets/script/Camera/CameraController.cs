using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float cameraOffset;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        if (offset == Vector3.zero)
        {
            offset = new Vector3(0, 2, -cameraOffset);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;

    }
}

