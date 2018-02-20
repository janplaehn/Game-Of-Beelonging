using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    public float speed;

    private GameObject MainCamera;
    private float rightBoundary;


	void Start () {

        MainCamera = GameObject.Find("Main Camera");
        rightBoundary = MainCamera.GetComponent<MainCamera>().offset + 12f;
    }
	
	void Update () {
		if (transform.position.x <= rightBoundary)
        {
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else
        {
            Destroy(gameObject);
        }
	}
}
