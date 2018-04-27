using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnRoad : MonoBehaviour
{
    [HideInInspector] public List<GameObject> roads;
    [HideInInspector] public List<RoadType> roadTypes;
    [HideInInspector]
    public enum RoadType
    {
        straight, right, left
    }
    public float turnSpeed;
    public int roadNum
    {
        get { return roads.Count; }
    }

    private GameObject currentTurn;
    private TurnPreRender turnPreRender;
    private Quaternion targetRotation;
    private int renderTo;

    private List<TurnPreRender> preRenders;

	void Start ()
    {
        roads = new List<GameObject>();
        roadTypes = new List<RoadType>();
        preRenders = new List<TurnPreRender>();
	}

    void Update()
    {
        if (currentTurn != null)
        {
            currentTurn.transform.rotation = Quaternion.RotateTowards(currentTurn.transform.rotation, targetRotation, turnSpeed*Time.deltaTime);
        }
        //renderMeshes();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.Equals(roads[0]))
        {
            if (roadTypes[0] != RoadType.straight)
            {
                currentTurn = roads[0];
                turnPreRender = currentTurn.GetComponent<TurnPreRender>();
                if (roadTypes[0] == RoadType.left)
                    targetRotation = Quaternion.Euler(new Vector3(0, 90, 0));
                else if (roadTypes[0] == RoadType.right)
                    targetRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                preRenders[0].disable();
            }
            roads.RemoveAt(0);
            roadTypes.RemoveAt(0);
            preRenders.RemoveAt(0);
            renderMeshes();
        }
    }

    public void addRoadSegment(GameObject road, RoadType type)
    {
        roads.Add(road);
        roadTypes.Add(type);
        preRenders.Add(road.GetComponent<TurnPreRender>());
        switch (type)
        {
            case RoadType.right:
                road.GetComponent<TurnPreRender>().enable(TurnPreRender.Side.right);
                break;
            case RoadType.left:
                road.GetComponent<TurnPreRender>().enable(TurnPreRender.Side.left);
                break;
        }
        renderMeshes();
    }

    private void renderMeshes()
    {
        int i;
        for (i = 0; i < roads.Count; i++)
        {
            MeshRenderer[] meshes = roads[i].GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mr in meshes)
                mr.enabled = true;
            if (roadTypes[i] != RoadType.straight)
                break;
        }
        for (i += 1; i < roads.Count; i++)
        {
            MeshRenderer[] meshes = roads[i].GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mr in meshes)
                mr.enabled = false;
        }
    }

    private void enableMeshes (GameObject obj, bool active)
    {
        MeshRenderer[] meshes = obj.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mr in meshes)
            mr.enabled = active;
    }
}
