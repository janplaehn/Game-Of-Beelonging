using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawMovement : MonoBehaviour {

    public float moveSpeed;

    public Transform AttackPosition1;
    public Transform AttackPosition2;
    public Transform AttackPosition3;

    public Transform targetPoint;


    public GameObject openMouthCollider;
    public GameObject defaultCollider;
    public GameObject hitbox;
    public GameObject bear;

    private Vector3 targetPosition;
    private Vector3 startPosition;
    private Vector3 movementVector;

    [HideInInspector] public enum State {Indicate, TeleportToAttack, AttackSlow, GoBackFromAttack, AttackFast, TeleportBack, Default};
    [HideInInspector] public State battleState;

    void Start() {
        startPosition = transform.position + Vector3.left * 10;
        targetPosition = transform.position + Vector3.down * 5 + Vector3.left * 10;
        battleState = State.Default;

    }
	
	void Update () {
        switch (battleState) {
            case State.Indicate:
                IndicateAttack();
                bear.GetComponent<BearHead>().mouthOpen = true;
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
        defaultCollider.SetActive(false);
        openMouthCollider.SetActive(true);
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if (transform.position.y < -20) {
            battleState = State.TeleportToAttack;
        }

    }

    void TeleportToAttack() {
        hitbox.SetActive(false);
        defaultCollider.SetActive(false);
        openMouthCollider.SetActive(true);
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
        StartCoroutine(ChangeState(State.AttackFast, 1.7f));
    }

    void TeleportBack() {
        hitbox.SetActive(false);
        defaultCollider.SetActive(false);
        ResetRotation();
        transform.position = targetPosition;
        battleState = State.Default;       
        hitbox.SetActive(true);
        defaultCollider.SetActive(true);
        openMouthCollider.SetActive(false);
    }

    void AttackSlow() {
        hitbox.SetActive(false);
        defaultCollider.SetActive(false);
        openMouthCollider.SetActive(true);
        transform.position += movementVector * moveSpeed * Time.deltaTime;
    }

    void AttackFast() {
        hitbox.SetActive(false);
        defaultCollider.SetActive(false);
        openMouthCollider.SetActive(true);
        transform.position += movementVector * moveSpeed * Time.deltaTime * 8;
        if (transform.position.y > -2 && Mathf.Abs(transform.rotation.x) < 0.5f) {
            battleState = State.GoBackFromAttack;
        }
        else if (transform.position.y < 2 && Mathf.Abs(transform.rotation.x) > 0.5f) {
            battleState = State.GoBackFromAttack;
        }

    }

    void GoBackFromAttack() {
        hitbox.SetActive(false);
        defaultCollider.SetActive(false);
        openMouthCollider.SetActive(true);
        transform.position += (-movementVector) * moveSpeed * Time.deltaTime*2;
        if (Mathf.Abs(transform.position.y) > 20) {
            battleState = State.TeleportBack;
            hitbox.SetActive(true);
            defaultCollider.SetActive(true);
            openMouthCollider.SetActive(false);
            bear.GetComponent<BearHead>().mouthOpen = false;
        }
    }

    void Default() {
        if (transform.position.x < 6) {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
        }
        

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
            battleState = State.GoBackFromAttack;
        }
        else if (collision.tag == "PlayerBullet" && collision.GetComponent<PlayerBullet>().isAlive) {
            collision.GetComponent<PlayerBullet>().Die();
        }
    }

    IEnumerator ChangeState(State state, float seconds) {
        yield return new WaitForSeconds(seconds);
        if (battleState == State.AttackSlow) battleState = state;
        int soundNumber = Random.Range(0, 5);
        switch (soundNumber) {
            case 0:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BearAttack1");
                break;
            case 1:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BearAttack2");
                break;
            case 2:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BearAttack3");
                break;
            case 3:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BearAttack4");
                break;
            default:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BearAttack5");
                break;
        }
    }
}
