using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMPlayerFollow : MonoBehaviour
{
    private void Start()
    {
        GetComponent<CinemachineVirtualCamera>().Follow = FindObjectOfType<PlayerMovement>().transform;
    }
}
