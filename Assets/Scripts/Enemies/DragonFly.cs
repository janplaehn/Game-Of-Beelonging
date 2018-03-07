using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFly : MonoBehaviour {

    public int healthPoints;
    public float forwardMoveSpeed;
    public float backwardMoveSpeed;
    public float positionThreshold;

    [HideInInspector]  public bool isAlive;

    private GameObject MainCamera;
    private bool isCharging = false;

    void Start()
    {
        this.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        isAlive = true;
        healthPoints = 3;
        MainCamera = GameObject.Find("Main Camera");
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
            GetComponent<Animator>().Play("dragonfly_death");
        }
    }

    void OnTriggerStay2D(Collider2D otherCollider) {
        if (otherCollider.tag == "PlayerBullet" && otherCollider.GetComponent<PlayerBullet>().isAlive) {
            otherCollider.GetComponent<PlayerBullet>().Die();
            healthPoints--;
            int soundNumber = Random.Range(0, 5);
            switch (soundNumber) {
                case 0:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("DragonFlyHit1");
                    break;
                case 1:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("DragonFlyHit2");
                    break;
                case 2:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("DragonFlyHit3");
                    break;
                case 3:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("DragonFlyHit4");
                    break;
                case 4:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("DragonFlyHit5");
                    break;
                default:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("DragonFlyHit4");
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
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("DragonFlyDie1");
                break;
            case 1:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("DragonFlyDie2");
                break;
            case 2:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("DragonFlyDie3");
                break;
            case 3:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("DragonFlyDie4");
                break;
            case 4:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("DragonFlyDie5");
                break;
            default:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("DragonFlyDie4");
                break;
        }
    }

    void Move() {
        if (transform.position.x < MainCamera.transform.position.x + 10 && transform.position.x > MainCamera.transform.position.x + 4) {
            transform.position = new Vector3(transform.position.x + backwardMoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            
        }
        else if (transform.position.x < MainCamera.transform.position.x + 4 && transform.position.x > MainCamera.transform.position.x + positionThreshold) {
            transform.position = new Vector3(transform.position.x + backwardMoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            GetComponent<Animator>().Play("dragonfly_charge");
        }
        if (transform.position.x < MainCamera.transform.position.x + positionThreshold && !isCharging) {
            isCharging = true;
            int soundNumber = Random.Range(0, 4);
            switch (soundNumber) {
                case 0:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("DragonFlyAttack1");
                    break;
                case 1:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("DragonFlyAttack2");
                    break;
                case 2:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("DragonFlyAttack3");
                    break;
                default:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("DragonFlyAttack1");
                    break;
            }
        }
        if (transform.position.x < MainCamera.transform.position.x + positionThreshold) {
            transform.position = new Vector3(transform.position.x - forwardMoveSpeed* Time.deltaTime, transform.position.y, transform.position.z);
            GetComponent<Animator>().Play("dragonfly_attack");
        }
        
    }
}
