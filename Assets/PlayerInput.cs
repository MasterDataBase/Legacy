using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Yarn.Unity.Example;

/// <summary>
/// Script that has to be used during the player phase
/// </summary>
public class PlayerInput : MonoBehaviour
{
    // because we are using the same button press for both starting and skipping dialogue they collide
    // so we are going to make it so that the input gets turned off
    private DialogueAdvanceInput dialogueInput;

    private NPC talkToChar ;

    private void Start()
    {
        dialogueInput = FindObjectOfType<DialogueAdvanceInput>();
        dialogueInput.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<DialogueRunner>().IsDialogueRunning == true)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(talkToChar != null)
            {
                // Kick off the dialogue at this node.
                FindObjectOfType<DialogueRunner>().StartDialogue(talkToChar.talkToNode);
                // reenabling the input on the dialogue
                dialogueInput.enabled = true;

                talkToChar.transform.GetComponentInChildren<ChatZone>().disableIcon();
            }
        }
    }

    public void setNPC(NPC input)
    {
        talkToChar = input;
    }

    public void swipeNPC()
    {
        talkToChar = null;
    }

    /// <summary>
    /// All the event necessary at the end of each dialogue
    /// </summary>
    public void EndDialogue()
    {
        talkToChar.transform.GetComponentInChildren<ChatZone>().enableIcon();
    }
}
