using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private GameObject MainCamera;
    private float rightBoundary;
    public float speed;


	void Start () {

        MainCamera = GameObject.Find("Main Camera");
        rightBoundary = MainCamera.GetComponent<CameraMovement>().offset + 10f;
    }
	
	void Update () {
		if (transform.position.x <= rightBoundary)
        {
            transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
        }
        else
        {
            Destroy(gameObject);
        }
	}
}
