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
        SceneManager.LoadScene("Start Cutscene", LoadSceneMode.Single);
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
        SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
    }

    public void LevelTwo() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 2", LoadSceneMode.Single);
    }

    public void LevelThree() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 3", LoadSceneMode.Single);
    }

    public void LevelFour() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 4", LoadSceneMode.Single);
    }


    public void LevelFive() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 5", LoadSceneMode.Single);
    }

    public void Credits() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }

    public void Controls() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Controls", LoadSceneMode.Single);
    }

    public void SpikeLevel() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Spike Level", LoadSceneMode.Single);
    }

    public void BossFight() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("End Cutscene", LoadSceneMode.Single);
    }

    public void NextCostume() {
        GameManager.NextCostume();
    }

    public void PreviousCostume() {
        GameManager.PreviousCostume();
    }

    public void Resume() {
        GameObject.Find("_PauseController").GetComponent<PauseScreen>().Resume();
    }

    public void UnlockEverything() {
        GameManager.beeCount = 10;
        GameManager.costume = GameManager.Costumes.King;
        GameManager.isGangsterhatUnlocked = 1;
        PlayerPrefs.SetInt("isGangsterhatUnlocked", 1);
        GameManager.isSombreroUnlocked = 1;
        PlayerPrefs.SetInt("isSombreroUnlocked", 1);
        GameManager.isPartyhatUnlocked = 1;
        PlayerPrefs.SetInt("isPartyhatUnlocked", 1);
        GameManager.isRomanhatUnlocked = 1;
        PlayerPrefs.SetInt("isRomanhatUnlocked", 1);
        GameManager.isKinghatUnlocked = 1;
        PlayerPrefs.SetInt("isKinghatUnlocked", 1);
        GameManager.isPiratehatUnlocked = 1;
        PlayerPrefs.SetInt("isPiratehatUnlocked", 1);
        GameManager.isVikinghatUnlocked = 1;
        PlayerPrefs.SetInt("isVikinghatUnlocked", 1);
        GameManager.unlockedLevelNumber = 100;
    }

    public void ResetData() {
        PlayerPrefs.DeleteAll();
        GameManager.beeCount = 10;
        GameManager.costume = GameManager.Costumes.Default;
        GameManager.isGangsterhatUnlocked = 0;
        GameManager.isSombreroUnlocked = 0;
        GameManager.isPartyhatUnlocked = 0;
        GameManager.isRomanhatUnlocked = 0;
        GameManager.isKinghatUnlocked = 0;
        GameManager.isVikinghatUnlocked = 0;
        GameManager.isPiratehatUnlocked = 0;
        GameManager.unlockedLevelNumber = 0;
    }
}
