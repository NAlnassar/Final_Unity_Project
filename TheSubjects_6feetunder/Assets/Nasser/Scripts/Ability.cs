using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public static void ability_activation(int ability,
        GameObject player, ref GameObject prefab,
        ref GameObject active_ghost, ref GameObject Vircam3rd,
        ref GameObject Vircam1st)
    {
        switch (ability)
        {
            case 0:
                {
                    active_ghost = Instantiate(prefab, player.GetComponent<move>().cam.transform.position
                    , player.GetComponent<move>().cam.transform.rotation);
                    active_ghost.GetComponent<follow_cam>().SetFollowTarget(player.GetComponent<move>().cam.transform);
                    Vircam1st.transform.position = player.transform.position;
                    Vircam1st.transform.rotation = player.transform.rotation;
                    Vircam1st.transform.SetParent(player.transform);
                    Vircam1st.GetComponent<CinemachineVirtualCamera>().Priority = 10;
                    Vircam3rd.GetComponent<CinemachineVirtualCamera>().Priority = 2;
                    break;
                }
            case 1:
                {
                    //Ghost vision logic
                    break;
                }
            case 2:
                {
                    active_ghost = Instantiate(prefab, player.transform.position + player.transform.forward
                    , Quaternion.identity);
                    active_ghost.SetActive(true);
                    Vircam1st.transform.position = active_ghost.transform.position;
                    Vircam1st.transform.rotation = active_ghost.transform.rotation;
                    Vircam1st.transform.SetParent(active_ghost.transform);
                    Vircam1st.GetComponent<CinemachineVirtualCamera>().Priority = 10;
                    Vircam3rd.GetComponent<CinemachineVirtualCamera>().Priority = 2;
                    break;
                }
            case 3:
                {
                    break;
                }
            case 4:
                {
                    break;
                }
            case 5:
                {
                    break;
                }

            default:
                {
                    break;
                }
        }


    }

    public static void ability_deactivation(int ability,
        GameObject player, GameObject active_ghost, GameObject Vircam3rd,
    GameObject Vircam1st)
    {

        switch (ability)
        {
            case 0:
                {
                    Vircam1st.transform.SetParent(null);
                    Vircam3rd.GetComponent<CinemachineVirtualCamera>().Priority = 10;
                    Vircam1st.GetComponent<CinemachineVirtualCamera>().Priority = 2;
                    Destroy(active_ghost);
                    break;
                }
            case 1:
                {
                    //Ghost vision logic
                    break;
                }
            case 2:
                {
                    Vircam1st.transform.SetParent(null);
                    Vircam3rd.GetComponent<CinemachineVirtualCamera>().Priority = 10;
                    Vircam1st.GetComponent<CinemachineVirtualCamera>().Priority = 2;
                    Destroy(active_ghost);
                    break;
                }
            case 3:
                {
                    break;
                }
            case 4:
                {
                    break;
                }
            case 5:
                {
                    break;
                }

            default:
                {
                    break;
                }
        }
    }
}
