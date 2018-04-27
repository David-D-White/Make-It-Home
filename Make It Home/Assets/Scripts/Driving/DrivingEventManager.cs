using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingEventManager : MonoBehaviour
{
    public float startDelay;
    public CameraFade camFade;
    public MusicFade crickets;
    public AudioSource engine;
    public Timer timer;
    public PopIn messagePop;
    public TextFade messageFade;
    public EndMessage endMessage;
    public CarController car;
    public DebuffManager debuffManager;
    public float maxDebuffCooldown;
    [Range (0,1)] public float cooldownScale;
    public float minDebuffDuration;
    [Range(0, 1)] public float durationScale;

    private float delay;
    private float debuffCooldown;
    private float debuffDuration;
    private float cooldownTimer;

	void Start ()
    {
        delay = startDelay;
        debuffCooldown = maxDebuffCooldown / (1 + Data.numDrinks * cooldownScale);
        debuffDuration = minDebuffDuration * (1 + Data.numDrinks * durationScale);
        if (Data.numDrinks == 0)
        {
            debuffCooldown = 0;
            debuffDuration = 0;
        }
        cooldownTimer = debuffCooldown;
        crickets.fadeIn();
	}

	void Update ()
    {

        if (!timer.running)
        {
            if (delay > 0)
            {
                delay -= Time.deltaTime;
            }
            else if (messagePop.poppedOut())
            {
                messagePop.popIn();
                engine.Play();
                delay = 1;
            }
            else if (messagePop.poppedIn() && camFade.Saturated())
            {
                camFade.Fade();
                messagePop.stopPop();
                delay = 1;
            }
            else if (camFade.Faded() && !timer.running)
            {
                timer.running = true;
                car.controlsEnabled = true;
                messageFade.Fade();
            }
        }
        else if (timer.time <= 0 || car.crashed)
        {
            if (camFade.Faded())
            {
                camFade.ReverseFade();
                endMessage.display(!car.crashed);
            }
            else if (camFade.Saturated())
            {
                camFade.StopFade();
            }
        }
        else if (debuffCooldown != 0)
        {
            if (cooldownTimer > 0)
            {
                cooldownTimer -= Time.deltaTime;
            }
            else
            {
                debuffManager.debuff(debuffDuration);
                cooldownTimer = debuffCooldown;
            }
        }
    }
}
