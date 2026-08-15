using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 6f;
    public float sprintSpeed = 9f;
    public float jumpHeight = 2f;
    public float gravity = -20f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 250f;
    public float mouseSmoothTime = 0.03f;
    public Transform playerCamera;

    private CharacterController controller;

    private Vector3 velocity;
    private float xRotation;

    private float currentMouseX;
    private float currentMouseY;
    private float mouseXVelocity;
    private float mouseYVelocity;

    [Header("Run Sound")]
    public AudioSource runAudio;

    [Header("Jump Sound")]
    public AudioSource jumpAudio;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Get AudioSources automatically if they are not assigned
        AudioSource[] audioSources = GetComponents<AudioSource>();

        if (runAudio == null && audioSources.Length > 0)
        {
            runAudio = audioSources[0];
        }

        if (jumpAudio == null && audioSources.Length > 1)
        {
            jumpAudio = audioSources[1];
        }

        // Make sure running sound loops
        if (runAudio != null)
        {
            runAudio.loop = true;
        }
    }

    void Update()
    {
        if (Cursor.lockState == CursorLockMode.None)
            return;

        MouseLook();
        Movement();
    }

    void MouseLook()
    {
        float targetMouseX =
            Input.GetAxisRaw("Mouse X") * mouseSensitivity;

        float targetMouseY =
            Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        currentMouseX = Mathf.SmoothDamp(
            currentMouseX,
            targetMouseX,
            ref mouseXVelocity,
            mouseSmoothTime
        );

        currentMouseY = Mathf.SmoothDamp(
            currentMouseY,
            targetMouseY,
            ref mouseYVelocity,
            mouseSmoothTime
        );

        transform.Rotate(
            Vector3.up * currentMouseX * Time.deltaTime
        );

        xRotation -= currentMouseY * Time.deltaTime;

        xRotation = Mathf.Clamp(
            xRotation,
            -80f,
            80f
        );

        if (playerCamera != null)
        {
            playerCamera.localRotation =
                Quaternion.Euler(xRotation, 0f, 0f);
        }
    }

    void Movement()
    {
        bool grounded = controller.isGrounded;

        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 move =
            transform.right * horizontal +
            transform.forward * vertical;

        float speed =
            Input.GetKey(KeyCode.LeftShift)
            ? sprintSpeed
            : walkSpeed;

        controller.Move(
            move.normalized * speed * Time.deltaTime
        );

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            velocity.y =
                Mathf.Sqrt(jumpHeight * -2f * gravity);

            // Jump sound
            if (jumpAudio != null)
            {
                jumpAudio.PlayOneShot(jumpAudio.clip);
            }
        }

        // Gravity
        velocity.y += gravity * Time.deltaTime;

        controller.Move(
            velocity * Time.deltaTime
        );

        // Running sound
        if (move.magnitude > 0.1f && controller.isGrounded)
        {
            if (runAudio != null && !runAudio.isPlaying)
            {
                runAudio.Play();
            }
        }
        else
        {
            StopRunSound();
        }
    }

    // Stop running sound
    public void StopRunSound()
    {
        if (runAudio != null && runAudio.isPlaying)
        {
            runAudio.Stop();
        }
    }

    // IMPORTANT:
    // This runs when PlayerMovement is disabled.
    private void OnDisable()
    {
        StopRunSound();
    }
}