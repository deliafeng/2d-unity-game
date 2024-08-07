using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    public string itemName;
    public Dialogue dialogue;
    //also include item sprite later

    public void CollectItem()
    {
        FindObjectOfType<CollectibleManager>().identifyItem(itemName);
        FindObjectOfType<dialogueManager>().StartDialogue(dialogue);
        Destroy(this.gameObject);
    }
}
