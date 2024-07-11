using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class move : MonoBehaviour, IPunObservable
{

    Rigidbody body;
    [SerializeField] GameObject Vircam3rd;
    [SerializeField] GameObject Vircam1st;
    public GameObject cam;
    public GameObject[] ability_prefabs = new GameObject[6];

    PhotonView view;
    GameObject active_ghost;
    Animator anim;
    bool ability_active = false;
    Vector2 xRotation = Vector2.zero;
    public int ability = -1;

    Vector3 realposition = Vector3.zero;
    Quaternion realrotation = Quaternion.identity;
    

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
            anim.SetBool(anim.GetParameter(5).name, (bool) stream.ReceiveNext());//Fall
            anim.SetBool(anim.GetParameter(6).name, (bool)stream.ReceiveNext());//Climb
            anim.SetBool(anim.GetParameter(7).name, (bool) stream.ReceiveNext());//Drink
            anim.SetBool(anim.GetParameter(8).name, (bool) stream.ReceiveNext());//Ghost
            anim.SetBool(anim.GetParameter(9).name, (bool)stream.ReceiveNext());
            anim.SetBool(anim.GetParameter(10).name, (bool)stream.ReceiveNext());
            anim.SetBool(anim.GetParameter(11).name, (bool)stream.ReceiveNext());
            anim.SetBool(anim.GetParameter(12).name, (bool)stream.ReceiveNext());
            anim.SetBool(anim.GetParameter(13).name, (bool)stream.ReceiveNext());


        }
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
            Vircam3rd.GetComponent<CinemachineVirtualCamera>().Priority = 10;
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
            else if (Input.GetKeyDown(KeyCode.Q) && ability_active)
            {
                ability_active = false;
                Ability.ability_deactivation(ability, gameObject, active_ghost, Vircam3rd, Vircam1st);
            }

            if (ability_active)
            {
                if (active_ghost.TryGetComponent(out Rigidbody body))
                {

                    xRotation.y += Input.GetAxis("Mouse X");
                    xRotation.x += -Input.GetAxis("Mouse Y");
                    xRotation.x = Mathf.Clamp(xRotation.x, -7f, 7f);
                    active_ghost.transform.eulerAngles = xRotation * 5;
                    active_ghost.GetComponent<Rigidbody>().velocity =
                        active_ghost.transform.TransformDirection(moveDirection) * 10f;
                }
                else
                {
                    xRotation.y += Input.GetAxis("Mouse X");
                    xRotation.x += -Input.GetAxis("Mouse Y");
                    xRotation.x = Mathf.Clamp(xRotation.x, -45f, 45f);
                    transform.eulerAngles = xRotation * 5;
                    this.body.velocity = transform.TransformDirection(moveDirection) * 10f;
                }

            }

            else
            {

                body.velocity = cam.transform.TransformDirection(moveDirection) * 10f;
            }
        }
    }


}
