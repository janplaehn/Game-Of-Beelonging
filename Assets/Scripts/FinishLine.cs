using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour {

    public string nextSceneName;

    private GameObject MainCamera;

	void Start () {
        MainCamera = GameObject.Find("Main Camera");
    }
	
	void Update () {
        if (transform.position.x < MainCamera.transform.position.x + 8)
        {
            MainCamera.GetComponent<MainCamera>().speed = 0;
        }	
	}

    void OnTriggerStay2D(Collider2D otherCollider) {
        if (otherCollider.tag == "MainBee") {
            SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
        }
    }
}
