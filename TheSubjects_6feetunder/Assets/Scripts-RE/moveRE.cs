using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveRE : MonoBehaviour
{
    private float speed = 1f;
    private Rigidbody body;
    public float jumpForce = 2f;
    private bool isGrounded = false;
    private bool canDoubleJump = false;
    private bool isOnLadder = false;
    private bool canGrabPotion = false; // Can grab potion flag
    public Animator anim;
    public Transform handTransform; // The transform of the character's hand
    public GameObject objectToPickUp; // The object to pick up
    public float rotationSpeed = 90f; // Degrees per second
    public GameObject newObjectPrefab; // The prefab for the new object
    public Transform instantiatePosition; // The position where the new object will be instantiated

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
            body.MovePosition(transform.position + ladderMoveDirection * speed * Time.deltaTime);
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
                anim.SetTrigger("WalkJump");
            }
            if (verticalMovement != 0 && Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetTrigger("RunJump");
            }

            body.MovePosition(transform.position + moveDirection * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump"))
            {
                if (isGrounded)
                {
                    Jump();
                    canDoubleJump = true;
                }
                else if (canDoubleJump)
                {
                    Jump();
                    canDoubleJump = false;
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
            anim.SetTrigger("Drink");
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
