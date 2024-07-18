using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

public class move : MonoBehaviour, IPunObservable
{

    public Rigidbody body;
    [SerializeField] GameObject Vircam3rd;
    [SerializeField] GameObject Vircam1st;
    public GameObject cam;
    public GameObject[] ability_prefabs = new GameObject[6];
    PhotonView view;
    GameObject active_ghost;
    public Animator anim;
    float speed = 5f;
    public bool ability_active = false;
    Vector2 xRotation = Vector2.zero;
    public int ability = -1;
    float turnSmoothVelocity;
    Vector3 realposition = Vector3.zero;
    Quaternion realrotation = Quaternion.identity;
    float velocity_ = 0f;
    float gravity_mul = 0.5f;

    float ApplyGravity()
    {
        if (body.isGrounded)
        {
            velocity_ = -1;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity_ += 2.5f;
            }
        }
        else
        {
            velocity_ += -9.81f * gravity_mul * Time.deltaTime;
        }
        return velocity_;

    }


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        view = GetComponent<PhotonView>();
        body = GetComponent<Rigidbody>();
        if (view.IsMine)
        {
            cam.SetActive(true);
            Vircam3rd.SetActive(true);
            Vircam1st.SetActive(true);
            cam.transform.SetParent(null);
            Vircam3rd.transform.SetParent(null);
            Vircam1st.transform.SetParent(null);
            Vircam3rd.GetComponent<CinemachineFreeLook>().Priority = 10;
            Vircam1st.GetComponent<CinemachineVirtualCamera>().Priority = 2;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!view.IsMine)
        {
            transform.position = realposition;
            transform.rotation = realrotation;
            //Anim Logic
        }
        else
        {
            Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"),
                0f, Input.GetAxis("Vertical"));
            if (Input.GetKeyDown(KeyCode.Q) && !ability_active)
            {
                if (ability != -1)
                {
                    ability_active = true;
                    Ability.ability_activation(ability, gameObject, ref ability_prefabs[ability], ref active_ghost, ref Vircam3rd, ref Vircam1st);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Q) && ability_active && ability!=2)
            {
                ability_active = false;
                Ability.ability_deactivation(ability, gameObject, active_ghost, Vircam3rd, Vircam1st);
            }

            if (ability_active)
            {
                if (active_ghost.TryGetComponent(out CharacterController body))
                {
                    if(Vircam1st.GetComponent<CinemachineVirtualCamera>().Priority == 10)
                    {
                        //if first person ability
                        
                        xRotation.y += Input.GetAxisRaw("Mouse X");
                        xRotation.x += -Input.GetAxisRaw("Mouse Y");
                        xRotation.x = Mathf.Clamp(xRotation.x, -30f, 30f);
                        if (xRotation != Vector2.zero)
                        {
                            active_ghost.transform.eulerAngles = xRotation * 5;
                        }

                        Vector3 dir = active_ghost.transform.forward * Input.GetAxis("Vertical") + active_ghost.transform.right * Input.GetAxis("Horizontal");
                        FirstPersonMove(dir, body);

                            
                    }
                    else
                    {
                        //if Third person ability
                        MoveCharacter(moveDirection, active_ghost.GetComponent<CharacterController>());
                    }
                    active_ghost.GetComponent<Rigidbody>().velocity =
                        active_ghost.transform.TransformDirection(moveDirection) * 10f;
                }
                else
                {
                    xRotation.y += Input.GetAxis("Mouse X");
                    xRotation.x += -Input.GetAxis("Mouse Y");
                    xRotation.x = Mathf.Clamp(xRotation.x, -10f, 10f);
                    if(xRotation!= Vector2.zero)
                    {
                        Vircam1st.transform.rotation = Quaternion.Euler(xRotation.x, xRotation.y, 0f);
                        transform.rotation = Quaternion.Euler(0f, xRotation.y, 0f);
                    }
                    Vector3 dir = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
                    FirstPersonMove(dir, this.body);
                }

            }

            else
            {
                MoveCharacter(moveDirection, body);
            }
        }
    }

    void MoveCharacter(Vector3 moveDirection, CharacterController body)
    {
        if (moveDirection.magnitude > 0.1f)
        {
            float targetAngle = (Mathf.Atan2(moveDirection.x, moveDirection.z)
                * Mathf.Rad2Deg) + cam.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.rotation.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 0.1f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //moveDirection = Quaternion.AngleAxis(cam.transform.rotation.eulerAngles.y, Vector3.up) * moveDirection;
            //moveDirection.Normalize();

        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 7f;
        }
        else
        {
            speed = 5f;
        }
        // Debug.Log("Speed: " + speed);
        moveDirection.y = ApplyGravity();
        body.Move(moveDirection * speed * Time.deltaTime);
    }

    void FirstPersonMove(Vector3 moveDir, CharacterController body)
    {
        body.Move(moveDir * speed * Time.deltaTime);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(anim.GetFloat(anim.GetParameter(0).name));
            stream.SendNext(anim.GetFloat(anim.GetParameter(1).name));
            stream.SendNext(anim.GetFloat(anim.GetParameter(2).name));
            stream.SendNext(anim.GetBool(anim.GetParameter(3).name));
            stream.SendNext(anim.GetBool(anim.GetParameter(4).name));
            stream.SendNext(anim.GetBool(anim.GetParameter(5).name));
            stream.SendNext(anim.GetBool(anim.GetParameter(6).name));
            stream.SendNext(anim.GetBool(anim.GetParameter(7).name));
            stream.SendNext(anim.GetBool(anim.GetParameter(8).name));
            stream.SendNext(anim.GetBool(anim.GetParameter(9).name));
            stream.SendNext(anim.GetBool(anim.GetParameter(10).name));
            stream.SendNext(anim.GetBool(anim.GetParameter(11).name));
            stream.SendNext(anim.GetBool(anim.GetParameter(12).name));
            stream.SendNext(anim.GetBool(anim.GetParameter(13).name));


        }
        else
        {
            //Fall-- 
            //Drink--
            //Ghost--
            //Throw
            //Happy
            //WalkJump
            //RunJump
            realposition = (Vector3)stream.ReceiveNext();
            realrotation = (Quaternion)stream.ReceiveNext();
            anim.SetFloat(anim.GetParameter(0).name, (float)stream.ReceiveNext());//Hori
            anim.SetFloat(anim.GetParameter(1).name, (float)stream.ReceiveNext());//Verti
            anim.SetFloat(anim.GetParameter(2).name, (float)stream.ReceiveNext());//Blend
            anim.SetBool(anim.GetParameter(3).name, (bool)stream.ReceiveNext());//Run
            anim.SetBool(anim.GetParameter(4).name, (bool)stream.ReceiveNext()); //Jump
            anim.SetBool(anim.GetParameter(5).name, (bool)stream.ReceiveNext());//Fall
            anim.SetBool(anim.GetParameter(6).name, (bool)stream.ReceiveNext());//Climb
            anim.SetBool(anim.GetParameter(7).name, (bool)stream.ReceiveNext());//Drink
            anim.SetBool(anim.GetParameter(8).name, (bool)stream.ReceiveNext());//Ghost
            anim.SetBool(anim.GetParameter(9).name, (bool)stream.ReceiveNext());
            anim.SetBool(anim.GetParameter(10).name, (bool)stream.ReceiveNext());
            anim.SetBool(anim.GetParameter(11).name, (bool)stream.ReceiveNext());
            anim.SetBool(anim.GetParameter(12).name, (bool)stream.ReceiveNext());
            anim.SetBool(anim.GetParameter(13).name, (bool)stream.ReceiveNext());


        }
    }

}
