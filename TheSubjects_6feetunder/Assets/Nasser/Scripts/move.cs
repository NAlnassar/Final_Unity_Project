using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Photon.Pun;
using UnityEditor.Playables;
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

    bool ability_active = false;
    Vector2 xRotation = Vector2.zero;
    int ability = 0;

    Vector3 realposition = Vector3.zero;
    Quaternion realrotation = Quaternion.identity;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            realposition = (Vector3)stream.ReceiveNext();
            realrotation = (Quaternion)stream.ReceiveNext();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
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
        }
        else
        {
            Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"),
                0f, Input.GetAxis("Vertical"));
            if (Input.GetKeyDown(KeyCode.Q) && !ability_active)
            {
                ability_active = true;
                Ability.ability_activation(ability, gameObject, ref ability_prefabs[ability], ref active_ghost, ref Vircam3rd, ref Vircam1st);
            }
            else if (Input.GetKeyDown(KeyCode.Q) && ability_active)
            {
                ability_active = false;
                Ability.ability_deactivation(ability, gameObject, active_ghost, Vircam3rd, Vircam1st);
            }

            if (ability_active && ability==2)
            {
                xRotation.y += Input.GetAxis("Mouse X");
                xRotation.x += -Input.GetAxis("Mouse Y");
                xRotation.x = Mathf.Clamp(xRotation.x, -45f, 45f);
                active_ghost.transform.eulerAngles = xRotation * 5;
                active_ghost.GetComponent<Rigidbody>().velocity =
                    active_ghost.transform.TransformDirection(moveDirection) * 10f;
            }

            if (ability!= 2)
            {
                body.velocity = transform.TransformDirection(moveDirection) * 10f;
            }
        }
    }


}
