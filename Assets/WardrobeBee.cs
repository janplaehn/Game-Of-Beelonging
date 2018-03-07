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
                break;
            case GameManager.Costumes.Sombrero:
                GetComponent<Animator>().Play("wardrobe_sombrero");
                break;
            case GameManager.Costumes.Party:
                GetComponent<Animator>().Play("wardrobe_party");
                break;
            default:
                break;
        }
    }
}
