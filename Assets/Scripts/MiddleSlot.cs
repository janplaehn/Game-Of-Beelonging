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
        Destroy(currentBee);
    }

    public GameObject GetBee()
    {
        return currentBee;
    }
}
