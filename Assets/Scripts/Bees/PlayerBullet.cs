using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    public float speed;
    [ShowOnly] public bool isAlive;

    private GameObject MainCamera;
    private float rightBoundary;


	void Start () {

        isAlive = true;
        MainCamera = GameObject.Find("Main Camera");
        rightBoundary = MainCamera.GetComponent<MainCamera>().offset + 12f;
    }
	
	void Update () {
		if (transform.position.x <= rightBoundary)
        {
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else
        {
            Destroy(gameObject);
        }
        if (!isAlive) {
            GetComponent<Animator>().Play("playerbullet_splash");
        }
	}

    public void Die() {
        isAlive = false;
        speed = 2;
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy() {
        yield return new WaitForSeconds(0.15f);
        Destroy(gameObject);
    }
}
