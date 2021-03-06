﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour {

    public GameObject nextSlot;
    public GameObject playerSlot;

    [HideInInspector] public bool isOccupied;

    private GameObject MainCamera;
    

    void Start () {
        MainCamera = GameObject.Find("Main Camera");
        isOccupied = true;
    }
	
	void Update () {
        transform.position = new Vector3(transform.position.x + MainCamera.GetComponent<MainCamera>().speed * Time.deltaTime / 100, transform.position.y, transform.position.z);
    }

}
