using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public Dialogue dialogue;
    //Boolean to ensure conversation can't be called again when X is being pressed
    private bool isActivated = false;
   

    public void TriggerDialogue()
    {
        if (!isActivated)
        {
            FindObjectOfType<dialogueManager>().StartDialogue(dialogue);
            isActivated = true;
        }
    }

    private void Update()
    {
        if (isActivated)
        {
            if(Input.GetKeyDown(KeyCode.X))
            {
                FindObjectOfType<dialogueManager>().DisplayNextSentence();
            }
        }
    }

}
