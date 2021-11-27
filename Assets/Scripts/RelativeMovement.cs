using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void Update()
    {
        Vector3 movement = Vector3.zero;

        float horizpntalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizpntalInput != 0 || verticalInput != 0)
        {
            movement.x = horizpntalInput;
            movement.z = verticalInput;

            Quaternion tmp = _target.rotation;
            _target.eulerAngles = new Vector3(0, _target.eulerAngles.y, 0);
            movement = _target.TransformDirection(movement);
            _target.rotation = tmp;

            transform.rotation = Quaternion.LookRotation(movement);
        }
    }
}
