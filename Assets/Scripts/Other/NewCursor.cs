using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCursor : MonoBehaviour {

    private GameObject MainCamera;

	// Use this for initialization
	void Start () {
        MainCamera = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
        if (MainCamera.GetComponent<MainCamera>().offset - 8.5f < Camera.main.ScreenToWorldPoint(Input.mousePosition).x &&
            MainCamera.GetComponent<MainCamera>().offset + 8.5f > Camera.main.ScreenToWorldPoint(Input.mousePosition).x &&
            4.9 > Camera.main.ScreenToWorldPoint(Input.mousePosition).y &&
            -4.9 < Camera.main.ScreenToWorldPoint(Input.mousePosition).y) {
            transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        }
        else {
            transform.position = new Vector2(transform.position.x + MainCamera.GetComponent<MainCamera>().speed / 100 * Time.deltaTime, transform.position.y);
        }
    }   
}
