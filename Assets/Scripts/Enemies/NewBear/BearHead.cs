using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearHead : MonoBehaviour {

    public float healthPoints;
    public GameObject leftPaw;
    public GameObject rightPaw;
    public float attackFrequency;


	void Start () {
        InvokeRepeating("Attack", 3.0f, attackFrequency);

    }

    void Update() {
        if (healthPoints <= 0) {
            this.GetComponent<Rigidbody2D>().gravityScale = 2.0f;
            leftPaw.GetComponent<Rigidbody2D>().gravityScale = 2.0f;
            rightPaw.GetComponent<Rigidbody2D>().gravityScale = 2.0f;
        }
        if (transform.position.y <= -12) {
            Destroy(this.gameObject);
        }
    }

    void Attack() {
        Debug.Log("Requested Attack");
        int tempNumber = Random.Range(0, 2);
        if (tempNumber == 0) leftPaw.GetComponent<PawMovement>().battleState = PawMovement.State.Indicate;
        else rightPaw.GetComponent<PawMovement>().battleState = PawMovement.State.Indicate;
    }
}
