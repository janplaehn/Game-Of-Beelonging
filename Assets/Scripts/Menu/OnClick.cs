using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClick : MonoBehaviour
{
    public void BackToMenu() {
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }

    public void Retry()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void Start()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void Options()
    {
        //SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
