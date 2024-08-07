using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    public int item;
    public string itemName;
    //also include item sprite later

    public void collectItem()
    {
        FindObjectOfType<CollectibleManager>().identifyItem(item, itemName);
    }
}
