using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float turnSpeed;
    public float rotationSpeed;
    public Vector3 turnRotation;
    public Transform meshTransform;
    public AudioSource crashSound;
    public AudioSource engineSound;
    public bool controlsEnabled;

    private Rigidbody rigidBody;
    private float turnInput;
    private Quaternion defaultRotation;
    private bool crashed_;

    public bool crashed
    {
        get { return crashed_; }
    }

	// Use this for initialization
	void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
        defaultRotation = rigidBody.rotation;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (controlsEnabled && !crashed_)
            GetInput();
        else if (crashed_ && !crashSound.isPlaying)
            engineSound.Stop();
        Move();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Collidable") || collision.collider.CompareTag("Damaging-Collidable"))
        {
            Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = false;
        }
        if (!crashed_ && (collision.collider.CompareTag("Damaging-Collidable") || collision.collider.CompareTag("Damaging")))
        {
            crashed_ = true;
            rigidBody.constraints = RigidbodyConstraints.None;
            rigidBody.useGravity = true;
            crashSound.Play();
            MusicFade crash = crashSound.GetComponent<MusicFade>();
            if (crash != null)
                crash.fadeOut();
            MusicFade engine = engineSound.GetComponent<MusicFade>();
            if (engine != null)
                engine.fadeOut();
        }
    }

    void GetInput ()
    {
        turnInput = Input.GetAxis("Horizontal");
    }

    void Move()
    {
        if (Mathf.Abs(turnInput) > 0.1)
        {
            rigidBody.velocity = Vector3.right * turnInput * turnSpeed;
            meshTransform.rotation = Quaternion.RotateTowards(meshTransform.rotation, defaultRotation * Quaternion.Euler(turnRotation*turnInput), rotationSpeed*Time.deltaTime);
        }
        else
        {
            rigidBody.velocity = Vector3.zero;
            meshTransform.rotation = Quaternion.RotateTowards(meshTransform.transform.rotation, defaultRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
