using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform cam;
    private CharacterController controller;

    [SerializeField] private float moveSpeed = 8f;
    private Vector3 velocity;
    private Vector3 moveDirection;

    private float smoothDampTime = 0.1f;
    private float refVelocity;

    private Vector3 gravityForce;
    [SerializeField] private float gravity = -9.81f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpHeight = 2f;
    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        GetPlayerInput();
        if (velocity.magnitude >= 0.05f)
        {
            SetRotation();
            if (!UIManager.instance.IsAnythingOpenInUI()) MovePlayer();
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);
        if (isGrounded && gravityForce.y < 0) gravityForce.y = -1f;
        if (Input.GetButtonDown("Jump") && isGrounded && !UIManager.instance.IsAnythingOpenInUI())
        {
            gravityForce.y = Mathf.Sqrt(jumpHeight * (-2) * gravity);
        }
        ApplyGravity();
    }
    private void GetPlayerInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        velocity = new Vector3(x, 0f, z).normalized;
    }
    private void SetRotation()
    {
        float targetAngle = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref refVelocity, smoothDampTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
    }
    private void MovePlayer()
    {
        controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
    }
    private void ApplyGravity()
    {
        gravityForce.y += gravity * Time.deltaTime;
        controller.Move(gravityForce * Time.deltaTime);
    }
}
