using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour {

    public GameObject nextSlot;
    public GameObject playerSlot;
    private GameObject MainCamera;
    public bool isOccupied;

    void Start () {
        MainCamera = GameObject.Find("Main Camera");
        isOccupied = true;
    }
	
	void Update () {
        transform.position = new Vector3(transform.position.x + MainCamera.GetComponent<MainCamera>().speed / 100, transform.position.y, transform.position.z);
    }

    void OnTriggerStay2D(Collider2D CollisionCheck) {
        if (CollisionCheck.gameObject.tag == "AIBee" && this == CollisionCheck.gameObject.GetComponent<AIBee>().currentSlot) {
            isOccupied = true;
        }
    }

    void OnTriggerExit2D(Collider2D CollisionCheck) {
        if (CollisionCheck.gameObject.tag == "AIBee" && transform.position.x > MainCamera.transform.position.x -10f) {
            isOccupied = false;
        }
    }
}
