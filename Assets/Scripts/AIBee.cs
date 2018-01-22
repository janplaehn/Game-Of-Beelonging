using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBee : MonoBehaviour {

    public float moveSpeed;
    public float curveExtremes;
    public float curveSpeed;
    public float shootRange;
    public float fireRate;
    public float bulletOffset;
    public Transform playerBullet;
    public Transform currentSlot;

    private enum Direction { Up, Down };
    private Direction moveDirection;
    private enum State { Swarm, MoveToSlot, MoveToPlayer, Die }
    private State beeState;
    private Vector2 startPosition;
    private GameObject player;
    private float nextFire;
    private Collider2D[] collidersWithinRadius;
    private GameObject MainCamera;

    void Start () {
        beeState = State.Swarm;
        GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        moveDirection = Direction.Up;
        startPosition = transform.position;
        player = GameObject.Find("MainBee");
        MainCamera = GameObject.Find("Main Camera");

    }
	
	void Update () {
        switch (beeState) {

            case State.Swarm:
                Move();
                Shoot();
                if (Input.GetMouseButtonDown(1)) {
                    fireRate *= 2;
                    beeState = State.MoveToPlayer;
                }
                else if (!NextSlotOccupied()) {
                    beeState = State.MoveToSlot;
                }
                break;

            case State.MoveToSlot:
                MoveToSlot();
                Shoot();
                if (Input.GetMouseButtonDown(1)) {
                    fireRate /= 2;
                    beeState = State.MoveToPlayer;
                }
                if (transform.position == currentSlot.transform.position)
                {
                    beeState = State.Swarm;
                }
                break;

            case State.MoveToPlayer:
                MoveToPlayerSlot();
                Shoot();
                if (Input.GetMouseButtonUp(1)) {
                    beeState = State.MoveToSlot;
                }
                break;

            case State.Die:
                if (transform.position.y <= -6) {
                    Destroy(gameObject);
                }
                break;

            default:
                beeState = State.Swarm;
                break;
        }
    }

    void OnTriggerStay2D(Collider2D otherCollider) {
        if (otherCollider.tag == "Fly" && otherCollider.transform.GetComponent<Fly>().isAlive && beeState != State.Die) {
            otherCollider.transform.gameObject.GetComponent<Fly>().Die();
            Die();
        }
        else if (otherCollider.tag == "Wasp" && otherCollider.transform.GetComponent<Wasp>().isAlive && beeState != State.Die)
        {
            otherCollider.transform.gameObject.GetComponent<Wasp>().Die();
            Die();
        }
        else if (otherCollider.tag == "Dragonfly" && otherCollider.transform.GetComponent<DragonFly>().isAlive && beeState != State.Die)
        {
            otherCollider.transform.gameObject.GetComponent<DragonFly>().Die();
            Die();
        }
        else if (otherCollider.tag == "EnemyBullet" && beeState != State.Die) {
            Destroy(otherCollider.transform.gameObject);
            Die();
        }
    }

    void Die() {
        currentSlot.GetComponent<Slot>().isOccupied = false;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        GetComponent<Rigidbody2D>().gravityScale = 2.0f;
        beeState = State.Die;
    }

    void Shoot() {
        if (Time.time > nextFire && EnemiesInRange()) {
            nextFire = Time.time + fireRate;
            Instantiate(playerBullet, new Vector3(transform.position.x - bulletOffset, transform.position.y, transform.position.z), Quaternion.identity);
        }
    }

    void Move() {
        startPosition = currentSlot.position;
        if (moveDirection == Direction.Up) {
            transform.position = new Vector3(transform.position.x, transform.position.y + curveSpeed, transform.position.z);
            if (transform.position.y > startPosition.y + curveExtremes) {
                moveDirection = Direction.Down;
            }
        }
        else if (moveDirection == Direction.Down) {
            transform.position = new Vector3(transform.position.x, transform.position.y - curveSpeed, transform.position.z);
            if (transform.position.y < startPosition.y - curveExtremes) {
                moveDirection = Direction.Up;
            }
        }
        transform.position = new Vector3(transform.position.x + MainCamera.GetComponent<MainCamera>().speed / 100, transform.position.y, transform.position.z);
    }

    bool EnemiesInRange() {
        collidersWithinRadius = Physics2D.OverlapCircleAll(new Vector3(transform.position.x + 3, transform.position.y, transform.position.z), shootRange);
        foreach (Collider2D collider in collidersWithinRadius) {
            if (collider.tag == "Fly" && collider.transform.GetComponent<Fly>().isAlive) {
                return true;
            }
            else if (collider.tag == "Wasp" && collider.transform.GetComponent<Wasp>().isAlive) {
                return true;
            }
        }
        return false;
    }

    bool NextSlotOccupied() {
        if (currentSlot.GetComponent<Slot>().nextSlot.GetComponent<Slot>().isOccupied) {
            return true;
        }
        else {
            currentSlot.GetComponent<Slot>().isOccupied = false;
            currentSlot = currentSlot.GetComponent<Slot>().nextSlot.transform;
            currentSlot.GetComponent<Slot>().isOccupied = true;
            return false;
        }
    }

    void MoveToSlot() {
        transform.position = Vector3.MoveTowards(transform.position, currentSlot.position, moveSpeed);
    }

    void MoveToPlayerSlot()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentSlot.GetComponent<Slot>().playerSlot.transform.position, moveSpeed*4);
    }
}
