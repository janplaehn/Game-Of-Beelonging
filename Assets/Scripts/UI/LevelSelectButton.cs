using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour {

    public int buttonNumber;
    public Button button;

	void Start () {
	if (buttonNumber <= GameManager.unlockedLevelNumber) {
            button.interactable = true;
        }
    else {
            button.interactable = false;
        }
	}
}
