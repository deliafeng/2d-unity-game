using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public string nextSceneName;
    public GameObject stool;
    public Data data;

    // Start is called before the first frame update
    void Start()
    {
        if (Data.StoolInHallway)
        {
            stool.SetActive(true);
        }
    }

    // Update is called once per frame

    void OnCollisionEnter2D(Collision2D collider) { 
    
            if(collider.gameObject.CompareTag("Player")) 
            {
                SceneManager.LoadScene(nextSceneName);
            }
            if(collider.gameObject.CompareTag("ObjToPush"))
            {
                collider.gameObject.SetActive(false);
                Data.StoolInHallway = true;
            }

    }


}
