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
    private bool isHit = false;


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
        if (transform.position.x <= MainCamera.GetComponent<MainCamera>().offset - 11f) {
            Destroy(gameObject);
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
            isHit = true;
            StartCoroutine(StopHitAnimation());
            int soundNumber = Random.Range(0, 5);
            switch (soundNumber) {
                case 0:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("SpiderHit1");
                    break;
                case 1:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("SpiderHit2");
                    break;
                case 2:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("SpiderHit3");
                    break;
                case 3:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("SpiderHit4");
                    break;
                default:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("SpiderHit5");
                    break;
            }
        }
        else if (otherCollider.tag == "PlayerMissile" && isAlive) {
            healthPoints--;
        }
    }

    public void Die() {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        this.GetComponent<Rigidbody2D>().gravityScale = 2.0f;
        isAlive = false;
        int soundNumber = Random.Range(0, 4);
        switch (soundNumber) {
            case 0:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("SpiderDie1");
                break;
            case 1:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("SpiderDie2");
                break;
            case 2:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("SpiderDie3");
                break;
            default:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("SpiderDie4");
                break;
        }
    }

    void Move() {
        if (moveDirection == Direction.Up) {
            if (transform.position.y > startPosition.y + stringExtremes) {
                StartCoroutine(ChangeDirection(Direction.Down));
                if (transform.position.x - player.transform.position.x < shootRange) {
                    Shoot();
                }
                if (isHit) {
                    GetComponent<Animator>().Play("spider_hit");
                }
                else {
                    GetComponent<Animator>().Play("spider_shooting");
                }
            }
            else {
                if (isHit) {
                    GetComponent<Animator>().Play("spider_hit");
                }
                else {
                    GetComponent<Animator>().Play("spider_default");
                }
                transform.position = new Vector3(transform.position.x, transform.position.y + stringSpeed * Time.deltaTime, transform.position.z);
            }
        }
        else if (moveDirection == Direction.Down) {
            if (transform.position.y < startPosition.y - stringExtremes) {
                StartCoroutine(ChangeDirection(Direction.Up));
                if (transform.position.x - player.transform.position.x < shootRange) {
                    Shoot();
                }
                if (isHit) {
                    GetComponent<Animator>().Play("spider_hit");
                }
                else {
                    GetComponent<Animator>().Play("spider_shooting");
                }
            }
            else {
                if (isHit) {
                    GetComponent<Animator>().Play("spider_hit");
                }
                else {
                    GetComponent<Animator>().Play("spider_default");
                }
                transform.position = new Vector3(transform.position.x, transform.position.y - stringSpeed * Time.deltaTime, transform.position.z);
            }
        }
    }

    IEnumerator ChangeDirection(Direction dir) {
        yield return new WaitForSeconds(0.7f);
        moveDirection = dir;
    }

    IEnumerator StopHitAnimation() {
        yield return new WaitForSeconds(0.1f);
        isHit = false;
    }

    void Shoot() {
        if (Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Instantiate(spiderBullet, new Vector3(transform.position.x, transform.position.y - bulletOffset, transform.position.z), Quaternion.identity);
        }
    }
}
