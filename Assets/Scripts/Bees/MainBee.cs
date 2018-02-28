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
    private GameObject cursor;
    

    void Start () {
        MainCamera = GameObject.Find("Main Camera");
        isAlive = true;
        isInEndSequence = false;
        cursor = GameObject.Find("Cursor");
    }
	
	void Update () {
        if (isInEndSequence) {
            MoveOutOfScreen();
        }
        else {
            if (isAlive) {
                Move();
                if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1)) {
                    Shoot();
                }
            }
            if (transform.position.y <= -6) {
                NewMainBee();
            }
        }
        SetAnimation();
    }

    void Move() {
        transform.position = Vector3.MoveTowards(transform.position, cursor.transform.position, speed * Time.deltaTime);
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
            else if (otherCollider.tag == "Beetle" && otherCollider.transform.GetComponent<Beetle>().isAlive) {
                Die();
            }
            else if (otherCollider.tag == "Spider" && otherCollider.transform.GetComponent<Spider>().isAlive) {
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
        if (GameManager.beeCount == 1) {
            Destroy(gameObject);
            return;
        }
        middleSlot.GetComponent<MiddleSlot>().DestroyBee();
        transform.position = middleSlot.GetComponent<MiddleSlot>().transform.position;
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
        foreach (GameObject bee in GameObject.FindGameObjectsWithTag("AIBee")) {
            bee.GetComponent<AIBee>().isInvincible = true;
        }
        yield return new WaitForSeconds(2);
        isInvincible = false;
        foreach (GameObject bee in GameObject.FindGameObjectsWithTag("AIBee")) {
            bee.GetComponent<AIBee>().isInvincible = false;
        }
    }

    IEnumerator ResetPowerup(float seconds) {
        yield return new WaitForSeconds(seconds);
        hasPowerup = false;
    }
}
