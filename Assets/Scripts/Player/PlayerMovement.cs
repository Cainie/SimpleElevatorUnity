using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour{

    [SerializeField] private float speed;
    private float _gravityForce = 9.81f;
    private Rigidbody _playerRigidbody;
    private Transform _playerTransform;
    

    private void Start()
    {
        _playerTransform = transform;
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate(){
        var xAxis = Input.GetAxis("Horizontal");
        var zAxis = Input.GetAxis("Vertical");
        var moveVector = _playerTransform.right * xAxis + _playerTransform.forward * zAxis ;
        moveVector *= speed;
        moveVector += -_playerTransform.up * _gravityForce;
        _playerRigidbody.velocity = moveVector;

        //_playerRigidbody.MovePosition(_playerTransform.position + Time.fixedDeltaTime * speed * moveVector);
    }
}