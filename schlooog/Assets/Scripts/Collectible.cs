using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    public Item item;
    public Item item2;
    public Dialogue dialogue;

    //also include item sprite later

    public void CollectItem()
    {
        FindObjectOfType<dialogueManager>().StartDialogue(dialogue);
        Destroy(this.gameObject);
    }
}
