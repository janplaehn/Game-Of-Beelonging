﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour {

    public float healthPoints;
    public float moveSpeed;
    public float leftBoundary;
    public float rightBoundary;
    public float targetHeight1;
    public float targetHeight2;
    public float targetHeight3;
    public float timeVulnerable;

    public GameObject leftarm;
    public GameObject rightarm;
    public GameObject hitbox;

    public enum State {AttackForward, AttackBackward, SpawnEnemies, Defend};
    [HideInInspector] public State fightState;

    private int attackNumber;
    private float currentTargetHeight;



    void Start () {
        fightState = State.Defend;
        StartCoroutine(ChangeState(State.AttackForward));
        attackNumber = 0;
        
    }
	
	void Update () {
        switch (fightState) {
            case State.AttackForward:
                hitbox.SetActive(false);
                rightarm.GetComponent<BearArm>().rotationDirection = new Vector3(0, 0, 1);
                leftarm.GetComponent<BearArm>().rotationDirection = new Vector3(0, 0, 1);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(leftBoundary, currentTargetHeight, 0), moveSpeed * Time.deltaTime);
                leftarm.GetComponent<BearArm>().MoveArm();
                rightarm.GetComponent<BearArm>().MoveArm();
                if (transform.position.x <= leftBoundary) {
                    fightState = State.AttackBackward;
                }
                break;
            case State.AttackBackward:
                hitbox.SetActive(false);
                rightarm.GetComponent<BearArm>().rotationDirection = new Vector3(0, 0, -1);
                leftarm.GetComponent<BearArm>().rotationDirection = new Vector3(0, 0, -1);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(rightBoundary, 0, 0), moveSpeed * Time.deltaTime);
                leftarm.GetComponent<BearArm>().MoveArm();
                rightarm.GetComponent<BearArm>().MoveArm();
                if (transform.position.x >= rightBoundary) {
                    attackNumber++;
                    if (attackNumber > 3) {
                        currentTargetHeight = SetRandomHeight();
                        fightState = State.Defend;
                        hitbox.SetActive(true);
                        attackNumber = 0;
                        StartCoroutine(ChangeState(State.AttackForward));
                    }
                    else {
                        currentTargetHeight = SetRandomHeight();
                        fightState = State.AttackForward;
                    }
                   
                }
                break;
            case State.SpawnEnemies:
                break;
            case State.Defend:
                rightarm.GetComponent<BearArm>().ResetArm();
                leftarm.GetComponent<BearArm>().ResetArm();
                break;
            default:
                break;
        }

        if (healthPoints <= 0) {
            this.GetComponent<Rigidbody2D>().gravityScale = 2.0f;
            leftarm.GetComponent<Rigidbody2D>().gravityScale = 2.0f;
            rightarm.GetComponent<Rigidbody2D>().gravityScale = 2.0f;
        }

        if (transform.position.y <= -12) {
            Destroy(this.gameObject);
        }

    }

    IEnumerator ChangeState(State st) {
        yield return new WaitForSeconds(timeVulnerable);
        fightState = st;     
    }

    float SetRandomHeight() {
        int temp = Random.Range(0, 3);
        switch (temp) {
            case 0:
                return targetHeight1;
            case 1:
                return targetHeight2;
            case 2:
                return targetHeight3;
            default:
                return targetHeight1;
        }
    }

    public void OnTriggerStay(Collider other) {
        if (other.tag == "PlayerBullet") {
            Destroy(other.gameObject);
        }
    }
}
