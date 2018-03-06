using UnityEngine;
using System.Collections;

public class RepeatingBackground : MonoBehaviour
{

    public float scrollSpeed;
    public float tileSize;
    private GameObject MainCamera;

    void Start() {

        MainCamera = GameObject.Find("Main Camera");

    }

    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            scrollSpeed *= 1.2f;
        }
        if (Input.GetMouseButtonUp(1)) {
            scrollSpeed /= 1.2f;
        }
        transform.position = new Vector3(transform.position.x - scrollSpeed * Time.deltaTime + MainCamera.GetComponent<MainCamera>().speed * Time.deltaTime / 100, transform.position.y, transform.position.z);
        if (transform.position.x < MainCamera.GetComponent<MainCamera>().offset - tileSize) {
            transform.position = new Vector3(transform.position.x + tileSize + MainCamera.GetComponent<MainCamera>().speed * Time.deltaTime / 100, transform.position.y, transform.position.z);
        }
    }
}