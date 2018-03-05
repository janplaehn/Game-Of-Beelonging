using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour {

    private bool isPaused = false;
    public GameObject PauseUI;
    public GameObject cursorObject;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
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
    }

    void Pause() {
        PauseUI.SetActive(true);
        cursorObject.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.visible = true;
    }
}
