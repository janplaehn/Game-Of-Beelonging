using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthHitbox : MonoBehaviour {


    public GameObject leftPaw;
    public GameObject rightPaw;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.tag == "PlayerBullet") {
            Destroy(otherCollider.transform.root.gameObject);
            if (GameObject.FindGameObjectsWithTag("Wasp").Length == 0) {
                this.transform.root.GetComponent<BearHead>().isWaspSpawningRequested = true;
                this.transform.root.GetComponent<BearHead>().PlayEatAnimation();
            }

        }
    }
}
