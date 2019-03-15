using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private string horizontalInputName = "Horizontal";
    [SerializeField] private string verticalInputName = "Vertical";

    private CapsuleCollider playerCollider;

    // Gravity related stuffs
    [SerializeField] private float gravity = 50; // gravity acceleration
    [SerializeField] private float changeGravitySpeed = 10;
    private Rigidbody rigidBody; // rigid body of the player
    private Vector3 normal;
    private Vector3 targetSurfaceNormal;
    private Ray ray;
    private RaycastHit hit;
    private float distGround; //distance between player position to ground
    public float deltaGrounded = 0.2f; //player is grounded upto this distance

    // Moving speed related stuffs
    [SerializeField] private float walkSpeed = 6.0f;
    [SerializeField] private float runSpeed = 9.0f;
    [SerializeField] private float runBuildUpSpeed = 3.0f;
    [SerializeField] private KeyCode runKey = KeyCode.LeftShift;
    private float movementSpeed;

    // Jump related variables
    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier = 9.0f;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    private CharacterController characterController;

    private bool isJumping;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        rigidBody = GetComponent<Rigidbody>();
        playerCollider = characterController.GetComponent<CapsuleCollider>(); // TODO: this is not working
        normal = transform.up;
        distGround = playerCollider.bounds.extents.y - playerCollider.center.y;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    // MonoBehaviour.FixedUpdate has the frequency of the physics system
    private void FixedUpdate()
    {
        rigidBody.AddForce(-gravity * rigidBody.mass * normal);
    }

    private void PlayerMovement()
    {
        float vertInput = Input.GetAxis(verticalInputName);
        float horizInput = Input.GetAxis(horizontalInputName);

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        SetMovementSpeed();
        // Handle Jump
        JumpInput();

        // Move on the plane
        characterController.SimpleMove(Vector3.ClampMagnitude(forwardMovement + rightMovement, 1.0f) * movementSpeed);

        //update surface normal and isGrounded
        ray = new Ray(transform.position, -normal); //cast ray downwards
        if (Physics.Raycast(ray, out hit))
        {
            targetSurfaceNormal = hit.normal;
        }
        else
        {
            targetSurfaceNormal = Vector3.up;
        }

        normal = Vector3.Lerp(normal, targetSurfaceNormal, changeGravitySpeed * Time.deltaTime);
        Vector3 myForward = Vector3.Cross(transform.right, normal);
        Quaternion targetRot = Quaternion.LookRotation(myForward, normal);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, changeGravitySpeed * Time.deltaTime);
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

    private void JumpInput()
    {
        if (Input.GetKeyDown(jumpKey) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }

    private IEnumerator JumpEvent()
    {
        characterController.slopeLimit = 90.0f;
        float timeInAir = 0.0f;

        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            characterController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;
        } while (!characterController.isGrounded && characterController.collisionFlags != CollisionFlags.Above);

        characterController.slopeLimit = 45.0f;
        isJumping = false;
    }
}
