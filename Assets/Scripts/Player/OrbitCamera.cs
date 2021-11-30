using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _rotationSpeed = 1.5f;

    private float _rotationY;
    private Vector3 _offset;

    private void Start()
    {
        _rotationY = transform.eulerAngles.y;
        _offset = _target.position - transform.position;
    }

    private void LateUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            _rotationY += horizontalInput * _rotationSpeed;
        }
        else
        {
            _rotationY += Input.GetAxis("Mouse X") * _rotationSpeed * 10;
        }

        Quaternion rotation = Quaternion.Euler(0, _rotationY, 0);
        transform.position = _target.position - (rotation * _offset);
        transform.LookAt(_target);
    }
}
