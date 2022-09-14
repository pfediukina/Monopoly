using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform targetObject;

    private Vector3 _initialOffset;
    private Vector3 _initialRotation;
    private int _offset = 4;

    void Start()
    {
        _initialRotation = (new Vector3(35, 180, 0));
        transform.localEulerAngles = _initialRotation;
    }

    void FixedUpdate()
    {
        transform.localPosition = _initialOffset;
    }

    public void RotateCameraBySide(int last_pos, int new_pos)
    {
        var rot = (int)Mathf.Floor(new_pos / 10);
        //if (rot != (int)Mathf.Floor(last_pos / 10))
        //    RotateCamera(new Vector3(0, (rot * 90) % 360, 0));
        
        switch (rot)
        {
            case 1: 
                SetOffset(new Vector3(_offset, 5, 0));
                RotateCamera(new Vector3(0, (rot * 90) % 360, 0)); 
                break;
            case 2: 
                SetOffset(new Vector3(0, 5, -_offset));
                RotateCamera(new Vector3(0, (rot * 90) % 360, 0)); 
                break;
            case 3: 
                SetOffset(new Vector3(-_offset, 5, 0));
                RotateCamera(new Vector3(0, (rot * 90) % 360, 0)); 
                break;
            default: 
                SetOffset(new Vector3(0, 5, _offset));
                RotateCamera(new Vector3(0, (rot * 90) % 360, 0)); 
                break;
        }
    }

    private void RotateCamera(Vector3 cameraRotation)
    {
        transform.localEulerAngles = cameraRotation + _initialRotation;
        //Debug.Log(transform.localEulerAngles);
    }

    public void SetOffset(Vector3 offset)
    {
        _initialOffset = offset;
    }
}
