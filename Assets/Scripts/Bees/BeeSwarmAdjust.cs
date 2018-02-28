using UnityEngine;

public class BeeSwarmAdjust : MonoBehaviour {

    public float adjustSpeed;
    public bool isInBossFight = false;

	void Start () {
        if (!GameManager.restoreBees) {
            if (GameManager.beeCount < 10) {
                Destroy(GameObject.Find("AIBee"));
                GameObject.Find("Slot").GetComponent<Slot>().isOccupied = false;
            }
            if (GameManager.beeCount < 9) {
                Destroy(GameObject.Find("AIBee (1)"));
                GameObject.Find("Slot (1)").GetComponent<Slot>().isOccupied = false;
            }
            if (GameManager.beeCount < 8) {
                Destroy(GameObject.Find("AIBee (2)"));
                GameObject.Find("Slot (2)").GetComponent<Slot>().isOccupied = false;
            }
            if (GameManager.beeCount < 7) {
                Destroy(GameObject.Find("AIBee (3)"));
                GameObject.Find("Slot (3)").GetComponent<Slot>().isOccupied = false;
            }
            if (GameManager.beeCount < 6) {
                Destroy(GameObject.Find("AIBee (4)"));
                GameObject.Find("Slot (4)").GetComponent<Slot>().isOccupied = false;
            }
            if (GameManager.beeCount < 5) {
                Destroy(GameObject.Find("AIBee (5)"));
                GameObject.Find("Slot (5)").GetComponent<Slot>().isOccupied = false;
            }
            if (GameManager.beeCount < 4) {
                Destroy(GameObject.Find("AIBee (6)"));
                GameObject.Find("Slot (6)").GetComponent<Slot>().isOccupied = false;
            }
            if (GameManager.beeCount < 3) {
                Destroy(GameObject.Find("AIBee (7)"));
                GameObject.Find("Slot (7)").GetComponent<Slot>().isOccupied = false;
            }
            if (GameManager.beeCount < 2) {
                Destroy(GameObject.Find("AIBee (8)"));
                GameObject.Find("Slot (8)").GetComponent<Slot>().isOccupied = false;
            }
        }
        else {
            GameManager.restoreBees = false;
        }
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
