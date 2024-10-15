using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ManagingOfTheScenes : MonoBehaviour
{
    public void quitGame()
    {
        #if UNITY_STANDALONE
                                Application.Quit();
        #endif
        #if UNITY_EDITOR
                                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    public void goMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void goTutorial()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }

    public void startGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(2);
    }

    public void nextScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void previoiusScene()
    {
        Time.timeScale = 1.0f;
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        } else
        {
            throw new Exception("Cannot go to negative scene");
        }
    }

    public void goToScene(int i)
    {
        Time.timeScale = 1.0f;
        if (SceneManager.GetActiveScene().buildIndex >= 0)
        {
            SceneManager.LoadScene(i);
        } else
        {
            throw new Exception("Cannot go to negative scene");
        }
    }
}
