using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBeeMovement : MonoBehaviour {

    private GameObject MainCamera;
    private Vector2 newPosition;
    private float leftBoundary;
    private float rightBoundary;
    private float topBoundary;
    private float bottomBoundary;

    void Start () {
        MainCamera = GameObject.Find("Main Camera");
        leftBoundary = -8.5f;
        rightBoundary = 8.5f;
        topBoundary = 4.75f;
        bottomBoundary = -4.75f;
    }
	
	void Update () {
        leftBoundary = MainCamera.GetComponent<CameraMovement>().offset - 8.5f;
        rightBoundary = MainCamera.GetComponent<CameraMovement>().offset + 8.5f;
        newPosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        if (newPosition.x >= leftBoundary && newPosition.x <= rightBoundary &&  newPosition.y <= topBoundary && newPosition.y >= bottomBoundary)
        {
            this.transform.position = newPosition;
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x + MainCamera.GetComponent<CameraMovement>().speed / 100, this.transform.position.y, this.transform.position.z);
        }
    }
}
