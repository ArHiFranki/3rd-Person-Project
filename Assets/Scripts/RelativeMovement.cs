using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _rotationSpeed = 15.0f;
    [SerializeField] private float _moveSpeed = 6.0f;

    private CharacterController _characterController;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 movement = Vector3.zero;

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

        movement *= Time.deltaTime;
        _characterController.Move(movement);
    }
}
