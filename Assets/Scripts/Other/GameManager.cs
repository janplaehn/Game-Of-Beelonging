using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


    public static GameManager instance = null;
    public bool isCursorVisible;
    
    [ShowOnly] public static int beeCount = 10;
 
	void Awake () {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(this.gameObject);

        Cursor.visible = isCursorVisible;
        Cursor.lockState = CursorLockMode.Confined;
    }
	
	// Update is called once per frame
	void Update () {
        if (beeCount <= 0) {
            Exit();
        }
		
	}

    void Exit() {
        SceneManager.LoadScene("Game Over Screen", LoadSceneMode.Single);
        beeCount = 10;
        Cursor.visible = true;
    }

    public void ToggleCursorVisibility(bool isVisible) {
        isCursorVisible = isVisible;
        Cursor.visible = isCursorVisible;
    }
}
