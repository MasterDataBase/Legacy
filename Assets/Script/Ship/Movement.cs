using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Script that has to be used during the ship phase
/// </summary>
public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100;
    [SerializeField] float rotationSpeedSX = 1;
    [SerializeField] float rotationSpeedDX = 1;
    Rigidbody rb;
    AudioSource asrc;

    [SerializeField] AudioClip audioSuccess;
    [SerializeField] AudioClip audioCrash;
    [SerializeField] AudioClip audioThruster;

    [SerializeField] ParticleSystem mainThrustP;
    [SerializeField] ParticleSystem leftThrustP;
    [SerializeField] ParticleSystem rightThrustP;

    Player player;

    [SerializeField] float consumeMain;
    [SerializeField] float consumeSide;
    [SerializeField] PowerMeterUI powerMeter;
    [SerializeField] int OFFSET_SPEED = 100;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody>();
        asrc = GetComponent<AudioSource>();

        powerMeter = FindObjectOfType<PowerMeterUI>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessDirection();
    }

    private void ProcessDirection()
    {
    
        if (Input.GetKey(KeyCode.A))
        {
            if (player.GetCurrentFuel() > 0)
            {
                player.DecriseFuel(consumeSide);
                ApplyRotation(rotationSpeedDX);
                if (!rightThrustP.isPlaying)
                { rightThrustP.Play(); } 
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (player.GetCurrentFuel() > 0)
            {
                player.DecriseFuel(consumeSide);
                ApplyRotation(-rotationSpeedSX);
                if (!leftThrustP.isPlaying)
                { leftThrustP.Play(); } 
            }
        }
        else
        {
            leftThrustP.Stop();
            rightThrustP.Stop();
            rb.angularVelocity *= 0.6f;
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation  = true;
        //transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.AddRelativeTorque(Vector3.forward * rotationThisFrame * Time.deltaTime, ForceMode.VelocityChange);
        rb.freezeRotation  = false;
    }

    void ProcessThrust(){

        if (Input.GetKey(KeyCode.Space))
        {
            if (player.GetCurrentFuel() > 0)
            {
                player.DecriseFuel(consumeMain);
                if (!asrc.isPlaying)
                {
                    asrc.PlayOneShot(audioThruster);

                }
                if (!mainThrustP.isPlaying) { mainThrustP.Play(); }
                rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime); 
            }
        }
        else
        {
            asrc.Stop();
            mainThrustP.Stop();
        } 
    }

    public void AudioSuccess(){
        asrc.PlayOneShot(audioSuccess);
    }

    public void AudioCrash(){
        asrc.PlayOneShot(audioCrash);
    }

    private void FixedUpdate()
    {
        float speed = rb.velocity.magnitude; // Unit / physic dt
        speed *= Time.fixedDeltaTime; // Unit / s, fixedDeltaTime being 1 / physic dt
        powerMeter.speed = speed * OFFSET_SPEED;
    }

    #region HealthStatus
    public void SetMainThrust(float input) { mainThrust = input; }
    #endregion
}
