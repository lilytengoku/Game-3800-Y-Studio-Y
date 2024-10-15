using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PauseOnEscape : MonoBehaviour
{
    public GameObject pauseImage;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pressedPause();
        }
    }

    public void pressedPause()
    {
        bool isPaused = pauseImage.activeSelf;
        pauseImage.SetActive(!isPaused);
        if (isPaused)
        {
            Time.timeScale = 1.0f;
        } else
        {
            Time.timeScale = 0.0f;
        }
    }
}
