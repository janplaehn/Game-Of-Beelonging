using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalBeeSpawning : MonoBehaviour {

    public Transform bee;

    private GameObject MainCamera;    

	// Use this for initialization
	void Start () {
        MainCamera = GameObject.Find("Main Camera"); 
	}
	
	// Update is called once per frame
	void Update () {
        if ((transform.position.x <= MainCamera.GetComponent<MainCamera>().offset + 10f) && GameManager.beeCount >= 10) {
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D otherCollider) {
        if (otherCollider.tag == "MainBee") {
            if (GameManager.beeCount < 10) {
            Instantiate(bee, transform.position, Quaternion.identity);
                GameManager.beeCount += 1;
                int soundNumber = Random.Range(0, 3);
                switch (soundNumber) {
                    case 0:
                        GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("Cheer1");
                        break;
                    case 1:
                        GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("Cheer2");
                        break;
                    case 2:
                        GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("Cheer3");
                        break;
                    default:
                        GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("Cheer1");
                        break;
                }
                Destroy(this.gameObject);
            }
        }
    }
}
