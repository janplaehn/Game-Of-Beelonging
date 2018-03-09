using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThistleCollision : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.tag == "PlayerBullet" && otherCollider.GetComponent<PlayerBullet>().isAlive) {
            otherCollider.GetComponent<PlayerBullet>().Die();
        }
        if (otherCollider.tag == "EnemyBullet") {
            Destroy(otherCollider.gameObject);
        }
    }
}
