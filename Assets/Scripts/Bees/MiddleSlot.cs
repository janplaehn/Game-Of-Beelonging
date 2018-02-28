using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleSlot : MonoBehaviour {

    private GameObject currentBee;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    }

    void OnTriggerEnter2D(Collider2D CollisionCheck) {
        if (CollisionCheck.gameObject.tag == "AIBee") {
            currentBee = CollisionCheck.gameObject;
        }
    }

    public void DestroyBee () {
        if (DestroyBeeInRange()) {
        }
    }

    public GameObject GetBee()
    {
        return currentBee;
    }

    private bool DestroyBeeInRange() {
        Collider2D[] collidersWithinRadius;
        collidersWithinRadius = Physics2D.OverlapCircleAll(new Vector3(transform.position.x, transform.position.y, transform.position.z), 0.5f);
        foreach (Collider2D collider in collidersWithinRadius) {
            if (collider.tag == "AIBee") {
                Destroy(collider.gameObject);
                GetComponent<Slot>().isOccupied = false;
                return true;
            }
        }
        return false;
    }
}
