using UnityEngine;

public class BeeSwarmAdjust : MonoBehaviour {

    public float adjustSpeed;
    public bool isInBossFight = false;

	void Start () {
        if (GameManager.beeCount < 9) Destroy(GameObject.Find("AIBee"));
        if (GameManager.beeCount < 8) Destroy(GameObject.Find("AIBee (1)"));
        if (GameManager.beeCount < 7) Destroy(GameObject.Find("AIBee (2)"));
        if (GameManager.beeCount < 6) Destroy(GameObject.Find("AIBee (3)"));
        if (GameManager.beeCount < 5) Destroy(GameObject.Find("AIBee (4)"));
        if (GameManager.beeCount < 4) Destroy(GameObject.Find("AIBee (5)"));
        if (GameManager.beeCount < 3) Destroy(GameObject.Find("AIBee (6)"));
        if (GameManager.beeCount < 2) Destroy(GameObject.Find("AIBee (7)"));
        if (GameManager.beeCount < 1) Destroy(GameObject.Find("AIBee (8)"));
    }
	
	void Update () {
        if (Input.GetMouseButton(1) || isInBossFight) {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 0, 0), adjustSpeed * Time.deltaTime);
        }
        else {
            Adjust();
        }
       

    }

    void Adjust() {
        if (!(transform.position.x < 0.05 && transform.position.x > 0.05)) {
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y - getAIBeeCenter() * Time.deltaTime, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, newPosition, adjustSpeed * Time.deltaTime);
        }
    }

    float getAIBeeCenter() {
        GameObject[] bees = GameObject.FindGameObjectsWithTag("AIBee");
        if (bees.Length <= 1) {
            return 0;
        }
        float totalYPos = 0;
        float beeCount = 0;
        foreach (GameObject bee in bees) {
            totalYPos += bee.transform.position.y;
            beeCount++;
        }
        return totalYPos / beeCount;
    }
}
