using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _rotationSpeed = 15.0f;
    [SerializeField] private float _moveSpeed = 6.0f;
    [SerializeField] private float _jumpSpeed = 15.0f;
    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private float _terminalVelocity = -10.0f;
    [SerializeField] private float _minFall = -1.5f;

    private CharacterController _characterController;
    private ControllerColliderHit _contact;
    private float _verticalSpeed;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _verticalSpeed = _minFall;
    }

    private void Update()
    {
        Vector3 movement = Vector3.zero;
        bool hitGround = false;
        RaycastHit hit;

        float horizpntalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizpntalInput != 0 || verticalInput != 0)
        {
            movement.x = horizpntalInput * _moveSpeed;
            movement.z = verticalInput * _moveSpeed;
            movement = Vector3.ClampMagnitude(movement, _moveSpeed);

            Quaternion tmp = _target.rotation;
            _target.eulerAngles = new Vector3(0, _target.eulerAngles.y, 0);
            movement = _target.TransformDirection(movement);
            _target.rotation = tmp;

            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, _rotationSpeed * Time.deltaTime);
        }

        if (_verticalSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            float check = (_characterController.height + _characterController.radius) / 1.9f;
            hitGround = hit.distance <= check;
        }

        if (hitGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _verticalSpeed = _jumpSpeed;
            }
            else
            {
                _verticalSpeed = _minFall;
            }
        }
        else
        {
            _verticalSpeed += _gravity * 5 * Time.deltaTime;
            if (_verticalSpeed < _terminalVelocity)
            {
                _verticalSpeed = _terminalVelocity;
            }

            if (_characterController.isGrounded)
            {
                if (Vector3.Dot(movement, _contact.normal) < 0)
                {
                    movement = _contact.normal * _moveSpeed;
                }
                else
                {
                    movement += _contact.normal * _moveSpeed;
                }
            }
        }
        movement.y = _verticalSpeed;

        movement *= Time.deltaTime;
        _characterController.Move(movement);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _contact = hit;
    }
}
