using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour {

    public float stringExtremes;
    public float stringSpeed;
    public int healthPoints;
    public float fireRate;
    public float shootRange;
    public Transform spiderBullet;
    public float bulletOffset;

    [HideInInspector] public bool isAlive;

    private enum Direction { Up, Down };
    private GameObject player;
    private float nextFire;
    private Direction moveDirection;
    private Vector2 startPosition;
    private GameObject MainCamera;


    void Start() {
        this.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        moveDirection = Direction.Up;
        startPosition = transform.position;
        isAlive = true;
        MainCamera = GameObject.Find("Main Camera");
        player = GameObject.Find("MainBee");
    }

    void Update() {
        if (transform.position.y <= -6) {
            Destroy(this.gameObject);
        }
        if (isAlive) {
            Move();
            if (healthPoints <= 0 || transform.position.x <= MainCamera.GetComponent<MainCamera>().offset - 10f) {
                Die();
            }
        }
        else {
            GetComponent<Animator>().Play("spider_death");
        }
    }

    void OnTriggerStay2D(Collider2D otherCollider) {
        if (otherCollider.tag == "PlayerBullet" && isAlive) {
            Destroy(otherCollider.transform.root.gameObject);
            healthPoints--;
        }
        else if (otherCollider.tag == "PlayerMissile" && isAlive) {
            healthPoints--;
        }
    }

    public void Die() {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        this.GetComponent<Rigidbody2D>().gravityScale = 2.0f;
        isAlive = false;
    }

    void Move() {
        if (moveDirection == Direction.Up) {
            if (transform.position.y > startPosition.y + stringExtremes) {
                StartCoroutine(ChangeDirection(Direction.Down));
                if (transform.position.x - player.transform.position.x < shootRange) {
                    Shoot();
                }
                GetComponent<Animator>().Play("spider_shooting");
            }
            else {
                GetComponent<Animator>().Play("spider_default");
                transform.position = new Vector3(transform.position.x, transform.position.y + stringSpeed * Time.deltaTime, transform.position.z);
            }
        }
        else if (moveDirection == Direction.Down) {
            if (transform.position.y < startPosition.y - stringExtremes) {
                StartCoroutine(ChangeDirection(Direction.Up));
                if (transform.position.x - player.transform.position.x < shootRange) {
                    Shoot();
                }
                GetComponent<Animator>().Play("spider_shooting");
            }
            else {
                GetComponent<Animator>().Play("spider_default");
                transform.position = new Vector3(transform.position.x, transform.position.y - stringSpeed * Time.deltaTime, transform.position.z);
            }
        }
    }

    IEnumerator ChangeDirection(Direction dir) {
        yield return new WaitForSeconds(0.7f);
        moveDirection = dir;
    }

    void Shoot() {
        if (Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Instantiate(spiderBullet, new Vector3(transform.position.x, transform.position.y - bulletOffset, transform.position.z), Quaternion.identity);
        }
    }
}
