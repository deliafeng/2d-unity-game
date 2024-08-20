using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public Dialogue dialogue;

    public GameObject UI;
    public int sceneActivationNumber = -1;
    public int sceneDeactivationNumber = -1;

   

    public void TriggerDialogue()
    {

      FindObjectOfType<dialogueManager>().StartDialogue(dialogue);

    }

    public void TriggerScene()
    {

        if (UI != null) { 
            UI.SetActive(true);
        }
    }

    public void EndScene()
    {

        if (UI != null)
        {
            UI.SetActive(false);
        }
    }


}
