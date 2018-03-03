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
        SceneManager.LoadScene(GameManager.currentSceneNumber, LoadSceneMode.Single);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LevelSelect() {
        SceneManager.LoadScene("Level Select", LoadSceneMode.Single);
    }

    public void MainLevel() {
        SceneManager.LoadScene("Main Level", LoadSceneMode.Single);
    }

    public void BossFight() {
        SceneManager.LoadScene("Boss Fight", LoadSceneMode.Single);
    }
}
