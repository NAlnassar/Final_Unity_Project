using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reset_char : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.SetInt("character", 0);
    }
}
