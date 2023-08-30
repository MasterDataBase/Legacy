using UnityEngine;
using UnityEngine.UI;

///https://www.youtube.com/watch?v=oBkfujKPZw8
public class MissionWaypoint : MonoBehaviour
{
    //values that will be set in the Inspector
    //public Transform Target;
    //public float RotationSpeed;

    //values for internal use
    //private Quaternion _lookRotation;
    //private Vector3 _direction;

    /// <summary>
    /// 
    /// </summary>
    public Transform player;

    // Indicator icon
    public Image img;
    // The target (location, enemy, etc..)
    public Transform target;
    // UI Text to display the distance
    public Text meter;
    //// To adjust the position of the icon
    //public Vector3 offset;

    private void Update()
    {
        ///Equal axis
        Vector3 startPoint = player.position;
        Vector3 endPoint = target.position;
        startPoint.z = 0f;
        endPoint.z = 0f;

        // Change the meter text to the distance with the meter unit 'm'
        meter.text = ((int)Vector3.Distance(endPoint, startPoint)).ToString() + "m";

        #region MDB was here
        //Debug.DrawLine(target.position, player.position, Color.yellow);
        #endregion

        CompassSystem();
    }

    /// <summary>
    /// A function to keep aligned the compass with the waypoint
    /// </summary>
    private void CompassSystem()
    {
        ///https://forum.unity.com/threads/2d-ui-compass-help.550780/
        ///it's a simple script but quite unbreakable
        ///Keep the distanze between the player and the waypoint.
        Vector3 dir = target.position - player.position;
        ///Variation for the script
        //float angle = Vector2.SignedAngle(Vector2.right, dir);
        ///Set the direction for the angle
        float angle = Vector2.SignedAngle(Vector2.up, dir);
        ///Set the compass rotation based on the angle
        img.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    #region comments
    //private void FixedUpdate()
    //{
    ///Questo script ruota un oggetto sulla posizione di un asse.
    //img.transform.RotateAround(Vector3.forward, img.transform.rotation.eulerAngles, movement * Time.fixedDeltaTime * -speedMov);
    //}
    #endregion
}