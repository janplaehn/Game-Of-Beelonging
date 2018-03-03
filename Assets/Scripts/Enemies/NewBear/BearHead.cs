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
    public Transform wasp;
    public bool isWaspSpawningRequested;


	void Start () {
        StartCoroutine(DelayAttack(3f));
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
        if (mouthOpen) {
            GetComponent<Animator>().Play("bear_openmouth");
        }
        else {
            if (isHit) {
                GetComponent<Animator>().Play("bear_hit");
            }
            else if (isEating) {
                GetComponent<Animator>().Play("bear_eating");
            }
            else {
                GetComponent<Animator>().Play("bear_default");
            }
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
        StartCoroutine(AnimationBackToDefault());
    }

    public void PlayEatAnimation() {
        isEating = true;
        StartCoroutine(AnimationBackToDefault());
    }

    public void SpawnWasp() {
        Instantiate(wasp, waspSpawnpont.transform.position, Quaternion.identity);
    }

    IEnumerator AnimationBackToDefault() {
        yield return new WaitForSeconds(0.5f);
        isHit = false;
        isEating = false;
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
    }
}
