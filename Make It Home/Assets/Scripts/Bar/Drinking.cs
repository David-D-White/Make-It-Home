using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drinking : BarEvent
{
    public float spawnInDist;
    public float spawnInSpeed;
    public int drinksToEmpty;
    public MeshRenderer[] meshes;
    public AudioSource drinkSound;

    private Vector3 animDirection;
    private Rigidbody rb;
    private int activeMesh;
    private float[] drinkLevels;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        active = false;
        //Setup Meshes
        activeMesh = 0;
        foreach (MeshRenderer m in meshes)
            m.enabled = false;
        meshes[activeMesh].enabled = true;
        //Setup meshes division
        drinkLevels = new float[meshes.Length - 1];
        drinkLevels[drinkLevels.Length - 1] = 0;
        float drinksPerLevel = (float)drinksToEmpty / (drinkLevels.Length); 
        for (int i = 0; i < drinkLevels.Length - 1; i++)
        {
            drinkLevels[i] = (drinkLevels.Length - i - 1) * drinksPerLevel;
        }
	}
	
	void Update ()
    {
        drink();
	}

    private void OnTriggerEnter(Collider other)
    {
        rb.velocity = Vector3.zero;
        if (other.CompareTag("BarEventStart"))
            active = true;
        else if (other.CompareTag("BarEventEnd"))
            active = false;
    }

    void drink ()
    {
        if (active && Input.GetButtonDown("Drink"))
        {
            drinksToEmpty --;
            if (activeMesh < meshes.Length - 1 && drinksToEmpty <= drinkLevels [activeMesh])
            {
                meshes[activeMesh].enabled = false;
                activeMesh++;
                meshes[activeMesh].enabled = true;
                if (!drinkSound.isPlaying)
                    drinkSound.Play();
            }
        }
    }

    public override bool completed()
    {
        return drinksToEmpty <= 0;
    }

    public override bool animating()
    {
        return rb.velocity != Vector3.zero;
    }

    public override void animateIn(Transform parentTransform)
    {
        active = false;
        rb = GetComponent<Rigidbody>();
        Vector3 relPos = parentTransform.position;
        rb.transform.position =  new Vector3 (relPos.x - spawnInDist, relPos.y, relPos.z);
        animDirection = parentTransform.position - rb.transform.position;
        animDirection /= animDirection.magnitude;
        rb.velocity = animDirection * spawnInSpeed;
    }

    public override void animateOut(Transform parentTransform)
    {
        rb.velocity = animDirection * spawnInSpeed;
    }
}
