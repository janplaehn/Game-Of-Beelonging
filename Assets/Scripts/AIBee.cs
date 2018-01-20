using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBee : MonoBehaviour {

    enum Direction { Up, Down };
    Direction moveDirection;
    private Vector2 startPosition;
    public float curveExtremes;
    public float curveSpeed;
    public float shootRange;
    public bool isAlive;
    private GameObject player;
    public float fireRate;
    private float nextFire;
    public Transform playerBullet;
    public float bulletOffset;
    private GameObject MainCamera;
    public Collider2D[] collidersWithinRadius;
    public Transform currentSlot;
    bool nextSlotOccupied;
    public float moveSpeed;
    private float slotCoolDown;

    void Start () {
        GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        moveDirection = Direction.Up;
        startPosition = transform.position;
        isAlive = true;
        player = GameObject.Find("MainBee");
        MainCamera = GameObject.Find("Main Camera");
        nextSlotOccupied = true;

    }
	
	void Update () {
        if (transform.position.y <= -6)
        {
            Destroy(gameObject);
        }
        if (isAlive) {
            if (nextSlotOccupied) {
                Move();
                Shoot();
            }
            else {
                MoveToNextSlot();
            }
        }
    }

    void OnTriggerStay2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Fly" && otherCollider.transform.GetComponent<Fly>().isAlive && isAlive)
        {
            otherCollider.transform.gameObject.GetComponent<Fly>().Die();
            Die();
        }
        else if (otherCollider.tag == "Wasp" && otherCollider.transform.GetComponent<Wasp>().isAlive && isAlive)
        {
            otherCollider.transform.gameObject.GetComponent<Wasp>().Die();
            Die();
        }
        else if (otherCollider.tag == "EnemyBullet" && isAlive) {
            Destroy(otherCollider.transform.gameObject);
            Die();
        }
        else if (otherCollider.tag == "Slot" && otherCollider.transform.GetComponent<Slot>().nextSlot.GetComponent<Slot>().isOccupied) {
            nextSlotOccupied = true;
            currentSlot = otherCollider.transform;
            Debug.Log("Next slot occupied");
        }
        else if (otherCollider.tag == "Slot" && !(otherCollider.transform.GetComponent<Slot>().nextSlot.GetComponent<Slot>().isOccupied)) {
            if (Time.time > slotCoolDown) { 
                nextSlotOccupied = false;
                currentSlot = otherCollider.transform.GetComponent<Slot>().nextSlot.transform;
                slotCoolDown = Time.time + 2;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.tag == "Slot" && !(otherCollider.transform.GetComponent<Slot>().nextSlot.GetComponent<Slot>().isOccupied)){
            slotCoolDown = Time.time + 2;
        }
    }

    void Die()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        GetComponent<Rigidbody2D>().gravityScale = 2.0f;
        isAlive = false;
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

    void MoveToNextSlot() {
        transform.position = Vector3.MoveTowards(transform.position, currentSlot.position, moveSpeed);
    }
}
