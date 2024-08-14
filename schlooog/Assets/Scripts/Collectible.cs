using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    public Item item;
    public Dialogue dialogue;
    public Inventory inventory;
    //also include item sprite later

    public void CollectItem()
    {
        inventory.AddItem(item);
        FindObjectOfType<dialogueManager>().StartDialogue(dialogue);
        Destroy(this.gameObject);
    }
}
