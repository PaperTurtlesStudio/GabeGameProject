using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using TreeEditor;
using UnityEditorInternal;
using UnityEngine;

public class mouseLook : MonoBehaviour
{
    public float mouseSensivity = 100f;

    public Transform playerBody;
    public GameObject cam1;
    public GameObject cam2;

    float xRotation = 0f;

    public static bool thirdPerson = true;

    Vector3 cameraFirstPerson = new Vector3(-0.033f, 0.835f, 0.096f);

    Vector3 cameraThirdPerson = new Vector3(0.01f, 0.974f, -1.253f);

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;

        if(mouseY < 0.1 && mouseY > -0.1)
        {
            mouseY = 0;
        }
        if(mouseX < 0.1 && mouseX > -0.1)
        {
            mouseX = 0;
        }

        if (Input.GetButtonUp("Camera Switch"))
        {
            if (thirdPerson)
            {
                cam1.SetActive(true);
                cam2.SetActive(false);
                thirdPerson = false;
            }
            else
            {
                cam1.SetActive(false);
                cam2.SetActive(true);
                thirdPerson = true;
            }
        }

        

        if (thirdPerson)
        {
            


        }
        else
        {
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }

        

        

        
        
        

        

        




    }
}
