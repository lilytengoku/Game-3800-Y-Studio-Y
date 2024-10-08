using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform cameraFollow;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(cameraFollow.position.x, cameraFollow.position.y, transform.position.z);
    }
}
