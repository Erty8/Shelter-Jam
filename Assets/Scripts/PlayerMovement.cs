using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Camera camera;
    public CharacterController controller;
    public float speed = 4f;
    public float crouchSpeed = 2f;
    public float walkSpeed = 4f;
    float targetCharacterHeight = 1.8f;
    public float CrouchingSharpness = 10f;
    public float CapsuleHeightCrouching = 0.5f;
    public float CapsuleHeightStanding = 1.8f;
    public float CameraHeightRatio = 0.9f;
    bool crouching = false;
    public KeyCode crouchKey;
    void Start()
    {
        crouchSpeed = speed / 2;
        controller = transform.GetComponent<CharacterController>();
        camera = GameObject.Find("Player Camera").GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        if (crouching)
        {
            speed = crouchSpeed;

        }
        else
        {
            speed = walkSpeed;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right*x+transform.forward*z;
        controller.Move(move * speed * Time.deltaTime);
        
        if (Input.GetKeyDown(crouchKey))
        {
            if (!crouching)
            {
                targetCharacterHeight = CapsuleHeightCrouching;
                
                crouching = true;
            }
            else
            {
                targetCharacterHeight = CapsuleHeightStanding;
                crouching = false;
            }
        }
        UpdateCharacterHeight();
    }
    void UpdateCharacterHeight()
    {
        // Update height instantly
        
        // Update smooth height
        if (controller.height != targetCharacterHeight)
        {
            // resize the capsule and adjust camera position
            controller.height = Mathf.Lerp(controller.height, targetCharacterHeight,
                CrouchingSharpness * Time.deltaTime);
            //controller.center = Vector3.up * controller.height * 0.5f;
            camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition,
                Vector3.up * targetCharacterHeight/2 * CameraHeightRatio, CrouchingSharpness * Time.deltaTime);
            //m_Actor.AimPoint.transform.localPosition = m_Controller.center;
        }
    }
}
