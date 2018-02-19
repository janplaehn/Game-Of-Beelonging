using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalBeeSpawning : MonoBehaviour {

    public Transform bee;

    

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D otherCollider) {
        if (otherCollider.tag == "MainBee") {
            if (GameManager.beeCount <= 10)
            Instantiate(bee, transform.position, Quaternion.identity);
            bee.GetComponent<AIBee>().beeState = AIBee.State.MoveToSlot;
            Destroy(this.gameObject);
        }
    }
}
