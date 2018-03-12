using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {

    public float scrollSpeed;
    public string nextScene;


	void Start () {
        Time.timeScale = 1.0f;
	}
	
	void Update () {
        transform.position += Vector3.up * scrollSpeed * Time.deltaTime;
        if (transform.position.y > 1000) {
            Time.timeScale = 1f;
            GameManager.isInLevel = false;
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        }
	}
}
