using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerController : MonoBehaviour
{
    //inventorytoggle just toggles on and off inventory upon load
    public GameObject InventoryToggle;
    [SerializeField] private UIInventory uiInventory;
    public float playerSpeed = 128f;
    private float WalkSpeed;
    public Transform movePoint;

    public LayerMask obstacle;
    public LayerMask teleporter;
    public int direction = 4;
    private Ray ray;

    public bool inDialogue = false;

    public dialogueManager dialogueManager;

    public Animator inventoryAnimator;
    private bool isInventoryOpen = false;
    private Animator playerAnimation;
    private RaycastHit2D hit;
    private Inventory inventory;
    public Push p;

    // Start is called before the first frame update
    void Start()
    {
        WalkSpeed = playerSpeed; 
        movePoint.parent = null;
        playerAnimation = this.gameObject.GetComponent<Animator>();

        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
        InventoryToggle.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        //All actions outside of Dialogue
        if (!inDialogue)
        {
            //Grid Movement
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, playerSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, movePoint.position) == 0f)
            {

                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {

                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * 32, 0f, 0f), .2f, obstacle)&&Input.GetAxisRaw("Horizontal")!=p.blockedHorizDir)
                    {
                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * 32, 0f, 0f);

                    }

                }

                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {

                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * 32, 0f), .2f, obstacle)&&Input.GetAxisRaw("Vertical")!=p.blockedVertDir)
                    {
                        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * 32, 0f);

                    }
                }
                else
                {
                    movePoint.position += new Vector3(0f, 0f, 0f);
                }
            }

            //Animations
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
                    inventoryAnimator.SetBool("isOpen", true);
                    isInventoryOpen = true;
                } else
                {
                    inventoryAnimator.SetBool("isOpen", false);
                    isInventoryOpen = false;
                    uiInventory.itemName.gameObject.SetActive(false);
                    uiInventory.itemDesc.gameObject.SetActive(false);
                }
            }
        }


        //Shift to Sprint
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerSpeed *= 1.5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerSpeed = WalkSpeed;
        }

        //Interactable and collectible detection
        //Shoot a ray and check the tag of the object if it is hit
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!inDialogue)
            {
                if (direction == 0)
                {
                    hit = Physics2D.Raycast(transform.position, transform.up, 32f);
                    detectObject();
                }

                else if (direction == 1)
                {
                    hit = Physics2D.Raycast(transform.position, transform.right, 32f);
                    detectObject();

                }
                else if (direction == 2)
                {
                    hit = Physics2D.Raycast(transform.position, -transform.up, 32f);
                    detectObject();
                }
                else if (direction == 3)
                {
                    hit = Physics2D.Raycast(transform.position, -transform.right, 32f);
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

    //Detect tags
    private void detectObject()
    {
        if (hit && hit.collider.CompareTag("Interactable"))
        {
            dialogueManager.interactable = hit.collider.gameObject.GetComponent<Interactable>();


            hit.collider.GetComponent<Interactable>().TriggerDialogue();
            

        }
        else if (hit && hit.collider.CompareTag("Collectible"))
        {
            hit.collider.GetComponent<Collectible>().CollectItem();
            if (hit.collider.GetComponent<Collectible>().item.amount>0) {
                inventory.AddItem(hit.collider.GetComponent<Collectible>().item);
            }
        
            if (hit.collider.GetComponent<Collectible>().item2.amount>0) {
                inventory.AddItem(hit.collider.GetComponent<Collectible>().item2);
            }
        }
            
         else if (hit && hit.collider.CompareTag("Unlockable"))
        {
            bool unlocked = false;
            Item key = hit.collider.GetComponent<Unlockable>().keyItem;  
            List<Item> list = inventory.GetItemList();

            Item usedItem = null;

            foreach (Item item in list)
            {
                if (item.itemType == key.itemType)
                {
                    unlocked = true;
                    usedItem = item;
                }
            }   

            if (unlocked)
            {
                inventory.RemoveItem(usedItem);
                hit.collider.GetComponent<Unlockable>().UnlockedDialogue();
                if (hit.collider.gameObject.GetComponent<Unlockable>().hasItem)
                {
                    inventory.AddItem(hit.collider.GetComponent<Unlockable>().containedItem);
                }
            }
            else
            {
                hit.collider.GetComponent<Unlockable>().LockedDialogue();
            }
        }
        else if (hit&&hit.collider.CompareTag("ObjToPush"))
        {
            if (p.targetReached)
            {
                hit.collider.GetComponent<Collectible>().CollectItem();
                inventory.AddItem(hit.collider.GetComponent<Collectible>().item);
            }
        }
        
    }
}


