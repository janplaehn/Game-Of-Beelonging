using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour {

    public float healthPoints;
    public float moveSpeed;
    public Vector3 leftBoundary;
    public Vector3 rightBoundary;

    public GameObject leftarm;
    public GameObject rightarm;
    public GameObject hitbox;

    public enum State {AttackForward, AttackBackward, SpawnEnemies, Defend};
    [ShowOnly] public State fightState;

    private int attackNumber;


	void Start () {
        fightState = State.Defend;
        StartCoroutine(ChangeState(State.AttackForward));
        attackNumber = 0;
    }
	
	void Update () {
        switch (fightState) {
            case State.AttackForward:
                hitbox.SetActive(false);
                transform.position = Vector3.MoveTowards(transform.position, leftBoundary, moveSpeed * Time.deltaTime);
                leftarm.GetComponent<BearArm>().MoveArm();
                rightarm.GetComponent<BearArm>().MoveArm();
                if (transform.position.x <= leftBoundary.x) {
                    rightarm.GetComponent<BearArm>().ChangeDirection();
                    leftarm.GetComponent<BearArm>().ChangeDirection();
                    fightState = State.AttackBackward;
                }
                break;
            case State.AttackBackward:
                hitbox.SetActive(false);
                transform.position = Vector3.MoveTowards(transform.position, rightBoundary, moveSpeed * Time.deltaTime);
                leftarm.GetComponent<BearArm>().MoveArm();
                rightarm.GetComponent<BearArm>().MoveArm();
                if (transform.position.x >= rightBoundary.x) {
                    rightarm.GetComponent<BearArm>().ChangeDirection();
                    leftarm.GetComponent<BearArm>().ChangeDirection();
                    attackNumber++;
                    if (attackNumber > 3) {
                        fightState = State.Defend;
                        attackNumber = 0;
                        StartCoroutine(ChangeState(State.AttackForward));
                    }
                    else {
                        fightState = State.AttackForward;
                    }
                   
                }
                break;
            case State.SpawnEnemies:
                break;
            case State.Defend:
               hitbox.SetActive(true);
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
        yield return new WaitForSeconds(5);
        fightState = st;     
    }
}
