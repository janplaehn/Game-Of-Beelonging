﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    private GameObject MainCamera;
    private float leftBoundary;
    public float speed;

    void Start() {
        MainCamera = GameObject.Find("Main Camera");
        leftBoundary = MainCamera.GetComponent<MainCamera>().offset - 10f;
    }

    void Update() {
        if (transform.position.x >= leftBoundary) {
            transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
        }
        else {
            Destroy(gameObject);
        }
    }
}