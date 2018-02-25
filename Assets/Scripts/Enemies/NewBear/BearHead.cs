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
    public bool mouthOpen;
    public bool isHit;


	void Start () {
        InvokeRepeating("Attack", 3.0f, attackFrequency);
        mouthOpen = false;
        isHit = false;
    }

    void Update() {
        if (healthPoints <= 0) {
            this.GetComponent<Rigidbody2D>().gravityScale = 2.0f;
            leftPaw.GetComponent<Rigidbody2D>().gravityScale = 2.0f;
            rightPaw.GetComponent<Rigidbody2D>().gravityScale = 2.0f;
        }
        if (transform.position.y <= -12) {
            Debug.Log("Dead");
            gameManager.GetComponent<GameManager>().ToggleCursorVisibility(true);
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
            else {
                GetComponent<Animator>().Play("bear_default");
            }
        }
    }

    void Attack() {
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

    IEnumerator AnimationBackToDefault() {
        yield return new WaitForSeconds(0.5f);
        isHit = false;
    }
}
