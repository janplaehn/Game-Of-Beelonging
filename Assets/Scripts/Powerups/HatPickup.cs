using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatPickup : MonoBehaviour {

    public GameObject unlockCanvas;

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
            case GameManager.Costumes.Gangster:
                if (GameManager.isGangsterhatUnlocked != 0) {
                    Destroy(gameObject);
                }
                break;
            case GameManager.Costumes.Roman:
                if (GameManager.isRomanhatUnlocked != 0) {
                    Destroy(gameObject);
                }
                break;
            case GameManager.Costumes.King:
                if (GameManager.isKinghatUnlocked != 0) {
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
                    GameManager.isPartyhatUnlocked = 1;
                    PlayerPrefs.SetInt("isPartyhatUnlocked", 1);
                    break;
                case GameManager.Costumes.Gangster:
                    GameManager.isGangsterhatUnlocked = 1;
                    PlayerPrefs.SetInt("isGangsterhatUnlocked", 1);
                    break;
                case GameManager.Costumes.Roman:
                    GameManager.isRomanhatUnlocked = 1;
                    PlayerPrefs.SetInt("isRomanhatUnlocked", 1);
                    break;
                case GameManager.Costumes.King:
                    GameManager.isKinghatUnlocked = 1;
                    PlayerPrefs.SetInt("isKinghatUnlocked", 1);
                    break;
                default:
                    break;
            }
            GameObject.Find("_SoundManager").GetComponent<SoundManager>().Play("Trumpet1");
            unlockCanvas.SetActive(true);
            unlockCanvas.GetComponent<HatUnlockUI>().DisableAfterSecond();
            Destroy(gameObject);
        }
    }
}
