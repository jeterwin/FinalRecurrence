using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Footsteps;
using Cinemachine;
    public class Fps_Script : MonoBehaviour
    {
        public static Fps_Script instance;
        public float walkingSpeed = 9f;
        public float runningSpeed = 17f;
        public float jumpSpeed;
        public float gravity;
        public Transform playerCamera;
        public Camera cam;
        public CinemachineVirtualCamera virtualCamera;

        //public float lookSpeed = 2.0f;
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
        }
        void Start()
        {
        if(virtualCamera)
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
            // We are grounded, so recalculate move direction based on axes
            forward = transform.TransformDirection(Vector3.forward);
            right = transform.TransformDirection(Vector3.right);
            // Press Left Shift to run
            float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
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
                //rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                //rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                //playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                //transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
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
        }

    }