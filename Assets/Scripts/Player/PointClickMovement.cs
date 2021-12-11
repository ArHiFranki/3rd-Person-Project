using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointClickMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _rotationSpeed = 15.0f;
    [SerializeField] private float _moveSpeed = 6.0f;
    [SerializeField] private float _jumpSpeed = 15.0f;
    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private float _terminalVelocity = -10.0f;
    [SerializeField] private float _minFall = -1.5f;
    [SerializeField] private float _pushForce = 3.0f;
    [SerializeField] private float _deceleration = 25.0f;
    [SerializeField] private float _targetBuffer = 1.5f;

    private CharacterController _characterController;
    private ControllerColliderHit _contact;
    private Animator _animator;
    private float _verticalSpeed;
    private float _currentSpeed = 0f;
    private Vector3 _targetPosition = Vector3.one;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _verticalSpeed = _minFall;
    }

    private void Update()
    {
        Vector3 movement = Vector3.zero;
        bool hitGround = false;
        RaycastHit hit;

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;

            if (Physics.Raycast(ray, out mouseHit))
            {
                _targetPosition = mouseHit.point;
                _currentSpeed = _moveSpeed;
            }
        }

        if (_targetPosition != Vector3.one)
        {
            if (_currentSpeed > _moveSpeed * 0.5f)
            {
                Vector3 adjustedPosition = new Vector3(_targetPosition.x, transform.position.y, _targetPosition.z);
                Quaternion targetRotation = Quaternion.LookRotation(adjustedPosition - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            }

            movement = _currentSpeed * Vector3.forward;
            movement = transform.TransformDirection(movement);

            if (Vector3.Distance(_targetPosition, transform.position) < _targetBuffer)
            {
                _currentSpeed -= _deceleration * Time.deltaTime;

                if (_currentSpeed <= 0)
                {
                    _targetPosition = Vector3.one;
                }
            }
        }

        _animator.SetFloat("Speed", movement.sqrMagnitude);

        if (_verticalSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            float check = (_characterController.height + _characterController.radius) / 1.9f;
            hitGround = hit.distance <= check;
        }

        if (hitGround)
        {
            _verticalSpeed = _minFall;
            _animator.SetBool("Jumping", false);
        }
        else
        {
            _verticalSpeed += _gravity * 5 * Time.deltaTime;
            if (_verticalSpeed < _terminalVelocity)
            {
                _verticalSpeed = _terminalVelocity;
            }

            if (_contact != null)
            {
                _animator.SetBool("Jumping", true);
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

        Rigidbody rigidbody = hit.collider.attachedRigidbody;
        if (rigidbody != null && !rigidbody.isKinematic)
        {
            rigidbody.velocity = hit.moveDirection * _pushForce;
        }
    }
}
