using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoad : MonoBehaviour
{
    public GameObject[] straights;
    public GameObject[] obstacles;
    public GameObject[] turns;
    public int roadSpeed;
    [Range(0,100)]public int turnChance;
    public int turnBuffer;
    [Range(0, 100)]public int obstacleChance;
    public int startBuffer;
    public TurnRoad roadTurnScript;

    private int sinceTurn;

	void Start ()
    {
        sinceTurn = 0;
        NewRoad();
	}

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Road") && roadTurnScript.roadNum < 10)
            NewRoad();
    }

    void NewRoad()
    {
        GameObject road;
        TurnRoad.RoadType type;
        if (startBuffer > 0 || sinceTurn < turnBuffer || Random.Range(0, 100) > turnChance)
        {
            if (startBuffer < 0 && sinceTurn >= turnBuffer && Random.Range(0,100) < obstacleChance)
            {
                int index = Random.Range(0, obstacles.Length);
                road = GameObject.Instantiate<GameObject>(obstacles[index], transform.position, transform.rotation);
            }
            else
            {
                int index = Random.Range(0, straights.Length);
                road = GameObject.Instantiate<GameObject>(straights[index], transform.position, transform.rotation);
            }
            Rigidbody rb = road.GetComponent<Rigidbody>();
            if (Random.Range(0, 10) > 5)
                rb.rotation *= Quaternion.Euler(new Vector3(0, 180, 0));
            Vector3 velocity = transform.forward * -1;
            velocity *= roadSpeed;
            rb.velocity = velocity;
            type = TurnRoad.RoadType.straight;
            sinceTurn++;
        }
        else
        {
            Vector3 rotation;
            if (Random.Range(0, 10) >= 5)
            {
                type = TurnRoad.RoadType.left;
                rotation = new Vector3(0, 0, 0);
            }
            else
            {
                type = TurnRoad.RoadType.right;
                rotation = new Vector3(0, -90, 0);
            }
            int index = Random.Range(0, turns.Length);
            road = GameObject.Instantiate<GameObject>(turns[index], transform.position, transform.rotation);
            Rigidbody rb = road.GetComponent<Rigidbody>();
            Vector3 velocity = transform.forward * -1;
            velocity *= roadSpeed;
            rb.velocity = velocity;
            rb.gameObject.transform.rotation = Quaternion.Euler(rotation);
            sinceTurn = 0;
        }
        startBuffer --;
        roadTurnScript.addRoadSegment(road, type);
    }
}
