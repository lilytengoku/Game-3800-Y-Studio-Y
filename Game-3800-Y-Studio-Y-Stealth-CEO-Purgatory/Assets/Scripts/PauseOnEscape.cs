using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;
using System;

public class PauseOnEscape : MonoBehaviour
{
    public GameObject pauseImage;
    public TextMeshProUGUI pauseText;

    private static bool gameLost;

    void Start()
    {
        gameLost = false;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pressedPause();
        }
    }

    public void pressedPause()
    {
        if (gameLost)
        {
            return;
        }
        bool isPaused = !pauseImage.activeSelf;
        pauseText.text = "Game Paused";
        pauseImage.SetActive(isPaused);
        if (isPaused)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }

    }

    public void gameOver()
    {
        gameLost = true;
        pauseText.text = "You were caught!";
        Time.timeScale = 0.0f;
        pauseImage.SetActive(true);
    }

    public void resetGameOver() {
        gameLost = false;
    }
}
