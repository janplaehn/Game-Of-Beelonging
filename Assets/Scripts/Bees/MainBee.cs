using System.Collections;
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
    private bool isInvincible;
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
        topBoundary = 6f;
        bottomBoundary = -6f;
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
        leftBoundary = MainCamera.GetComponent<MainCamera>().offset - 15f;
        rightBoundary = MainCamera.GetComponent<MainCamera>().offset + 15f;
        newPosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        this.transform.position = new Vector3(this.transform.position.x + MainCamera.GetComponent<MainCamera>().speed * Time.deltaTime / 100, this.transform.position.y, this.transform.position.z);
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
        if (!isInvincible && this.isAlive) {
            if (otherCollider.tag == "Fly" && otherCollider.transform.GetComponent<Fly>().isAlive) {
                Die();
            }
            else if (otherCollider.tag == "Wasp" && otherCollider.transform.GetComponent<Wasp>().isAlive) {
                Die();
            }
            else if (otherCollider.tag == "Dragonfly" && otherCollider.transform.GetComponent<DragonFly>().isAlive) {
                Die();
            }
            else if (otherCollider.tag == "Bear") {
                Die();
            }
            else if (otherCollider.tag == "Thistle") {
                Die();
            }
            else if (otherCollider.tag == "Beetle") {
                Die();
            }
            else if (otherCollider.tag == "Spider") {
                Die();
            }
            else if (otherCollider.tag == "EnemyBullet") {
                Destroy(otherCollider.transform.root.gameObject);
                Die();
            }
        }
    }

    void Die() {
        hasPowerup = false;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        this.GetComponent<Rigidbody2D>().gravityScale = 2.0f;
        isAlive = false;
        StartCoroutine(MakeInvincible());
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
            if (isInvincible) {
                transform.localScale = new Vector3(0.228f, 0.228f, 1);
                GetComponent<Animator>().Play("mainBee_invincible");
            }
            else {
                transform.localScale = new Vector3(0.228f, 0.228f, 1);
                GetComponent<Animator>().Play("mainBee_flying");
            }
            
        }
    }

    public void SetPowerupTimer(float time) {
        StartCoroutine(ResetPowerup(time));
    }

    IEnumerator MakeInvincible() {
        isInvincible = true;
        yield return new WaitForSeconds(2);
        isInvincible = false;
    }

    IEnumerator ResetPowerup(float seconds) {
        yield return new WaitForSeconds(seconds);
        hasPowerup = false;
    }
}
