using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateDebuff : Debuff
{
    public Camera cam;
    public Vector3 [] debuffRotations;
    public float maxTurnDelta;

    private Quaternion [] targetRotations;
    private Quaternion defaultRotation;

    private int targetRotation;
    private bool rotationSwap;

    private void Start()
    {
        defaultRotation = cam.transform.rotation;
        targetRotations = new Quaternion[debuffRotations.Length];
        for (int i = 0; i < targetRotations.Length; i++)
            targetRotations[i] = Quaternion.Euler(debuffRotations[i]);
        rotationSwap = true;
    }

    public override void debuff()
    {
        if (rotationSwap)
        {
            targetRotation = Random.Range(0, targetRotations.Length);
            rotationSwap = false;
        }
        cam.transform.rotation = Quaternion.RotateTowards(cam.transform.rotation, targetRotations[targetRotation], maxTurnDelta*Time.deltaTime);
    }

    public override void removeDebuff()
    {
        rotationSwap = true;
        cam.transform.rotation = Quaternion.RotateTowards(cam.transform.rotation, defaultRotation, maxTurnDelta*Time.deltaTime);
    }
}
