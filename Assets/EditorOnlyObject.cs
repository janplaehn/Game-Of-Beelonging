﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorOnlyObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
