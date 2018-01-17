using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBee : MonoBehaviour {

    private GameObject MainCamera;
    private Vector2 newPosition;
    private float leftBoundary;
    private float rightBoundary;
    private float topBoundary;
    private float bottomBoundary;
    public Transform bullet;
    public float bulletOffset;

    void Start () {
        MainCamera = GameObject.Find("Main Camera");
        leftBoundary = -8.5f;
        rightBoundary = 8.5f;
        topBoundary = 4.75f;
        bottomBoundary = -4.75f;
    }
	
	void Update () {
        Move();
        if (Input.GetMouseButtonDown(0)) {
            Shoot();
        }
        if (Input.GetMouseButtonDown(1)) {
            CallSwarm();
        }
    }

    void Move() {
        leftBoundary = MainCamera.GetComponent<MainCamera>().offset - 8.5f;
        rightBoundary = MainCamera.GetComponent<MainCamera>().offset + 8.5f;
        newPosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        if (newPosition.x >= leftBoundary && newPosition.x <= rightBoundary && newPosition.y <= topBoundary && newPosition.y >= bottomBoundary) {
            this.transform.position = newPosition;
        }
        else {
            this.transform.position = new Vector3(this.transform.position.x + MainCamera.GetComponent<MainCamera>().speed / 100, this.transform.position.y, this.transform.position.z);
        }
    }

    void Shoot() {
        Instantiate(bullet, new Vector3(transform.position.x + bulletOffset, transform.position.y, transform.position.z), Quaternion.identity);
    }

    void CallSwarm() {

    }

    void OnTriggerStay2D(Collider2D otherCollider) {
        if (otherCollider.tag == "Enemy")
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
