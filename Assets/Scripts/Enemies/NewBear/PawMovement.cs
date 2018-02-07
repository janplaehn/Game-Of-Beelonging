using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawMovement : MonoBehaviour {

    public float moveSpeed;

    public Transform AttackPosition1;
    public Transform AttackPosition2;
    public Transform AttackPosition3;

    public Transform targetPoint;

    public GameObject hitbox;

    private Vector3 targetPosition;
    private Vector3 startPosition;
    private Vector3 movementVector;

    [HideInInspector] public enum State {Indicate, TeleportToAttack, AttackSlow, GoBackFromAttack, AttackFast, TeleportBack, Default};
    [HideInInspector] public State battleState;

    void Start() {
        startPosition = transform.position;
        targetPosition = transform.position + Vector3.down * 5;
        battleState = State.Default;

    }
	
	void Update () {
        switch (battleState) {
            case State.Indicate:
                IndicateAttack();
                break;
            case State.TeleportToAttack:
                TeleportToAttack();
                battleState = State.AttackSlow;
                break;
            case State.AttackSlow:
                AttackSlow();
                break;
            case State.AttackFast:
                AttackFast();
                break;
            case State.GoBackFromAttack:
                GoBackFromAttack();
                break;
            case State.TeleportBack:
                TeleportBack();
                break;
            case State.Default:
                Default();
                break;
            default:
                Default();
                break;
        }
    }

    void IndicateAttack() {
        hitbox.SetActive(false);
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if (transform.position.y < -20) {
            battleState = State.TeleportToAttack;
        }

    }

    void TeleportToAttack() {
        hitbox.SetActive(false);
        Debug.Log("Teleporting to Attack");
        int tempNum = Random.Range(0, 3);
        switch (tempNum) {
            case 0:
                transform.position = AttackPosition1.position;
                break;
            case 1:
                transform.position = AttackPosition2.position;
                break;
            case 2:
                transform.position = AttackPosition3.position;
                break;
            default:
                transform.position = AttackPosition1.position;
                break;        
        }
        LookAtTarget();
        battleState = State.AttackSlow;
        StartCoroutine(ChangeState(State.AttackFast, 1.5f));
    }

    void TeleportBack() {
        hitbox.SetActive(false);
        Debug.Log("Going Back Back");
        ResetRotation();
        transform.position = targetPosition;
        battleState = State.Default;
        hitbox.SetActive(true);
    }

    void AttackSlow() {
        hitbox.SetActive(false);
        transform.position += movementVector * moveSpeed * Time.deltaTime;
    }

    void AttackFast() {
        hitbox.SetActive(false);
        transform.position += movementVector * moveSpeed * Time.deltaTime * 6;
        if (transform.position.y > -2 && Mathf.Abs(transform.rotation.x) < 0.5f) {
            battleState = State.GoBackFromAttack;
        }
        else if (transform.position.y < 2 && Mathf.Abs(transform.rotation.x) > 0.5f) {
            battleState = State.GoBackFromAttack;
        }

    }

    void GoBackFromAttack() {
        hitbox.SetActive(false);
        transform.position += (-movementVector) * moveSpeed * Time.deltaTime*2;
        if (Mathf.Abs(transform.position.y) > 20) {
            battleState = State.TeleportBack;
        }
    }

    void Default() {
        transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime * 6);
        

    }

    void LookAtTarget() {
        transform.right = targetPoint.position - transform.position;
        transform.Rotate(transform.rotation.x, transform.rotation.y, transform.rotation.z - 90);
        if (transform.position.y > 5) {
            transform.Rotate(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z);
        }
        SetMovementVector();
    }

    void ResetRotation() {
        transform.rotation = Quaternion.identity;
    }

    void SetMovementVector() {
        movementVector = targetPoint.position - transform.position;
        movementVector /= movementVector.magnitude;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "AIBee" || collision.tag == "MainBee") {
            Debug.Log("Hurt");
            battleState = State.GoBackFromAttack;
        }
    }

    IEnumerator ChangeState(State state, float seconds) {
        yield return new WaitForSeconds(seconds);
        if (battleState == State.AttackSlow) battleState = state;
    }
}
