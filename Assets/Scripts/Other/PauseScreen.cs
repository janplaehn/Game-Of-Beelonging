using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour {

    private bool isPaused = false;
    public GameObject PauseUI;
    public GameObject cursorObject;


	void Start () {
		
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
            if (isPaused) {
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Resume() {
        PauseUI.SetActive(false);
        cursorObject.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
        switch (GameManager.costume) {
            case GameManager.Costumes.Default:
                break;
            case GameManager.Costumes.Sombrero:
                if (GameManager.isSombreroUnlocked == 0) {
                    GameManager.costume = GameManager.Costumes.Default;
                }
                break;
            case GameManager.Costumes.Party:
                if (GameManager.isPartyhatUnlocked == 0) {
                    GameManager.costume = GameManager.Costumes.Default;
                }
                break;
            case GameManager.Costumes.Roman:
                if (GameManager.isPartyhatUnlocked == 0) {
                    GameManager.costume = GameManager.Costumes.Default;
                }
                break;
            case GameManager.Costumes.King:
                if (GameManager.isPartyhatUnlocked == 0) {
                    GameManager.costume = GameManager.Costumes.Default;
                }
                break;
            case GameManager.Costumes.Gangster:
                if (GameManager.isPartyhatUnlocked == 0) {
                    GameManager.costume = GameManager.Costumes.Default;
                }
                break;
            default:
                break;
        }
    }

    void Pause() {
        PauseUI.SetActive(true);
        cursorObject.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.visible = true;
    }
}
