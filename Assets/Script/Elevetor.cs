using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Elevetor : MonoBehaviour
{
    public GameObject targetPosition;
    public GameObject VCam1;
    public GameObject VCam2;
    /// <summary>
    /// true uguale upperposition
    /// </summary>
    bool position = false;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Vuoi prendere l'ascensore?");

        position = true;
    }

    private void OnTriggerExit(Collider other)
    {
        position = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && position)
        {
            FindObjectOfType<ThirdPersonCharacter>().transform.position = targetPosition.transform.position;
            VCam2.SetActive(true);
            VCam1.SetActive(false);
        }
    }
}
