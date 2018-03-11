using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatUnlockUI : MonoBehaviour {

    public void DisableAfterSecond() {
        StartCoroutine(Disable());
    }

    public IEnumerator Disable() {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
