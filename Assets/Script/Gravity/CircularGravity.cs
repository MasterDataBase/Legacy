using UnityEngine;
using System.Collections;

public class CircularGravity : MonoBehaviour
{

    public Transform target; // Big object
    Vector3 targetDirection;

    public int radius = 5;
    public int forceAmount = 100;
    public float gravity = 0;
    private Rigidbody rb;

    private float distance;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    // Use this for initialization
    void Start()
    {
        Physics.gravity = new Vector3(0, gravity, 0);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        //targetDirection = target.position - transform.position; // Save direction
        ////Debug.Log("Target Direction: " + targetDirection);
        //distance = targetDirection.magnitude; // Find distance between this object and target object
        ////Debug.Log("Distance: " + distance);
        //targetDirection = targetDirection.normalized; // Normalize target direction vector

        //if (distance < radius)
        //{
        //    rb.AddForce(targetDirection * forceAmount * Time.deltaTime);
        //    //Debug.Log("Force added: " + targetDirection* forceAmount *Time.deltaTime);
        //}


    }
    ///Se
    ///1.Convertissi lo script per essere attaccato ai pianeti e agli oggetti che devono attirare l'astronave
    ///2.Se creassi un collider cerchio che detecta se l'astronave o altri oggetti passano attraverso
    ///2.1 Li inserisce in una lista e li attrae finche questi non escono dall'atmosfera.
}
