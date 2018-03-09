using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationBee : MonoBehaviour {

    public Vector3 direction1;
    public Vector3 direction2;
    public Vector3 direction3;
    public Vector3 direction4;
    public Vector3 direction5;
    public Vector3 direction6;

    private Vector3 currentDirection;
    private bool directionChangeRequested;

    void Start () {
        directionChangeRequested = false;
        StartCoroutine(ChangeDirection(0));
        currentDirection = direction1;
    }

    IEnumerator ChangeDirection(float time) {
        yield return new WaitForSeconds(time);
        int newDirectionNumber = Random.Range(1, 7);
        switch (newDirectionNumber) {
            case 1:
                currentDirection = direction1;
                break;
            case 2:
                currentDirection = direction2;
                break;
            case 3:
                currentDirection = direction3;
                break;
            case 4:
                currentDirection = direction4;
                break;
            case 5:
                currentDirection = direction5;
                break;
            default:
                currentDirection = direction6;
                break;
        }
        directionChangeRequested = true;
    }

    void Update () {
		transform.position += (currentDirection  / currentDirection.magnitude) * Time.deltaTime;

        if (transform.position.x > 10) {
            transform.position += Vector3.left * 19;
        }
        else if (transform.position.x < -10) {
            transform.position += Vector3.right * 19;
        }
        else if (transform.position.y > 3 || transform.position.y < -5) {
            currentDirection = new Vector3(currentDirection.x, currentDirection.y * -1, currentDirection.z);
        }

        if (currentDirection.x <= 0) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (currentDirection.x > 0) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }


        if (directionChangeRequested) {
            directionChangeRequested = false;
            StartCoroutine(ChangeDirection(Random.Range(1, 8)));
        }
    }
}
