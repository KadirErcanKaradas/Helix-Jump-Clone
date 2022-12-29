using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameManager manager;
    private Transform target;
    private Vector3 offset;
    public GameObject player;
    private void Start()
    {
        manager = GameManager.Instance;
        target = player.transform;
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        SmoothFollow();
    }
    
    public void SmoothFollow()
    {
        if (manager.isFallen)
        {
            Vector3 targetPos = target.position + offset;
            transform.position = new Vector3(transform.position.x, targetPos.y, transform.position.z);
        }
    }
}