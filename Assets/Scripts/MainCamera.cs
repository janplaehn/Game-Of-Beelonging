using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    public float speed;
    public float offset;

	void Start () {
		
	}
	
	void Update () {
        this.transform.position = new Vector3(this.transform.position.x + speed/100, this.transform.position.y, this.transform.position.z);
        offset = transform.position.x;
    }
}
