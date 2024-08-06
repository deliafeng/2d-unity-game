using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerController : MonoBehaviour
{
    public float playerSpeed = 7f;
    public Transform movePoint;

    public LayerMask obstacle;

    private int direction = 4;

    private Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        //Grid movement

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, playerSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) == 0f)
        {

            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {

                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, obstacle))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);

                }

            }

            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {

                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, obstacle))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);

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
        }
        if (Input.GetAxisRaw("Horizontal") == 1f)
        {
            direction = 1;
        }
        if (Input.GetAxisRaw("Vertical") == -1f)
        {
            direction = 2;
        }
        if (Input.GetAxisRaw("Horizontal") == -1f)
        {
            direction = 3;
        }



        //Shift to Sprint
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerSpeed *= 1.4f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerSpeed = 7f;
        }

        //Interactable
        if (Input.GetKeyDown(KeyCode.X))
        {

            if (direction == 0)
            {

                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 1f);
                if (hit.collider.CompareTag("Interactable")) {
                    hit.collider.GetComponent<Interactable>().TriggerDialogue();
                }

            }

            else if (direction == 1)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1f);
                if (hit.collider.CompareTag("Interactable"))
                {
                    hit.collider.GetComponent<Interactable>().TriggerDialogue();
                }

            }
            else if (direction == 2)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 1f);
                if (hit.collider.CompareTag("Interactable"))
                {
                    hit.collider.GetComponent<Interactable>().TriggerDialogue();
                }

            }
            else if (direction == 3)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.right, 1f);
                if (hit.collider.CompareTag("Interactable"))
                {
                    hit.collider.GetComponent<Interactable>().TriggerDialogue();
                }

            }

        }



    }

}

