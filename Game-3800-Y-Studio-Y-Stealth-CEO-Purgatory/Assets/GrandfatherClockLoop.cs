using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandfatherClockLoop : MonoBehaviour
{
    public float timer = 300.0f;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0.0f) {
            AudioSource clockSfx = GetComponent<AudioSource>();
            clockSfx.Play();
            timer = 300.0f;
        }
    }
}
