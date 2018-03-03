using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThistleRemoval : MonoBehaviour {

    private GameObject MainCamera;

	// Use this for initialization
	void Start () {
        MainCamera = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x <= MainCamera.GetComponent<MainCamera>().offset - 11f) {
            Destroy(gameObject);
        }
    }
}
