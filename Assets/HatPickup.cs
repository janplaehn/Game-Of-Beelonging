using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatPickup : MonoBehaviour {

    public GameManager.Costumes hatType;

	void Start () {
        switch (hatType) {
            case GameManager.Costumes.Default:
                break;
            case GameManager.Costumes.Sombrero:
                if (GameManager.isSombreroUnlocked != 0) {
                    Destroy(gameObject);
                }
                break;
            case GameManager.Costumes.Party:
                if (GameManager.isPartyhatUnlocked != 0) {
                    Destroy(gameObject);
                }
                break;
            default:
                break;
        }
    }
	
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.tag == "MainBee" && otherCollider.transform.GetComponent<MainBee>().isAlive) {
            switch (hatType) {
                case GameManager.Costumes.Default:
                    break;
                case GameManager.Costumes.Sombrero:
                    GameManager.isSombreroUnlocked = 1;
                    PlayerPrefs.SetInt("isSombreroUnlocked", 1);
                    break;
                case GameManager.Costumes.Party:
                    GameManager.isSombreroUnlocked = 1;
                    PlayerPrefs.SetInt("isPartyhatUnlocked", 1);
                    break;
                default:
                    break;
            }
            GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("Trumpet1");
            Destroy(gameObject);
        }
    }
}
