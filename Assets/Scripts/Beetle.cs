using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : MonoBehaviour {

	void Start () {
        this.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        

    }
	
	void Update () {
		if (transform.position.y <= -6) {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D otherCollider) {
        if (otherCollider.tag == "PlayerBullet")
        {
            Die(); 
        }
    }

    void Die() {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        this.GetComponent<Rigidbody2D>().gravityScale = 2.0f;
    }
}
