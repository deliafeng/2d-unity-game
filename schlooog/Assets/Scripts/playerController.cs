using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerController : MonoBehaviour
{
    public float playerSpeed = 128f;
    public Transform movePoint;

    public LayerMask obstacle;

    private int direction = 4;
    private Ray ray;

    public bool inDialogue = false;

    public Animator inventory;
    private bool isInventoryOpen = false;
    private Animator playerAnimation;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        playerAnimation = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Grid movement
        if (!inDialogue)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, playerSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, movePoint.position) == 0f)
            {

                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {

                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * 32, 0f, 0f), .2f, obstacle))
                    {
                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * 32, 0f, 0f);

                    }

                }

                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {

                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * 32, 0f), .2f, obstacle))
                    {
                        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * 32, 0f);

                    }
                }
                else
                {
                    movePoint.position += new Vector3(0f, 0f, 0f);
                }
            }

            //Direction the character is facing
            //0 is up, 1 is right, 2 is down, 3 is left

            if (Input.GetAxisRaw("Vertical") == 1f)
            {
                direction = 0;
                SetAnimation("isGoingUp");
                playerAnimation.Play("PickleUp");

            }
            if (Input.GetAxisRaw("Horizontal") == 1f)
            {
                direction = 1;
                SetAnimation("isGoingRight");
                playerAnimation.Play("PickleRight");
            }
            if (Input.GetAxisRaw("Vertical") == -1f)
            {
                direction = 2;
                SetAnimation("isGoingDown");
                playerAnimation.Play("PickleDown");
            }
            if (Input.GetAxisRaw("Horizontal") == -1f)
            {
                direction = 3;
                SetAnimation("isGoingLeft");
                playerAnimation.Play("PickleLeft");
            }

            if(Input.GetAxisRaw("Vertical") == 0f && Input.GetAxisRaw("Horizontal")==0f)
            {

               if(direction == 0) {
                playerAnimation.Play("PickleUp_Idle");
            }
                else if(direction==1) {
                playerAnimation.Play("PickleRight_Idle");
               }
               else if (direction == 2) {
                playerAnimation.Play("PickleDown_Idle");
               } else {
                playerAnimation.Play("PickleLeft_Idle");
               }


            }


            //Open inventory

            if (Input.GetKeyDown(KeyCode.E))
            {

                if (!isInventoryOpen)
                {
                    inventory.SetBool("isOpen", true);
                    isInventoryOpen = true;
                } else
                {
                    inventory.SetBool("isOpen", false);
                    isInventoryOpen = false;
                }
            }
        }


        //Shift to Sprint
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerSpeed *= 1.4f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerSpeed = 128f;
        }

        //Interactable and collectible detection
        //Shoot a ray and check the tag of the object if it is hit
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!inDialogue)
            {
                if (direction == 0)
                {
                    detectObject();
                }

                else if (direction == 1)
                {
                    detectObject();

                }
                else if (direction == 2)
                {
                    detectObject();
                }
                else if (direction == 3)
                {
                    detectObject();
                }
            }

            if (inDialogue)
            {

                FindObjectOfType<dialogueManager>().DisplayNextSentence();

            }

        }

    }

    //Set the current animation into the direction of movement
    private void SetAnimation(string currentAnimation)
    {
        playerAnimation.SetBool("isMoving", true);
        playerAnimation.SetBool("isGoingUp", false);
        playerAnimation.SetBool("isGoingDown", false);
        playerAnimation.SetBool("isGoingLeft", false);
        playerAnimation.SetBool("isGoingRight", false);
        playerAnimation.SetBool(currentAnimation, true);

    }

    //Shoot a ray
    private void detectObject()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 32f);
        if (hit && hit.collider.CompareTag("Interactable"))
        {
            hit.collider.GetComponent<Interactable>().TriggerDialogue();
        }
        else if (hit && hit.collider.CompareTag("Collectible"))
        {
            hit.collider.GetComponent<Collectible>().CollectItem();
        }
    }
}

