using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour {

    public float speed;

    private GameObject MainCamera;
    private float rightBoundary;


    void Start() {

        MainCamera = GameObject.Find("Main Camera");
        rightBoundary = MainCamera.GetComponent<MainCamera>().offset + 15f;

        int soundNumber = Random.Range(0, 2);
        switch (soundNumber) {
            case 0:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BeeMissile1");
                break;
            case 1:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BeeMissile2");
                break;
            default:
                GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("BeeMissile1");
                break;
        }
    }

    void Update() {
        if (transform.position.x <= rightBoundary) {
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else {
            Destroy(gameObject);
        }
    }
}
