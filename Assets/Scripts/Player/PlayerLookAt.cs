using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAt : MonoBehaviour{
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Transform playerTransform;

    private float _xRotation;

    private void Start(){
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update(){
        var mouseXAxis = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        var mouseYAxis = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseYAxis;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);


        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        playerTransform.Rotate(Vector3.up * mouseXAxis);
    }
}
