using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject playerWeapon;

    public Vector2 camRotation;
    public Vector3 camPos;
    private float sensivity = 10f;
    public float lookMax = 90f;
    public float lookMin = -90f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void CameraRotation()
    {
        camRotation.x += Input.GetAxis("Mouse X") * sensivity;
        camRotation.y += Input.GetAxis("Mouse Y") * sensivity;

        playerWeapon.transform.localRotation = Quaternion.Euler(-camRotation.y, 0, 0);

        playerCamera.transform.localRotation = Quaternion.Euler(-camRotation.y, 0 , 0);

        gameObject.transform.localRotation = Quaternion.Euler(0, camRotation.x, 0);

        camRotation.y = Mathf.Clamp(camRotation.y, lookMin, lookMax);
    }

    // Update is called once per frame
    public void Update()
    {
        CameraRotation();
    }
}

