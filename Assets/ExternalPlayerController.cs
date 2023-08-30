using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class ExternalPlayerController : MonoBehaviour
{
    public Rigidbody rig;
    public ThirdPersonUserControl thirdControl;

    private void Start()
    {
        Physics.gravity = new Vector3(0, -9.81f, 0);
        rig = GetComponent<Rigidbody>();
        thirdControl = GetComponent<ThirdPersonUserControl>();
    }

    public void LockRig()
    {
        rig.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void UnlockRig()
    {
        rig.constraints = RigidbodyConstraints.None;
    }

    public void LockInput()
    {
        thirdControl.dialogueRunning = true;
    }

    public void UnlockInput()
    {
        thirdControl.dialogueRunning = false;
    }
}
