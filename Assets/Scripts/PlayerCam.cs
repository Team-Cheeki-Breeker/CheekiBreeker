using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCam : MonoBehaviour
{
    private CinemachineVirtualCamera vCamera;
    private CinemachineConfiner2D confiner;

    // Finds player and sets them up as the camera's target
    // Finds boundary area and sets them up as the camera's bounding
    void Start()
    {
        vCamera = GetComponent<CinemachineVirtualCamera>();
        vCamera.Follow = GameObject.FindGameObjectWithTag("Player").transform;
        vCamera.LookAt = vCamera.Follow;
        confiner = vCamera.GetComponentInChildren<CinemachineConfiner2D>();
        confiner.m_BoundingShape2D = GameObject.FindGameObjectWithTag("Boundary").GetComponent<PolygonCollider2D>();
    }

}
