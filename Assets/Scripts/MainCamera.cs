using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    public float speed;

    [ShowOnly] public float offset;

	void Start () {
		
	}
	
	void Update () {
        this.transform.position = new Vector3(this.transform.position.x + speed/100 * Time.deltaTime, this.transform.position.y, this.transform.position.z);
        offset = transform.position.x;
        if (Input.GetMouseButtonDown(1)) {
            speed *= 3;
        }
        if (Input.GetMouseButtonUp(1))
        {
            speed /= 3;
        }
    }
}
