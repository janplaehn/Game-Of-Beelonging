using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleSlot : MonoBehaviour {

    private GameObject currentBee;

    private bool isColliding;

	// Use this for initialization
	void Start () {
        isColliding = false;
		
	}
	
	// Update is called once per frame
	void Update () {
    }

    void OnTriggerEnter2D(Collider2D CollisionCheck) {
        if (CollisionCheck.gameObject.tag == "AIBee") {
            currentBee = CollisionCheck.gameObject;
            isColliding = true;
        }
    }

    void OnTriggerExit2D(Collider2D CollisionCheck) {
        if (CollisionCheck.gameObject.tag == "AIBee") {
            isColliding = false;
        }
    }

    public void DestroyBee () {
        Destroy(currentBee);
        GameManager.beeCount -= 1;
    }

    public GameObject GetBee()
    {
        return currentBee;
    }
}
