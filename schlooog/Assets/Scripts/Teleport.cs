using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public string nextSceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    void OnCollisionEnter2D(Collision2D collider) { 

            if(collider.gameObject.CompareTag("Player")) {
                SceneManager.LoadScene(nextSceneName);
            }
    }

}
