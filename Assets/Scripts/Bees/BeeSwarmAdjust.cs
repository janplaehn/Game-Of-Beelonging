using UnityEngine;

public class BeeSwarmAdjust : MonoBehaviour {

    public float adjustSpeed;

	void Start () {
        
    }
	
	void Update () {
        if (!(transform.position.x < 0.05 && transform.position.x > 0.05)) {
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y - getAIBeeCenter() * Time.deltaTime, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, newPosition, adjustSpeed * Time.deltaTime);
        }

    }

    float getAIBeeCenter() {
        GameObject[] bees = GameObject.FindGameObjectsWithTag("AIBee");
        if (bees.Length == 0) {
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
