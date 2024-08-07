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

        public void TeleportToScene() { 
            Debug.Log("triggered");
            if(this.gameObject.CompareTag("Player")) {
                SceneManager.LoadScene(nextSceneName);
            }
        }

}
