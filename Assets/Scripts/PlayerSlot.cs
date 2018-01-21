using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlot : MonoBehaviour {


    private Vector3 origin;
    private Vector3 rotationDirection;
    private Transform player;

    void Start () {
        origin = new Vector3(0, 0, 0);
        rotationDirection = new Vector3(0, 0, 1);
        player = this.transform.root.transform;
    }


	void Update () {
        transform.RotateAround(player.position, rotationDirection, 30 * Time.deltaTime);
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
