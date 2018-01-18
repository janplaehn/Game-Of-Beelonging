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
    public float speed;
    public bool isCursorVisible;
    private bool isAlive;

    void Start () {
        MainCamera = GameObject.Find("Main Camera");
        leftBoundary = -8.5f;
        rightBoundary = 8.5f;
        topBoundary = 4.75f;
        bottomBoundary = -4.75f;
        Cursor.visible = isCursorVisible;
        isAlive = true;
    }
	
	void Update () {
        if (isAlive)
        {
            Move();
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
            if (Input.GetMouseButtonDown(1))
            {
                CallSwarm();
            }
        }
        else
        {
            if (transform.position.y <= -6) {
                Destroy(this.gameObject);
            }
        }
    }

    void Move() {
        leftBoundary = MainCamera.GetComponent<MainCamera>().offset - 8.5f;
        rightBoundary = MainCamera.GetComponent<MainCamera>().offset + 8.5f;
        newPosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        if (newPosition.x >= leftBoundary && newPosition.x <= rightBoundary && newPosition.y <= topBoundary && newPosition.y >= bottomBoundary) {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, speed);
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
        if (otherCollider.tag == "Enemy" && otherCollider.transform.root.gameObject.GetComponent<Fly>().isAlive && this.isAlive) {
            Die();
        }
    }

    void Die() {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        this.GetComponent<Rigidbody2D>().gravityScale = 2.0f;
        isAlive = false;
    }
}
