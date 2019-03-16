using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private string horizontalInputName = "Horizontal";
    [SerializeField] private string verticalInputName = "Vertical";

    private Rigidbody rigidBody; // rigid body of the player
    private Vector3 normal;
    private Vector3 targetSurfaceNormal;

    private Vector3 inputs = Vector3.zero;
    private CapsuleCollider col;
    [SerializeField] private LayerMask groundLayer;

    // Moving speed related stuffs
    [SerializeField] private float walkSpeed = 6.0f;
    [SerializeField] private float runSpeed = 8.0f;
    [SerializeField] private float runBuildUpSpeed = 5.0f;
    [SerializeField] private KeyCode runKey = KeyCode.LeftShift;
    private float movementSpeed = 2;

    // Jump related variables
    [SerializeField] private float jumpHeight = 3.0f;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    private bool isJumping;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        normal = transform.up;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CalcPlayerMovementParameters();
    }

    // MonoBehaviour.FixedUpdate has the frequency of the physics system
    private void FixedUpdate()
    {

    }

    private void CalcPlayerMovementParameters()
    {
        if (IsGrounded())
        {
            inputs.x = Input.GetAxis(horizontalInputName);
            inputs.z = Input.GetAxis(verticalInputName);

            if (Input.GetKeyDown(jumpKey))
            {
                rigidBody.AddForce(normal * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.Impulse);
            }
        }

        SetMovementSpeed();
        transform.Translate(inputs * movementSpeed * Time.deltaTime);
    }

    private bool IsGrounded()
    {
        Vector3 centerOfPlayer = transform.position;
        Vector3 centerOfBottomSphere = new Vector3(transform.position.x, transform.position.y - col.radius, transform.position.z);

        return Physics.CheckCapsule(centerOfPlayer, centerOfBottomSphere, col.radius + 0.01f, groundLayer);
    }

    private void SetMovementSpeed()
    {
        if (Input.GetKey(runKey))
        {
            movementSpeed = Mathf.Lerp(movementSpeed, runSpeed, Time.deltaTime * runBuildUpSpeed);
        }
        else
        {
            movementSpeed = Mathf.Lerp(movementSpeed, walkSpeed, Time.deltaTime * runBuildUpSpeed);
        }
    }
}
