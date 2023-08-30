using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Yarn.Unity.Example;

public class ChatZone : MonoBehaviour
{
    public GameObject dialogueIcon;

    private void OnTriggerEnter(Collider other)
    {
        dialogueIcon.SetActive(true);
        FindObjectOfType<PlayerInput>().setNPC(GetComponentInParent<NPC>());
    }

    private void OnTriggerExit(Collider other)
    {
        dialogueIcon.SetActive(false);
        FindObjectOfType<PlayerInput>().swipeNPC();
    }

    public void disableIcon()
    {
        dialogueIcon.SetActive(false);
    }

    public void enableIcon()
    {
        dialogueIcon.SetActive(true);
    }
}