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
            MovePlayer();
        }
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
}
