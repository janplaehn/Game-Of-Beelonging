using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


    
    public static GameManager instance = null;
    public static bool isCursorVisible;
    public static bool restoreBees = false;

    private static GameObject mainCamera;
    [ShowOnly] public static int beeCount = 9;
 
	void Awake () {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(this.gameObject);

        Cursor.visible = isCursorVisible;
        Cursor.lockState = CursorLockMode.Confined;
        mainCamera = GameObject.Find("Main Camera");
    }

    private void Start() {
        mainCamera = GameObject.Find("Main Camera");
        getAllBeesOnScreen();
    }

    void Update () {
        if (getAllBeesOnScreen() <= 0) {
            Exit();
        }
	}

    void Exit() {
        SceneManager.LoadScene("Game Over Screen", LoadSceneMode.Single);
        beeCount = 10;
        Cursor.visible = true;
    }

    static public void ToggleCursorVisibility(bool isVisible) {
        isCursorVisible = isVisible;
        Cursor.visible = isCursorVisible;
    }

    static int getAllBeesOnScreen() {
        mainCamera = GameObject.Find("Main Camera");
        GameObject[] bees = GameObject.FindGameObjectsWithTag("AIBee");
        GameObject[] mainbees = GameObject.FindGameObjectsWithTag("MainBee");
        int AIBeeCount = 0;
        foreach (GameObject bee in bees) {
            if (bee.transform.position.x >= mainCamera.GetComponent<MainCamera>().offset - 10) {
                AIBeeCount++;
            }
        }
        beeCount = AIBeeCount + mainbees.Length;
        return AIBeeCount + mainbees.Length;
    }
}
