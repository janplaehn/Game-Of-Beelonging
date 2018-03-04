using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BearHead : MonoBehaviour {

    public float healthPoints;
    public float speed;
    public GameObject leftPaw;
    public GameObject rightPaw;
    public float attackFrequency;
    public GameObject gameManager;
    public GameObject waspSpawnpont;
    public bool mouthOpen;
    public bool isHit;
    public bool isEating;
    public bool isOpeningMouth;
    public Transform wasp;
    public bool isWaspSpawningRequested;


	void Start () {
        mouthOpen = false;
        isHit = false;
    }

    void Update() {
        if (healthPoints <= 0) {
            StartCoroutine(LoadWinScreen());
            this.GetComponent<Rigidbody2D>().gravityScale = 2.0f;
            leftPaw.GetComponent<Rigidbody2D>().gravityScale = 2.0f;
            rightPaw.GetComponent<Rigidbody2D>().gravityScale = 2.0f;
            foreach (GameObject wasp in GameObject.FindGameObjectsWithTag("Wasp")) {
                wasp.GetComponent<Wasp>().Die();
            }
        }
        if (transform.position.y <= -8) {
            GameManager.ToggleCursorVisibility(true);
            GameManager.isInLevel = false;
            SceneManager.LoadScene("Win Screen", LoadSceneMode.Single);
        }
        if (transform.position.x > 5) {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (isHit) {
            GetComponent<Animator>().Play("bear_hit");
        }
        else if (isOpeningMouth) {
            GetComponent<Animator>().Play("bear_openingmouth");
        }
        else if (isEating) {
            GetComponent<Animator>().Play("bear_lick");
        }   
        else if (mouthOpen) {
            GetComponent<Animator>().Play("bear_openmouth");
        }
        else {
            GetComponent<Animator>().Play("bear_default");
        }
    }

    public void Attack() {
        int tempNumber = Random.Range(0, 2);
        if (tempNumber == 0) leftPaw.GetComponent<PawMovement>().battleState = PawMovement.State.Indicate;
        else rightPaw.GetComponent<PawMovement>().battleState = PawMovement.State.Indicate;
    }

    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.tag == "PlayerBullet" && otherCollider.GetComponent<PlayerBullet>().isAlive) {
            otherCollider.GetComponent<PlayerBullet>().Die();
        }
    }

    public void PlayHitAnimation() {
        isHit = true;
        int soundNumber = Random.Range(0, 5);
        switch (soundNumber) {
            case 0:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BearHit1");
                break;
            case 1:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BearHit2");
                break;
            case 2:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BearHit3");
                break;
            case 3:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BearHit4");
                break;
            default:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BearHit5");
                break;
        }
        StartCoroutine(AnimationBackToDefault());
    }

    public void PlayEatAnimation() {
        int soundNumber = Random.Range(0, 5);
        switch (soundNumber) {
            case 0:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BearEat1");
                break;
            case 1:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BearEat2");
                break;
            case 2:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BearEat3");
                break;
            case 3:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BearEat4");
                break;
            default:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BearEat5");
                break;
        }
        isEating = true;
        StartCoroutine(AnimationBackToDefault());
    }

    public void PlayOpeningMouthAnimation() {
        isOpeningMouth = true;
        StartCoroutine(AnimationBackToDefault());
    }

    public void SpawnWasp() {
        Instantiate(wasp, waspSpawnpont.transform.position, Quaternion.identity);
    }

    IEnumerator AnimationBackToDefault() {
        yield return new WaitForSeconds(0.3f);
        if (isWaspSpawningRequested) {
            isWaspSpawningRequested = false;
            SpawnWasp();
            int soundNumber = Random.Range(0, 5);
            switch (soundNumber) {
                case 0:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BearCough1");
                    break;
                case 1:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BearCough2");
                    break;
                case 2:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BearCough3");
                    break;
                case 3:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BearCough4");
                    break;
                default:
                    GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BearCough5");
                    break;
            }
        }
        isHit = false;
        isEating = false;
        isOpeningMouth = false;
    }

    IEnumerator LoadWinScreen() {
        yield return new WaitForSeconds(1f);
        GameManager.ToggleCursorVisibility(true);
        GameManager.isInLevel = false;
        SceneManager.LoadScene("Win Screen", LoadSceneMode.Single);
    }

    public void StartAttackInSeconds(float time) {
        StartCoroutine(DelayAttack(time));
    }

    public IEnumerator DelayAttack(float time) {
        yield return new WaitForSeconds(time);
        Attack();
        PlayOpeningMouthAnimation();
    }
}
