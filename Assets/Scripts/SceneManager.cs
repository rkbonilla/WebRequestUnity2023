using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void Insert()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("InsertScene");
    }

    public void SearchAll()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SearchAllScene");
    }

    public void SearchOne()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SearchOneScene");
    }

    public void UpdateScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("UpdateScene");
    }

    public void Delete()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("DeleteScene");
    }

    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void ExitApp()
    {
        Debug.Log("Application Quit");
        Application.Quit();
    }
}
