using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    public int currentScene;

    void Awake()
    {
        instance = this;
        currentScene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Active Scene is " + currentScene);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            LoadCurrentLevel();
        }
    }

    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void LoadScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(currentScene + 1);
    }

}
