﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearHitbox : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.tag == "PlayerBullet") {
            Destroy(otherCollider.transform.root.gameObject);
            this.transform.root.GetComponent<BearHead>().healthPoints--;
            this.transform.root.GetComponent<BearHead>().PlayHitAnimation();
            this.transform.root.GetComponent<BearHead>().StartAttackInSeconds(1f);
            this.gameObject.SetActive(false);
        }
    }
}
