using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;


    [SerializeField] public AudioClip DoorOpen;
    [SerializeField] public AudioClip FemaleInjury;
    [SerializeField] public AudioClip MaleInjury;


    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.AudioClip AM = audioSource.clip;
    }

   


    public void PlayDoorOpen()
    {
        audioSource.PlayOneShot(DoorOpen);
    }
    public void PlayFemaleInjury()
    {
        audioSource.PlayOneShot(FemaleInjury);
    }
    public void PlayMaleInjury()
    {
        audioSource.PlayOneShot(MaleInjury);
    }


}
