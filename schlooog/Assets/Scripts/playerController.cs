using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float playerSpeed = 7f;
    public Transform movePoint;

    public LayerMask obstacle;
    public LayerMask interactable;


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
     
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, obstacle)) {
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

        //Shift to Sprint
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerSpeed *= 1.2f;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerSpeed = 7f;
        }

        //Interactable
        


    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Interactable"))
        {

            Debug.Log("collided");
        }
    }
}
