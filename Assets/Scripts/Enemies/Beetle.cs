using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : MonoBehaviour {

    public float curveExtremes;
    public float curveSpeed;
    public float moveSpeed;
    public int healthPoints;

    [HideInInspector] public bool isAlive;

    private enum Direction { Up, Down };
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
            if (isHit) {
                GetComponent<Animator>().Play("beetle_hit");
                StartCoroutine(StopHitAnimation());
            }
        }
        else {
            GetComponent<Animator>().Play("beetle_death");
        }
    }

    void OnTriggerStay2D(Collider2D otherCollider) {
        if (otherCollider.tag == "PlayerBullet" && otherCollider.GetComponent<PlayerBullet>().isAlive) {
            otherCollider.GetComponent<PlayerBullet>().Die();
            healthPoints--;
            isHit = true;
            int soundNumber = Random.Range(0, 5);
            switch (soundNumber) {
                case 0:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BeetleHit1");
                    break;
                case 1:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BeetleHit2");
                    break;
                case 2:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BeetleHit3");
                    break;
                case 3:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BeetleHit4");
                    break;
                case 4:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BeetleHit5");
                    break;
                default:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BeetleHit4");
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
        int soundNumber = Random.Range(0, 5);
        switch (soundNumber) {
            case 0:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BeetleDie1");
                break;
            case 1:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BeetleDie2");
                break;
            case 2:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BeetleDie3");
                break;
            case 3:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BeetleDie4");
                break;
            case 4:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BeetleDie5");
                break;
            default:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BeetleDie4");
                break;
        }
    }

    void Move() {
        if (moveDirection == Direction.Up) {
            transform.position = new Vector3(transform.position.x, transform.position.y + curveSpeed * Time.deltaTime, transform.position.z);
            if (transform.position.y > startPosition.y + curveExtremes) {
                moveDirection = Direction.Down;
            }
        }
        else if (moveDirection == Direction.Down) {
            transform.position = new Vector3(transform.position.x, transform.position.y - curveSpeed * Time.deltaTime, transform.position.z);
            if (transform.position.y < startPosition.y - curveExtremes) {
                moveDirection = Direction.Up;
            }
        }
        if (transform.position.x <= MainCamera.GetComponent<MainCamera>().offset + 10f) {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
    }

    IEnumerator StopHitAnimation() {
        yield return new WaitForSeconds(0.1f);
        isHit = false;
    }
}
