using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public bool isCursorVisible;
    
    [ShowOnly] public static int beeCount;
 
	// Use this for initialization
	void Start () {
        beeCount = 10;
        Cursor.visible = isCursorVisible;
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
