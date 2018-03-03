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
            //TODO: Play eating Animation
            this.transform.root.GetComponent<BearHead>().isWaspSpawningRequested = true;
        }
    }
}
