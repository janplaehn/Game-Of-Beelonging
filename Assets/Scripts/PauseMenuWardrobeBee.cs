using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuWardrobeBee : MonoBehaviour {

    public Sprite defaultSprite;
    public Sprite sombreroSprite;
    public Sprite partySprite;
    public Sprite romanSprite;
    public Sprite gangsterSprite;
    public Sprite kingSprite;
    public Sprite vikingSprite;
    public Sprite pirateSprite;


    void Start() {



    }

    void Update() {
        switch (GameManager.costume) {
            case GameManager.Costumes.Default:
                GetComponent<Image>().overrideSprite = defaultSprite;
                GetComponent<Image>().color = Color.white;
                break;
            case GameManager.Costumes.Sombrero:
                GetComponent<Image>().overrideSprite = sombreroSprite;
                if (GameManager.isSombreroUnlocked != 0) {
                    GetComponent<Image>().color = Color.white;
                }
                else {
                    GetComponent<Image>().color = new Color(0.02f, 0.02f, 0.02f, 1);
                }
                break;
            case GameManager.Costumes.Pirate:
                GetComponent<Image>().overrideSprite = pirateSprite;
                if (GameManager.isPiratehatUnlocked != 0) {
                    GetComponent<Image>().color = Color.white;
                }
                else {
                    GetComponent<Image>().color = new Color(0.02f, 0.02f, 0.02f, 1);
                }
                break;
            case GameManager.Costumes.Viking:
                GetComponent<Image>().overrideSprite = vikingSprite;
                if (GameManager.isVikinghatUnlocked != 0) {
                    GetComponent<Image>().color = Color.white;
                }
                else {
                    GetComponent<Image>().color = new Color(0.02f, 0.02f, 0.02f, 1);
                }
                break;
            case GameManager.Costumes.Party:
                GetComponent<Image>().overrideSprite = partySprite;
                if (GameManager.isPartyhatUnlocked != 0) {
                    GetComponent<Image>().color = Color.white;
                }
                else {
                    GetComponent<Image>().color = new Color(0.02f, 0.02f, 0.02f, 1);
                }
                break;
            case GameManager.Costumes.Roman:
                GetComponent<Image>().overrideSprite = romanSprite;
                if (GameManager.isRomanhatUnlocked != 0) {
                    GetComponent<Image>().color = Color.white;
                }
                else {
                    GetComponent<Image>().color = new Color(0.02f, 0.02f, 0.02f, 1);
                }
                break;
            case GameManager.Costumes.King:
                GetComponent<Image>().overrideSprite = kingSprite;
                if (GameManager.isKinghatUnlocked != 0) {
                    GetComponent<Image>().color = Color.white;
                }
                else {
                    GetComponent<Image>().color = new Color(0.02f, 0.02f, 0.02f, 1);
                }
                break;
            case GameManager.Costumes.Gangster:
                GetComponent<Image>().overrideSprite = gangsterSprite;
                if (GameManager.isGangsterhatUnlocked != 0) {
                    GetComponent<Image>().color = Color.white;
                }
                else {
                    GetComponent<Image>().color = new Color(0.02f, 0.02f, 0.02f, 1);
                }
                break;
            default:
                break;
        }
    }
}
