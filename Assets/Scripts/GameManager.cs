using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [ShowOnly] public static int beeCount;

	// Use this for initialization
	void Start () {
        beeCount = 10;
	}
	
	// Update is called once per frame
	void Update () {
        if (beeCount <= 0) {
            SceneManager.LoadScene("Game Over Screen", LoadSceneMode.Single);
            beeCount = 10;
        }
		
	}
}
