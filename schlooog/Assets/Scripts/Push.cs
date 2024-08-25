using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Push : MonoBehaviour
{

    public playerController pController;
    private Vector2 destination = new Vector2(0f,0f);
    public Transform movePoint;
    private bool touched = false;
    public bool blockedVert = false;
    public bool blockedHoriz = false;
    
    // Start is called before the first frame update
    void Start()
    {
        pController = FindObjectOfType<playerController>();
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        //If player is touching, MOVE
        if (touched) {

            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, pController.playerSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, movePoint.position) == 0f)
            {

                if (((Input.GetAxisRaw("Horizontal") == 1) && (transform.position.x>pController.transform.position.x)
                ||(Input.GetAxisRaw("Horizontal") == -1) && (transform.position.x<pController.transform.position.x)) &&transform.position.y==pController.transform.position.y){

                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * 32, 0f, 0f), .2f, pController.obstacle))
                    {
                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * 32, 0f, 0f);
                        blockedHoriz = false;

                    }

                }

                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {

                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * 32, 0f), .2f, pController.obstacle))
                    {
                        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * 32, 0f);
                        blockedVert = false;
                    }
                }
                else
                {
                    movePoint.position += new Vector3(0f, 0f, 0f);
                }
            }
        }    
//IF BLOCKED, SAY SO
        if (Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * 32, 0f, 0f), .2f, pController.obstacle))
        {
            blockedHoriz = true;
        }

        else if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * 32, 0f), .2f, pController.obstacle))
        {
            blockedVert = true;
        }        
        else {
            blockedHoriz = false;
            blockedVert = false;
        }
    }

//Check if touched by Player
    void OnCollisionEnter2D(Collision2D collider) { 
        Debug.Log("Collided");
        if(collider.gameObject.CompareTag("Player")) {
            touched = true;
        }   
    }
    void OnCollisionExit2D(Collision2D collider) { 
        Debug.Log("Collided");
        if(collider.gameObject.CompareTag("Player")) {
            touched = false;

        }   
    }
}


