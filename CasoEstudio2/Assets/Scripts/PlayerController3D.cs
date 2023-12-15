using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3D : MonoBehaviour
{
    [SerializeField]
    float walkSpeed = 5.0f;

    [SerializeField]
    float runSpeed = 10.0f;

    [SerializeField]
    float rotationSpeed = 500.0F;

    [SerializeField]
    float jumpForce = 15.0F;

    [SerializeField]
    float gravityMultiplier = 12.5F;

    [SerializeField]
    int maximumNumberOfJumps = 2;

    CharacterController _characterController;
    Camera _mainCamera;

// public GameObject PantallaGameOver;

    Vector3 _input;
    Vector3 _direction;



    float _inputX;
    float _inputZ;
    float _velocityY;
    float _gravityY;
    float _currentVelocity;

    int _numberOfJumps;

    bool _isMovePressed = false;
    bool _isRunning = false;
    bool _isJumpPressed = false;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _mainCamera = Camera.main;
        _gravityY = Physics.gravity.y;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Void"))
        {
            Debug.Log("Se supone que el Barsinso se murio xd");
        }
        else 
        
        if (other.gameObject.CompareTag("Goal"))
        {
            Debug.Log("El Barsinso gano");
        }
    }

    private void Update()
    {
        HandleInputs();
        HandleGravity();
        HandleRotation();
        HandleMove();

    }

    private void HandleGravity()
    {
        if (IsGrounded())
        {
            if (_velocityY < -1.0F)
            {
                _velocityY = -1;
            }

            if (_isJumpPressed)
            {
                HandleJump();
                StartCoroutine(WaitForGroundedCoroutine());
            }
        }
        else
        {
            if (_isJumpPressed && maximumNumberOfJumps > 1)
            {
                HandleJump();
            }
            _velocityY += _gravityY * gravityMultiplier * Time.deltaTime;
        }
    }

    private void HandleJump()
    {
        _isJumpPressed = false;

        if (_numberOfJumps >= maximumNumberOfJumps)
        {
            return;
        }

        _numberOfJumps++;
        _velocityY = jumpForce;
    }

    private void HandleRotation()
    {

        /*if (_input.sqrMagnitude == 0)
        {
            _direction = Vector3.zero;
            return;
        }
        _direction = Quaternion.Euler(0.0F, _mainCamera.transform.eulerAngles.y, 0.0F) * new Vector3(_input.x, 0.0F, _input.z);
        Quaternion targetRotation = Quaternion.LookRotation(_direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        //if (_isMovePressed)
        //{


        //float targetAngle = Mathf.Atan2(_inputX, _inputZ) * Mathf.Rad2Deg;
        //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothRotationTime);
        //transform.rotation = Quaternion.Euler(0.0F, angle, 0.0F);
        //Quaternion rotation = Quaternion.LookRotation(_direction, Vector3.up);
        //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        //}*/

        if (_input.sqrMagnitude == 0)
        {
            _direction = Vector3.zero;
            return;
        }
        _direction = new Vector3(_inputX, 0.0f, 0.0f);
        Quaternion targetRotation = Quaternion.LookRotation(_direction, Vector3.up);
        transform.rotation = targetRotation;
    }

    private void HandleMove()
    {
        /* //_direction = new Vector3(_inputX, 0.0F, _inputZ);
         //float magnitude = Mathf.Clamp01(_direction.magnitude);
         //_direction.Normalize();

         float speed = _isRunning
             ? runSpeed
             : walkSpeed;

         ///Vector3 velocity = _direction * magnitude * speed;
         //velocity.y = _velocityY;
         _direction.y = _velocityY;
         _characterController.Move(_direction * speed * Time.deltaTime); */

        float speed = _isRunning ? runSpeed : walkSpeed;

        Vector3 horizontalMovement = new Vector3(_inputX, 0.0f, 0.0f);

        Vector3 velocity = horizontalMovement.normalized * speed;

        velocity.y = _velocityY;

        _characterController.Move(velocity * Time.deltaTime);
    }

    private void HandleInputs()
    {
        _inputX = Input.GetAxisRaw("Horizontal");
        _inputZ = Input.GetAxisRaw("Vertical");
        _input = new Vector3(_inputX, 0.0F, _inputZ);

        _isMovePressed = _inputX != 0.0F || _inputZ != 0.0F;
        _isRunning = _isMovePressed && Input.GetButton("Fire3");
        _isJumpPressed = Input.GetButtonDown("Jump");
    }

    bool IsGrounded()
    {
        return _characterController.isGrounded;
    }


    IEnumerator WaitForGroundedCoroutine()
    {
        yield return new WaitUntil(() => !IsGrounded());
        yield return new WaitUntil(() => IsGrounded());
        _numberOfJumps = 0;
    }
}


