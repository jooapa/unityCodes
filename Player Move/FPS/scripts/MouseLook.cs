using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
  
    public float MouseSensitivuty = 10f;
    public Transform playerBody;

    float xRotation = 0f;
    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * MouseSensitivuty * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * MouseSensitivuty * Time.deltaTime;
         
        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * MouseX);
    }
}
