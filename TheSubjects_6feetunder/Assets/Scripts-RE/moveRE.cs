using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveRE : MonoBehaviour
{
    private float speed = 1f;
    private Rigidbody body;
    public float jumpForce = 2f;
    private bool isGrounded = false;
    private bool isOnLadder = false;
    private bool canGrabPotion = false; 
    public Animator anim;
    public Transform handTransform; 
    public GameObject objectToPickUp; 
    public float rotationSpeed = 90f; 
    public GameObject newObjectPrefab; 
    public Transform instantiatePosition; 

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        if (isOnLadder)
        {
            Vector3 ladderMoveDirection = new Vector3(horizontalMovement, verticalMovement, 0f);
            //body.MovePosition(transform.position + ladderMoveDirection * speed * Time.deltaTime);
            anim.SetBool("Climb", true);
        }
        else
        {
            Vector3 moveDirection = transform.TransformDirection(new Vector3(horizontalMovement, 0f, verticalMovement));

            if (verticalMovement != 0 && Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("Run", true);
                speed = 5;
            }
            else
            {
                anim.SetBool("Run", false);
                speed = 1;
            }
            if (verticalMovement != 0 && Input.GetKey(KeyCode.Space))
            {
                //anim.SetTrigger("WalkJump");
                anim.SetBool("WalkJump", true);
            }
            if (verticalMovement != 0 && Input.GetKey(KeyCode.Space))
            {
                // anim.SetTrigger("RunJump");
                anim.SetBool("RunJump", true);
            }
            if (Input.GetKey(KeyCode.C))
            {
                anim.SetBool("Crouch", true);
            }
            else
            {
                anim.SetBool("Crouch", false);

            }

            //body.MovePosition(transform.position + moveDirection * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump"))
            {
                if (isGrounded)
                {
                    Jump();
                    anim.SetBool("Jump", true);
                }
            }

            anim.SetFloat("Horizontal", horizontalMovement);
            anim.SetFloat("Vertical", verticalMovement);


           
           
        }

        if (canGrabPotion && Input.GetKeyDown(KeyCode.E))
        {
            GrabPotion();
        }
    }

    void Jump()
    {
        body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("Jump", false);
            anim.SetBool("WalkJump", false);
            anim.SetBool("RunJump", false);
        }
        if (collision.gameObject.CompareTag("Ladder"))
        {
            isOnLadder = true;
            anim.SetBool("Climb", true);
            body.useGravity = false;
            body.velocity = Vector3.zero;
        }
        if (collision.gameObject.CompareTag("Bar"))
        {
            canGrabPotion = true;

            StartCoroutine(DrinkRoutine());
        }

    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            // anim.SetBool("Jump", false);
        }

        if (collision.gameObject.CompareTag("Ladder"))
        {
            isOnLadder = false;
            anim.SetBool("Climb", false);
            body.useGravity = true;
        }

        if (collision.gameObject.CompareTag("Bar"))
        {
            canGrabPotion = false;
        }
    }

    void GrabPotion()
    {
        if (objectToPickUp != null)
        {
            objectToPickUp.transform.SetParent(handTransform);
            objectToPickUp.transform.localPosition = Vector3.zero; // Adjust as necessary
                                                                   // objectToPickUp.GetComponent<Rigidbody>().isKinematic = true; // Make the potion kinematic so it doesn't fall
            anim.SetBool("Drink", true);
        }
    }

    IEnumerator DrinkRoutine()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + 1);

        ReleaseObject();

        GameObject newObject = Instantiate(newObjectPrefab, instantiatePosition.position, instantiatePosition.rotation);
        newObject.transform.SetParent(handTransform);
        newObject.transform.localPosition = Vector3.zero;

        yield return new WaitForSeconds(5);
        Destroy(newObject);
    }

    void ReleaseObject()
    {
        if (objectToPickUp != null)
        {
            objectToPickUp.transform.SetParent(null);
            Destroy(objectToPickUp);
        }
    }
}
