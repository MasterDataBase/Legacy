using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlanetGravity : MonoBehaviour
{

    public Transform target; // Big object
    Vector3 targetDirection;

    public int radius = 5;
    public int forceAmount = 100;
    public float gravity = 0;
    private Rigidbody rb;

    private float distance;

    public void SetTarget(Transform input)
    {
        target = input;

        ///Si puó fare anche nello start ma é inutile tirare fuori una funzione
        ///che viene triggerata lo stesso numero di volte
        rb = GetComponentInParent<Rigidbody>();
        SetValue(GetComponentInParent<HolderDataGravity>().dataForGravity);
    }

    void SetValue(DataGravitySO data)
    {
        radius = data.GetRadius();
        forceAmount = data.GetForceAmount();
        gravity = data.GetGravity();
    }

    private void Update()
    {
        if (target != null)
        {
            GravityOnTarget();
        }
    }

    /// <summary>
    /// Puó essere davvero cosi semplice? 
    /// </summary>
    void GravityOnTarget()
    {
        //targetDirection = target.position - transform.position; // Save direction
        targetDirection = transform.position - target.position; // Save direction
        //Debug.Log("Target Direction: " + targetDirection);
        distance = targetDirection.magnitude; // Find distance between this object and target object
        //Debug.Log("Distance: " + distance);
        targetDirection = targetDirection.normalized; // Normalize target direction vector

        if (distance < radius)
        {
            //rb.AddForce(targetDirection * forceAmount * Time.deltaTime);
            target.GetComponent<Rigidbody>().AddForce(targetDirection * forceAmount * Time.deltaTime);
            //Debug.Log("Force added: " + targetDirection* forceAmount *Time.deltaTime);
        }
    }

    public void Destroy()
    {
        Destroy(this);
    }
}
