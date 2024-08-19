using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{

    private string name;


    //method declares item as being held by the user (things can be interacted with later on) and
    //inventory will be updated with name and sprite (prob need an array)

    public void identifyItem(string name)
    {
        this.name = name;
    }

}
