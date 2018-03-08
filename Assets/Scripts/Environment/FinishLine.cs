using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour {

    public string nextSceneName;
    public bool restoreBees;
    public int levelNumber;

    private GameObject MainCamera;
    private GameObject MainBee;

    void Start () {
        MainCamera = GameObject.Find("Main Camera");
        MainBee = GameObject.Find("MainBee");
    }
	
	void Update () {
        if (transform.position.x < MainCamera.transform.position.x + 8)
        {
            MainBee.GetComponent<MainBee>().isInEndSequence = true;
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("AIBee")) {
                go.GetComponent<AIBee>().isInBossFight = true;
            }
            MainCamera.GetComponent<MainCamera>().speed = 0;
            foreach (GameObject bg in GameObject.FindGameObjectsWithTag("Background")) {
                bg.GetComponent<RepeatingBackground>().scrollSpeed = 0;
            }
        }	
	}

    void OnTriggerStay2D(Collider2D otherCollider) {
        if (otherCollider.tag == "MainBee") {
            if (restoreBees) {
                GameManager.restoreBees = true;
            }
            if (levelNumber > GameManager.unlockedLevelNumber) {
                GameManager.unlockedLevelNumber = levelNumber;
                PlayerPrefs.SetInt("unlockedLevelNumber", GameManager.unlockedLevelNumber);
            }
            GameManager.ToggleCursorVisibility(true);
            GameManager.isInLevel = false;
            SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
        }
    }
}
