using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [Header("Movement")] 
    public float moveSpeed;
    public float sprintSpeed; // Tốc độ khi chạy nhanh (sprint)
    private float currentSpeed; // Tốc độ hiện tại
    private bool isSprinting = false; // Đang sprint hay không
    
    private Vector2 currentMovementInput;
    public float jumpForce;
    public LayerMask groundLayerMask;

    [Header("Look")] public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;

    private Vector2 mouseDelta;

    [HideInInspector]
    public bool canLook=true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        // Khởi tạo tốc độ
        currentSpeed = moveSpeed;
        
        // Set sprint speed mặc định nếu chưa set trong Inspector
        if (sprintSpeed == 0)
        {
            sprintSpeed = moveSpeed * 1.5f; // Sprint nhanh hơn 1.5 lần
        }
    }
    // Start is called before the first frame update

    //components

    private Rigidbody rig;
    public static PlayerController instance;
    private void Awake()
    {
        rig = GetComponent<Rigidbody>();

        instance = this;
    }


    
    //fixeduptade kullanmamın sebebi 0.02 saniyede çalışıp her framede çalışmayarak fiziğin daha iyi çalışmasını sağlamak
    void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook == true)
        {
            CameraLook();
        }
        
    }

    private void Move()
    {
        Vector3 dir = transform.forward * currentMovementInput.y + transform.right * currentMovementInput.x;
        dir *= currentSpeed; // Dùng currentSpeed thay vì moveSpeed
        dir.y = rig.linearVelocity.y;

        rig.linearVelocity = dir;

    }

    private void CameraLook()
    {
        //look up
        camCurXRot += mouseDelta.y * lookSensitivity;
        //clamp the max and min look
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);

    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        //WASD ye basarsak yürüyecek.
        if (context.phase == InputActionPhase.Performed)
        {   
            //belirtilen yöne yürüyecek.
            currentMovementInput = context.ReadValue<Vector2>();
        }
        //basmayı kaldırırsak
        else if (context.phase == InputActionPhase.Canceled)
        {   
            //hareket duracak
            currentMovementInput = Vector2.zero;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        //space basarsak..
        if (context.phase == InputActionPhase.Started)
        {
            //yerde miyiz? ground layer
            if (isGrounded())
            {
                //zıpla
                rig.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
                
            }
        }
    }
    
    // Hàm xử lý Sprint (Shift)
    public void OnSprintInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            // Bắt đầu sprint
            isSprinting = true;
            currentSpeed = sprintSpeed;
            Debug.Log("🏃 Sprint ON - Speed: " + currentSpeed);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            // Dừng sprint
            isSprinting = false;
            currentSpeed = moveSpeed;
            Debug.Log("🚶 Sprint OFF - Speed: " + currentSpeed);
        }
    }

    bool isGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray (transform.position + (transform.forward * 0.2f)+(Vector3.up*0.01f),Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f)+(Vector3.up*0.01f),Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f)+(Vector3.up*0.01f),Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f)+(Vector3.up*0.01f),Vector3.down)

        };
        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + (transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f),Vector3.down);
        Gizmos.DrawRay(transform.position + (transform.right * 0.2f),Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.2f),Vector3.down);
        
    }

    public void ToggleCursor(bool toggle)
    {
        //if the cursor is locked set it to none otherwise set the cursor to lock mode
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        //can look set to opposite of toggle
        canLook = !toggle;
    }
}
