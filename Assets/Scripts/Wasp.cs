using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasp : MonoBehaviour
{
    public int healthPoints;
    public float moveSpeed;
    public float detectionRange;
    public float shootRange;
    public float playerDistance;
    public float fireRate;
    public Transform enemyBullet;
    public float bulletOffset;

    [HideInInspector] public bool isAlive;

    private enum Direction { Up, Down };
    private Direction moveDirection;
    private GameObject player;
    private float nextFire;
    private GameObject MainCamera;

    void Start() {
        this.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        moveDirection = Direction.Up;
        isAlive = true;
        healthPoints = 3;
        player = GameObject.Find("MainBee");
        MainCamera = GameObject.Find("Main Camera");
    }

    void Update() {
        if (transform.position.y <= -6) {
            Destroy(this.gameObject);
        }
        if (isAlive) {
            if (transform.position.x - player.transform.position.x < detectionRange) {
                ChasePlayer();
            }
            if (transform.position.x - player.transform.position.x < shootRange) {
                Shoot();
            }
            if (healthPoints <= 0 || transform.position.x <= MainCamera.GetComponent<MainCamera>().offset - 10f)
            {
                Die();
            }
        }
    }

    void OnTriggerStay2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "PlayerBullet" && isAlive)
        {
            Destroy(otherCollider.transform.root.gameObject);
            healthPoints--;
        }
    }

    public void Die()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        this.GetComponent<Rigidbody2D>().gravityScale = 2.0f;
        isAlive = false;
    }

    void ChasePlayer() {
        if (player.GetComponent<MainBee>().isAlive == true) {
            Vector3 targetPosition = new Vector3(player.transform.position.x + playerDistance, player.transform.position.y, player.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed);
        }
    }

    void Shoot() {
        if (Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Instantiate(enemyBullet, new Vector3(transform.position.x - bulletOffset, transform.position.y, transform.position.z), Quaternion.identity);
        }
    }
}