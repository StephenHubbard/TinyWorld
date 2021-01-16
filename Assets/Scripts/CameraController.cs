using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject target;
    public float xSpeed = 3.5f;
    float sensitivity = 17f;

    float minFov = 10;
    float maxFov = 100;

    private void Start()
    {
        Camera.main.fieldOfView = 40;
    }


    private void Update()
    {

        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * -sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;

    }
}