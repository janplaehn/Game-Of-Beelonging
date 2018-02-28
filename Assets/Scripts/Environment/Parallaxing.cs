using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//Based on this tutorial: https://www.youtube.com/watch?v=QkisHNmcK7Y
public class Parallaxing : MonoBehaviour {

    public List<Transform> children;
    public float smoothing = 1f;

    [ShowOnly] [SerializeField] private Transform cam;

    private float[] parallaxScales;
    private Vector3 previousCamPos;

    void Awake() {
        cam = Camera.main.transform;

        foreach (Transform child in transform) {
           children.Add(child.gameObject.transform);
        }
    }

    void Start() {
        previousCamPos = cam.position;
        parallaxScales = new float[children.Count];
        for (int i = 0; i < children.Count; i++) {
            parallaxScales[i] = children[i].position.z * -1;
        }
    }

    void FixedUpdate() {
        for (int i = 0; i < children.Count; i++) {
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
            float backgroundTargetPosX = children[i].position.x + parallax;
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, children[i].position.y, children[i].position.z);
            children[i].position = Vector3.Lerp(children[i].position, backgroundTargetPos, smoothing);
        }
        previousCamPos = cam.position;
    }   
}