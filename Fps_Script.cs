using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Footsteps;
using Cinemachine;
using System;
using UnityEngine.InputSystem;

public class Fps_Script : MonoBehaviour
{
    public static Fps_Script instance;
    [SerializeField] public PlayerInput playerInput;
    [SerializeField] public PlayerInputActions playerInputActions;
    public float walkingSpeed = 9f;
    public float runningSpeed = 17f;
    public float jumpSpeed;
    public float gravity;
    public Transform playerCamera;
    public Camera cam;
    public CinemachineVirtualCamera virtualCamera;
    //Maximum angles that the player can look up or down
    public float lookXLimit = 45.0f;
    public float height;
    public bool isRunning = false;
    [Header("Crouch Parameters")]
    [SerializeField] private float footstepHeightCrouched;
    [SerializeField] private float footstepHeightStanding;
    [SerializeField] private float crouchHeight = 2.5f;
    [SerializeField] private float standingHeight = 2.5f;
    [SerializeField] private float timeToCrouch = 0.35f;
    [SerializeField] private Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    [SerializeField] private Vector3 standingCenter = new Vector3(0, 0, 0);
    public bool isCrouching;
    [Space]
    Vector3 forward, right;
    public CharacterController characterController;
    public Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    public MouseLook mouseLook = new MouseLook();

    public bool canMove = false;
    public bool canRotate = false;

    private void Awake()
    {
        instance = this;
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new PlayerInputActions();

        //Set and get default key bindings
        string W = "<Keyboard>/" + PlayerPrefs.GetString("WKey", "W");
        playerInputActions.Player.Movement.ApplyBindingOverride(1, new InputBinding { overridePath = W });
        string S = "<Keyboard>/" + PlayerPrefs.GetString("SKey", "S");
        playerInputActions.Player.Movement.ApplyBindingOverride(2, new InputBinding { overridePath = S });
        string A = "<Keyboard>/" + PlayerPrefs.GetString("AKey", "A");
        playerInputActions.Player.Movement.ApplyBindingOverride(3, new InputBinding { overridePath = A });
        string D = "<Keyboard>/" + PlayerPrefs.GetString("DKey", "D");
        playerInputActions.Player.Movement.ApplyBindingOverride(4, new InputBinding { overridePath = D });

        playerInputActions.Player.Enable();
        playerInputActions.Player.Movement.performed += Movement_performed;
    }

    private void Movement_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
    }
    void Start()
    {
        if (virtualCamera)
        mouseLook.Init(transform, virtualCamera.transform);
    else
        mouseLook.Init(transform, cam.transform);
    characterController = GetComponent<CharacterController>();


        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        //playerInputActions.Player.Movement.performed += Movement_performed;
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        // We are grounded, so recalculate move direction based on axes
        forward = transform.TransformDirection(new Vector3(0, 0, 1));
        right = transform.TransformDirection(new Vector3(1, 0, 0));
        // Press Left Shift to run
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * inputVector.y : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * inputVector.x : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if ((Input.GetKeyDown(GameManager.instance.jump) || Input.GetKeyDown(KeyCode.JoystickButton0)) && canMove && characterController.isGrounded && isCrouching == false)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }
        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        //Crouch
        if ((Input.GetKeyDown(GameManager.instance.crouch) || Input.GetKeyDown(KeyCode.JoystickButton1)) && canMove && characterController.isGrounded)
        {
            StartCoroutine(CrouchStand());
        }
        // Player and Camera rotation
        if (canMove)
        {
            if (canRotate)
            {
            mouseLook.XSensitivity = mouseLook.YSensitivity = PlayerPrefs.GetFloat("sens", 1f);
            if (virtualCamera)
                mouseLook.LookRotation(transform, virtualCamera.transform);
            else
                mouseLook.LookRotation(transform, cam.transform);
        }
        }
    }
    private IEnumerator CrouchStand()
    {
        RaycastHit hit;
        Vector3 above = transform.TransformDirection(Vector3.up);
        if (isCrouching && Physics.Raycast(playerCamera.transform.position, above, out hit, 3f))
            yield break;

        GetComponent<CharacterFootsteps>().groundCheckHeight = isCrouching ? footstepHeightStanding : footstepHeightCrouched;
        float timeElapsed = 0;
        float targetHeight = isCrouching ? standingHeight : crouchHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = characterController.center;

        while (timeElapsed < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        characterController.height = targetHeight;
        characterController.center = targetCenter;


        isCrouching = !isCrouching;
        if (isCrouching == true)
        {
            Stamina.instance.canRun = false;
            GetComponent<CharacterFootsteps>().enabled = false;
        }
        else
        {
            Stamina.instance.canRun = true;
            GetComponent<CharacterFootsteps>().enabled = true;
        }
    }

}