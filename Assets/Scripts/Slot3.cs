using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot3 : MonoBehaviour {

    public GameObject bottomSlot;

	void Update () {
        checkForNextSlot();
    }

    void checkForNextSlot() {
        if (!bottomSlot.GetComponent<Slot>().isOccupied) {
            if (!GameObject.Find("AIBee 4")
                && !GameObject.Find("AIBee 5")
                && !GameObject.Find("AIBee 6")
                && !GameObject.Find("AIBee 7")
                && !GameObject.Find("AIBee 8")) {
                this.GetComponent<Slot>().nextSlot = bottomSlot;
            }
        }
    }
}
