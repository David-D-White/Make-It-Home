using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVDebuff : Debuff
{
    public Camera cam;
    public float debuffFOV;
    public float FOVDelta;

    private float defaultFOV;

    void Start()
    {
        defaultFOV = cam.fieldOfView;
    }

    public override void debuff()
    {
        if (debuffFOV > defaultFOV)
        {
            if (cam.fieldOfView > debuffFOV)
                cam.fieldOfView = debuffFOV;
            else if (cam.fieldOfView < debuffFOV)
                cam.fieldOfView += FOVDelta * Time.deltaTime;
        }
        else
        {
            if (cam.fieldOfView < debuffFOV)
                cam.fieldOfView = debuffFOV;
            else if (cam.fieldOfView > debuffFOV)
                cam.fieldOfView -= FOVDelta * Time.deltaTime;
        }
    }

    public override void removeDebuff()
    {
        if (debuffFOV > defaultFOV)
        {
            if (cam.fieldOfView < defaultFOV)
                cam.fieldOfView = defaultFOV;
            else if (cam.fieldOfView > defaultFOV)
                cam.fieldOfView -= FOVDelta * Time.deltaTime;
        }
        else
        {
            if (cam.fieldOfView > defaultFOV)
                cam.fieldOfView = defaultFOV;
            else if (cam.fieldOfView < defaultFOV)
                cam.fieldOfView += FOVDelta * Time.deltaTime;
        }
    }
}
