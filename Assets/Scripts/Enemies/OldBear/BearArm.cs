using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearArm : MonoBehaviour {

    public float rotateSpeed;
    public Vector3 rotationDirection;

    private GameObject shoulder;


	void Start () {

        shoulder = transform.parent.gameObject;
    }
	
	void Update () {
	}

    public void MoveArm() {
        transform.RotateAround(shoulder.transform.position, rotationDirection, rotateSpeed * Time.deltaTime);
    }

    public void ResetArm() {
        if (transform.rotation.z > 0) {
            rotationDirection = new Vector3(0, 0, -1);
            transform.RotateAround(shoulder.transform.position, rotationDirection, rotateSpeed * 10 * Time.deltaTime);
        }
    }
   public void ChangeDirection() {
        rotationDirection = new Vector3(0, 0, rotationDirection.z * -1);
    }

    public void Reset() {
        
    }
}
