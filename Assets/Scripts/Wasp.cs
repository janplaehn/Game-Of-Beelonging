﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasp : MonoBehaviour
{

    enum Direction { Up, Down };
    Direction moveDirection;
    private Vector2 startPosition;
    public float curveExtremes;
    public float curveSpeed;
    public float moveSpeed;
    public float detectionRange;
    public float shootRange;
    public float playerDistance;
    public int healthPoints;
    public bool isAlive;
    private GameObject player;
    public float fireRate;
    private float nextFire;
    public Transform enemyBullet;
    public float bulletOffset;

    void Start()
    {
        this.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        moveDirection = Direction.Up;
        startPosition = transform.position;
        isAlive = true;
        healthPoints = 4;
        player = GameObject.Find("MainBee");
    }

    void Update()
    {
        if (transform.position.y <= -6)
        {
            Destroy(this.gameObject);
        }
        if (isAlive)
        {
            Move();
            if (transform.position.x - player.transform.position.x < detectionRange) {
                ChasePlayer();
            }
            if (transform.position.x - player.transform.position.x < shootRange) {
                Shoot();
            }
            if (healthPoints <= 0)
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

    void Die()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        this.GetComponent<Rigidbody2D>().gravityScale = 2.0f;
        isAlive = false;
    }

    void Move()
    {
        if (moveDirection == Direction.Up)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + curveSpeed, transform.position.z);
            if (transform.position.y > startPosition.y + curveExtremes)
            {
                moveDirection = Direction.Down;
            }
        }
        else if (moveDirection == Direction.Down)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - curveSpeed, transform.position.z);
            if (transform.position.y < startPosition.y - curveExtremes)
            {
                moveDirection = Direction.Up;
            }
        }
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