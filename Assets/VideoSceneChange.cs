using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoSceneChange : MonoBehaviour {

    public float videoDuration;
    public string nextSceneName;

	void Start () {
        StartCoroutine(ChangeScene(videoDuration));	
	}
	
IEnumerator ChangeScene(float time) {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
    }
}
