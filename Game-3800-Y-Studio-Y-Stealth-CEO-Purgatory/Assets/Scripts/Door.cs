using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Transform point;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource doorSfx = GetComponent<AudioSource>();
        doorSfx.Play();
        collision.gameObject.transform.position = point.position;
    }
}
