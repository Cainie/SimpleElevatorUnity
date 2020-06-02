using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour{

    [SerializeField] private float speed;
    private Rigidbody _playerRigidbody;
    private Transform _playerTransform;

    private void Start()
    {
        _playerTransform = transform;
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update(){
        var xAxis = Input.GetAxis("Horizontal");
        var zAxis = Input.GetAxis("Vertical");

        
        var moveVector = _playerTransform.right * xAxis + _playerTransform.forward * zAxis;
        _playerRigidbody.velocity = moveVector * speed;
    }
}
