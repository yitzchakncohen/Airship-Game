using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject menuCanvas;
    void Start()
    {
        
    }
    void Update()
    {
        if(menuCanvas.activeSelf)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    // public void Load()
    // {
    //     savingWrapper.Load();
    // }

    // public void Save()
    // {
    //     savingWrapper.Save();
    // }

    // public void Delete()
    // {
    //     savingWrapper.Delete();
    // }

    public void NewGame()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
