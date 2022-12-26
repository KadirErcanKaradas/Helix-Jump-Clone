using System;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private InputManager input;
    private GameManager manager;
    private Quaternion rotationY;
    public float speed = 0.1f;

    private void Start()
    {
        input = InputManager.Instance;
        manager = GameManager.Instance;
    }

    private void Update()
    {
        if(manager.GameStage == GameStage.Started)
            RotateCylinder();
    }

    private void RotateCylinder()
    {
        Vector3 pos = input.MoveFactor;
        rotationY = Quaternion.Euler(0, -pos.x * speed, 0f);
        transform.rotation = rotationY * transform.rotation;
    }
}
