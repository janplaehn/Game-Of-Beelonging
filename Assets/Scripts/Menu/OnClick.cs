using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClick : MonoBehaviour
{
    public void BackToMenu() {
        Time.timeScale = 1f;
        GameManager.isInLevel = false;
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(GameManager.currentSceneNumber, LoadSceneMode.Single);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LevelSelect() {
        GameManager.beeCount = 9;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level Select", LoadSceneMode.Single);
    }

    public void MainLevel() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Level", LoadSceneMode.Single);
    }

    public void BossFight() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Boss Fight", LoadSceneMode.Single);
    }

    public void Resume() {
        GameObject.Find("_PauseController").GetComponent<PauseScreen>().Resume();
    }
}
