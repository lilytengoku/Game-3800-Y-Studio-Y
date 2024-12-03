using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ManagingOfTheScenes : MonoBehaviour, IDataPersistence
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

    public void StartNewGame()
    {
        Time.timeScale = 1.0f;
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadScene(1);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1.0f;
        //DataPersistenceManager.instance.LoadGame();
        SceneManager.LoadScene(1);
    }

    public static void nextScene()
    {
        Time.timeScale = 1.0f;
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void previoiusScene()
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

    public static void goToScene(int i)
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

    public void retryFromSave() {
        Time.timeScale = 1.0f;
        //Debug.Log("Retry from save.\nLast saved scene = " + SceneManager.GetActiveScene().buildIndex);
        DataPersistenceManager.instance.LoadGame();
    }

    public void LoadData(GameData data) {
        Debug.Log("Loading saved scene: " + data.activeScene);
        if (data.activeScene != SceneManager.GetActiveScene().buildIndex && SceneManager.GetActiveScene().buildIndex != 0) {
            goToScene(data.activeScene);
        }
    }

    public void SaveData(ref GameData data) {
        data.activeScene = SceneManager.GetActiveScene().buildIndex;
    }

}
