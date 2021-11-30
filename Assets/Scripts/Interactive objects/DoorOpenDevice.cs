using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenDevice : MonoBehaviour
{
    [SerializeField] private Vector3 _doorPosition;

    private bool _isOpen;

    public void Operate()
    {
        if (_isOpen)
        {
            Vector3 position = transform.position - _doorPosition;
            transform.position = position;
        }
        else
        {
            Vector3 position = transform.position + _doorPosition;
            transform.position = position;
        }

        _isOpen = !_isOpen;
    }

    public void Activate()
    {
        if (!_isOpen)
        {
            Vector3 position = transform.position + _doorPosition;
            transform.position = position;
            _isOpen = true;
        }
    }

    public void Deactivate()
    {
        if (_isOpen)
        {
            Vector3 position = transform.position - _doorPosition;
            transform.position = position;
            _isOpen = false;
        }
    }
}
