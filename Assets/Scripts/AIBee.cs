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

    void Start () {
        GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        moveDirection = Direction.Up;
        startPosition = transform.position;
        isAlive = true;
        player = GameObject.Find("MainBee");
        MainCamera = GameObject.Find("Main Camera");

    }
	
	void Update () {
        if (transform.position.y <= -6)
        {
            Destroy(gameObject);
        }
        if (isAlive) {
            Move();
            Shoot();
        }
    }

    void OnTriggerStay2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Fly" && otherCollider.transform.GetComponent<Fly>().isAlive && isAlive)
        {
            Die();
        }
        else if (otherCollider.tag == "Wasp" && otherCollider.transform.GetComponent<Wasp>().isAlive && isAlive)
        {
            Die();
        }
        else if (otherCollider.tag == "EnemyBullet" && isAlive) {
            Destroy(otherCollider.transform.root.gameObject);
            Die();
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
            if (collider.tag == "Fly") {
                return true;
            }
            else if (collider.tag == "Wasp") {
                return true;
            }
        }
        return false;
    }
}
