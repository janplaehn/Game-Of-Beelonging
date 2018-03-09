using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeBee : MonoBehaviour {


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
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
            case GameManager.Costumes.Party:
                GetComponent<Animator>().Play("wardrobe_party");
                if (GameManager.isPartyhatUnlocked != 0) {
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
