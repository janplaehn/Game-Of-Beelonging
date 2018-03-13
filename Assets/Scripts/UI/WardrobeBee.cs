using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeBee : MonoBehaviour {



	void Start () {
	}
	
	void Update () {
        switch (GameManager.costume) {
            case GameManager.Costumes.Default:
                GetComponent<Animator>().Play("wardrobe_default");
                GetComponent<Renderer>().material.color = Color.white;
                break;
            case GameManager.Costumes.Sombrero:
                GetComponent<Animator>().Play("wardrobe_sombrero");
                if (GameManager.isSombreroUnlocked != 0) {
                    GetComponent<Renderer>().material.color = Color.white;
                }
                else {
                   GetComponent<Renderer>().material.color = new Color(0.02f, 0.02f, 0.02f, 1);
                }
                break;
            case GameManager.Costumes.Pirate:
                GetComponent<Animator>().Play("wardrobe_pirate");
                if (GameManager.isPiratehatUnlocked != 0) {
                    GetComponent<Renderer>().material.color = Color.white;
                }
                else {
                    GetComponent<Renderer>().material.color = new Color(0.02f, 0.02f, 0.02f, 1);
                }
                break;
            case GameManager.Costumes.Viking:
                GetComponent<Animator>().Play("wardrobe_viking");
                if (GameManager.isVikinghatUnlocked != 0) {
                    GetComponent<Renderer>().material.color = Color.white;
                }
                else {
                    GetComponent<Renderer>().material.color = new Color(0.02f, 0.02f, 0.02f, 1);
                }
                break;
            case GameManager.Costumes.Party:
                GetComponent<Animator>().Play("wardrobe_party");
                if (GameManager.isPartyhatUnlocked != 0) {
                    GetComponent<Renderer>().material.color = Color.white;
                }
                else {
                    GetComponent<Renderer>().material.color = new Color(0.02f, 0.02f, 0.02f, 1);
                }
                break;
            case GameManager.Costumes.Roman:
                GetComponent<Animator>().Play("wardrobe_roman");
                if (GameManager.isRomanhatUnlocked != 0) {
                    GetComponent<Renderer>().material.color = Color.white;
                }
                else {
                    GetComponent<Renderer>().material.color = new Color(0.02f, 0.02f, 0.02f, 1);
                }
                break;
            case GameManager.Costumes.King:
                GetComponent<Animator>().Play("wardrobe_king");
                if (GameManager.isKinghatUnlocked != 0) {
                    GetComponent<Renderer>().material.color = Color.white;
                }
                else {
                    GetComponent<Renderer>().material.color = new Color(0.02f, 0.02f, 0.02f, 1);
                }
                break;
            case GameManager.Costumes.Gangster:
                GetComponent<Animator>().Play("wardrobe_gangster");
                if (GameManager.isGangsterhatUnlocked != 0) {
                    GetComponent<Renderer>().material.color = Color.white;
                }
                else {
                    GetComponent<Renderer>().material.color = new Color(0.02f, 0.02f, 0.02f, 1);
                }
                break;
            default:
                break;
        }
    }
}
