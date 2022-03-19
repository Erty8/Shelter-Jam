using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Tooltip("Sensitivity multiplier for moving the camera around")]
    public float LookSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X")*LookSensitivity*Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y")*LookSensitivity*Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
    //public Vector3 GetMoveInput()
    //{
        
    //        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0f,
    //            Input.GetAxisRaw("Vertical"));

    //        // constrain move input to a maximum magnitude of 1, otherwise diagonal movement might exceed the max move speed defined
    //        move = Vector3.ClampMagnitude(move, 1);

    //        return move;

    //}
}
