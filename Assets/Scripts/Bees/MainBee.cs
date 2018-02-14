using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBee : MonoBehaviour {

    public Transform PlayerBullet;
    public Transform PlayerMissile;
    public float bulletOffset;
    public float speed;
    public GameObject middleSlot;
    public float fireRate;

    [ShowOnly] public bool isInEndSequence;
    [ShowOnly] public bool hasPowerup;
    [HideInInspector] public bool isAlive;


    private float nextFire;
    private GameObject MainCamera;
    private Vector2 newPosition;

    [ShowOnly] [SerializeField] private float leftBoundary;
    [ShowOnly] [SerializeField] private float rightBoundary;
    [ShowOnly] [SerializeField] private float topBoundary;
    [ShowOnly] [SerializeField] private float bottomBoundary;
    

    void Start () {
        MainCamera = GameObject.Find("Main Camera");
        leftBoundary = -8.5f;
        rightBoundary = 8.5f;
        topBoundary = 4.75f;
        bottomBoundary = -4.75f;
        isAlive = true;
        isInEndSequence = false;
    }
	
	void Update () {
        if (isInEndSequence) {
            MoveOutOfScreen();
        }
        else {
            if (isAlive) {
                Move();
                if (Input.GetMouseButtonDown(0)) {
                    Shoot();
                }
                if (Input.GetMouseButtonDown(1)) {
                    speed *= 2;
                }
                else if (Input.GetMouseButtonUp(1)) {
                    speed /= 2;
                }
            }
            if (transform.position.y <= -6) {
                NewMainBee();
            }
        }
        SetAnimation();
    }

    void Move() {
        leftBoundary = MainCamera.GetComponent<MainCamera>().offset - 8.5f;
        rightBoundary = MainCamera.GetComponent<MainCamera>().offset + 8.5f;
        newPosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        if (newPosition.x >= leftBoundary && newPosition.x <= rightBoundary && newPosition.y <= topBoundary && newPosition.y >= bottomBoundary) {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        }
        else {
            this.transform.position = new Vector3(this.transform.position.x + MainCamera.GetComponent<MainCamera>().speed * Time.deltaTime / 100, this.transform.position.y, this.transform.position.z);
        }
    }

    void Shoot() {
        if (hasPowerup) {
            if (Time.time > nextFire) {
                nextFire = Time.time + fireRate*2;
                Instantiate(PlayerMissile, new Vector3(transform.position.x + bulletOffset * 3, transform.position.y, transform.position.z), Quaternion.identity);
            }
           
        }
        else {
            if (Time.time > nextFire) {
                nextFire = Time.time + fireRate;
                Instantiate(PlayerBullet, new Vector3(transform.position.x + bulletOffset, transform.position.y, transform.position.z), Quaternion.identity);
            }
        }
        
    }

    void CallSwarm() {

    }

    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.tag == "Fly" && otherCollider.transform.GetComponent<Fly>().isAlive && this.isAlive) {
            Die();
        }
        else if (otherCollider.tag == "Wasp" && otherCollider.transform.GetComponent<Wasp>().isAlive && this.isAlive) {
            Die();
        }
        else if (otherCollider.tag == "Dragonfly" && otherCollider.transform.GetComponent<DragonFly>().isAlive && this.isAlive) {
            Die();
        }
        else if (otherCollider.tag == "Bear" && this.isAlive) {
            Die();
        }
        else if (otherCollider.tag == "EnemyBullet" && this.isAlive) {
            Destroy(otherCollider.transform.root.gameObject);
            Die();
        }
    }

    void Die() {
        GameManager.beeCount -= 1;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        this.GetComponent<Rigidbody2D>().gravityScale = 2.0f;
        isAlive = false;
    }

    void NewMainBee() {
        middleSlot.GetComponent<MiddleSlot>().DestroyBee();
        transform.position = middleSlot.GetComponent<MiddleSlot>().transform.position;
        middleSlot.GetComponent<Slot>().isOccupied = false;
        isAlive = true;
        this.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        
    }

    void MoveOutOfScreen() {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
    }

    void SetAnimation() {
        if (!isAlive) {
            GetComponent<Animator>().Play("mainBee_death");
        }
        else if (hasPowerup) {
            transform.localScale = new Vector3(0.4f, 0.4f, 1);
            GetComponent<Animator>().Play("mainBee_bodybuilder");
        }
        else {
            transform.localScale = new Vector3(0.228f, 0.228f, 1);
            GetComponent<Animator>().Play("mainBee_flying");
        }
    }

    public void SetPowerupTimer(float time) {
        StartCoroutine(ResetPowerup(time));
    }

    IEnumerator ResetPowerup(float seconds) {
        yield return new WaitForSeconds(seconds);
        hasPowerup = false;
    }
}
