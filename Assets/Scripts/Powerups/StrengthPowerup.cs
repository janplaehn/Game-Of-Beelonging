using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthPowerup : MonoBehaviour {

    public float powerupTime;

    private GameObject mainCamera;

    void Start() {
        mainCamera = GameObject.Find("Main Camera");
        this.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
    }

    void Update() {
        if (transform.position.x < mainCamera.GetComponent<MainCamera>().offset + 15) {
            this.GetComponent<Rigidbody2D>().gravityScale = 0.05f;
        }
        if (transform.position.x < mainCamera.GetComponent<MainCamera>().offset - 15) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.tag == "MainBee" && otherCollider.transform.GetComponent<MainBee>().isAlive) {
            otherCollider.gameObject.GetComponent<MainBee>().hasPowerup = true;
            otherCollider.gameObject.GetComponent<MainBee>().SetPowerupTimer(powerupTime);
            Destroy(gameObject);
        }
    }
}
