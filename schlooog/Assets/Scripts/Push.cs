using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Push : MonoBehaviour
{

    public playerController pController;
    private Vector2 destination = new Vector2(0f,0f);
    public Transform movePoint;
    private bool touched = false;
    public float blockedVertDir = 0;
    public float blockedHorizDir = 0;
    public Transform targetPosition;
    public bool targetReached;
    public Dialogue positionedDialogue;
    
    // Start is called before the first frame update
    void Start()
    {
        pController = FindObjectOfType<playerController>();
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        //If player is touching
        if (touched) {

            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, pController.playerSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, movePoint.position) == 0f)
            {
                //If on same Y level:
                if (transform.position.y==pController.transform.position.y)
                {
                    //If blocked, mark direction (horizontal)
                    if (Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * 32, 0f, 0f), .2f, pController.obstacle))
                    {
                        blockedHorizDir = Input.GetAxisRaw("Horizontal");
                        Debug.Log("blocked horiz" + blockedHorizDir);
                    }
                    //check horizontal pushing
                    if ((Input.GetAxisRaw("Horizontal") == 1) && (transform.position.x>pController.transform.position.x)
                    ||(Input.GetAxisRaw("Horizontal") == -1) && (transform.position.x<pController.transform.position.x))
                    {
                        //if not blocked, move (can clean up code but im lazy)
                        if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * 32, 0f, 0f), .2f, pController.obstacle))
                        {
                            movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * 32, 0f, 0f);
                            
                        }
                    }
                
                    
                }
                //If X axis same, check same things but vertically
                if (transform.position.x==pController.transform.position.x)
                {
                    //If blocked vertically, mark as such
                    Debug.Log("y axis detected");
                    if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * 32, 0f), .2f, pController.obstacle))
                    {
                        blockedVertDir = Input.GetAxisRaw("Vertical");
                        Debug.Log("blocked vert" + blockedVertDir);
                    }
                    //If aligned, move
                    if ((Input.GetAxisRaw("Vertical") == 1) && (transform.position.y>pController.transform.position.y)
                    ||(Input.GetAxisRaw("Vertical") == -1) && (transform.position.y<pController.transform.position.y))
                    {
                        // can clean up but im lazy
                        if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * 32, 0f), .2f, pController.obstacle))
                        {
                            movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * 32, 0f);
                            
                        }
                    }

                                    
                }
                
                else
                {
                    movePoint.position += new Vector3(0f, 0f, 0f);
                }
            }
        }   

        //If not in line, not blocked
        if (transform.position.x!=pController.transform.position.x) 
        {
            blockedVertDir = 0;
        }
        if (transform.position.y!=pController.transform.position.y) 
        {
            blockedHorizDir = 0;
        }
        if (transform == targetPosition)
        {
            targetReached = true;
        }
        else
        {
            targetReached=false;
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
        Debug.Log("Exited");
        if(collider.gameObject.CompareTag("Player")) {
            touched = false;
            blockedHorizDir = 0;
            blockedVertDir = 0;

        }   
    }
}


