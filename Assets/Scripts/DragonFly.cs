﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFly : MonoBehaviour {

    public int healthPoints;
    public float moveSpeed;
    public float positionThreshold;
    public bool isAlive;
    private GameObject MainCamera;

    void Start()
    {
        this.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        isAlive = true;
        healthPoints = 3;
        MainCamera = GameObject.Find("Main Camera");
    }

    void Update() {
        if (transform.position.y <= -6) {
            Destroy(this.gameObject);
        }
        if (isAlive) {
            Move();
            if (healthPoints <= 0 || transform.position.x <= MainCamera.GetComponent<MainCamera>().offset - 10f) {
                Die();
            }
        }
    }

    void OnTriggerStay2D(Collider2D otherCollider) {
        if (otherCollider.tag == "PlayerBullet" && isAlive) {
            Destroy(otherCollider.transform.root.gameObject);
            healthPoints--;
        }
    }

    public void Die() {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        this.GetComponent<Rigidbody2D>().gravityScale = 2.0f;
        isAlive = false;
    }

    void Move() {
        if (transform.position.x < MainCamera.transform.position.x + 10 && transform.position.x > MainCamera.transform.position.x + positionThreshold) {
            transform.position = new Vector3(transform.position.x + moveSpeed/50, transform.position.y, transform.position.z);
        }
        if (transform.position.x < MainCamera.transform.position.x + positionThreshold) {
            transform.position = new Vector3(transform.position.x - moveSpeed, transform.position.y, transform.position.z);
        }
        
    }
}