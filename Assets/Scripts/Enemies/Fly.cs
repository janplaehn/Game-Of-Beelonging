using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour {

    public float curveExtremes;
    public float curveSpeed;
    public int healthPoints;

    [HideInInspector] public bool isAlive;

    private enum Direction { Up, Down};
    private Direction moveDirection;
    private Vector2 startPosition;
    private GameObject MainCamera;
 

    void Start () {
        this.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        moveDirection = Direction.Up;
        startPosition = transform.position;
        isAlive = true;
        healthPoints = 1;
        MainCamera = GameObject.Find("Main Camera");
    }
	
	void Update () {
		if (transform.position.y <= -6) {
            Destroy(this.gameObject);
        }
        if (isAlive) {
            Move();
            if (healthPoints <= 0 || transform.position.x <= MainCamera.GetComponent<MainCamera>().offset - 10f)
            {
                Die();
            }
        }
        else {
            GetComponent<Animator>().Play("fly_death");
        }
    }

    void OnTriggerStay2D(Collider2D otherCollider) {
        if (otherCollider.tag == "PlayerBullet" && isAlive && otherCollider.GetComponent<PlayerBullet>().isAlive) {
            otherCollider.GetComponent<PlayerBullet>().Die();
            healthPoints--;
            int soundNumber = Random.Range(0, 5);
            switch (soundNumber) {
                case 0:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("FlyDie1");
                    break;
                case 1:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("FlyDie2");
                    break;
                case 2:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("FlyDie3");
                    break;
                case 3:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("FlyDie4");
                    break;
                default:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("FlyDie5");
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
            if (transform.position.y < startPosition.y - curveExtremes)
            {
                moveDirection = Direction.Up;
            }
        }
    }
}
