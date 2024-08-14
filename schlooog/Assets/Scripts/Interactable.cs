using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public Dialogue dialogue;


    //Boolean to ensure conversation can't be called again when X is being pressed

   

    public void TriggerDialogue()
    {
      FindObjectOfType<dialogueManager>().StartDialogue(dialogue);

    }
}
