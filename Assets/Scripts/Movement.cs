using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParicles;
    [SerializeField] ParticleSystem leftThrusterParicles;
    [SerializeField] ParticleSystem rightThrusterParicles;

    Rigidbody rb;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParicles.isPlaying)
        {
            mainEngineParicles.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParicles.Stop();
    }

    void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!rightThrusterParicles.isPlaying)
        {
            rightThrusterParicles.Play();
        }
    }

    void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!leftThrusterParicles.isPlaying)
        {
            leftThrusterParicles.Play();
        }
    }

    void StopRotating()
    {
        rightThrusterParicles.Stop();
        leftThrusterParicles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
