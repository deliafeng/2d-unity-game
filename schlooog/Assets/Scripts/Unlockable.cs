using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlockable : MonoBehaviour
{
    public Item keyItem;

    public Dialogue dialogue_locked;
    public Dialogue dialogue_unlocked;
    public bool isDoor;
    public bool hasItem;
    public Item containedItem;

    public void LockedDialogue()
    {
        FindObjectOfType<dialogueManager>().StartDialogue(dialogue_locked);
    }

    public void UnlockedDialogue()
    {
        FindObjectOfType<dialogueManager>().StartDialogue(dialogue_unlocked);
    }

}
