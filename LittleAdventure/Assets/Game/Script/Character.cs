using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private CharacterController _cc;
    private Vector3 _movementeVelocity;
    private PlayerInput _playerInput;

    public float MoveSpeed = 5f;

    private void Awake() {
        
        _cc = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void FixedUpdate() {
        
        CalculatePlayerMovement();
        _cc.Move( _movementeVelocity );
    }



    private void CalculatePlayerMovement(){

        _movementeVelocity.Set( _playerInput.HorizontalInput, 0f, _playerInput.VerticalInput );
        _movementeVelocity.Normalize();
        _movementeVelocity = Quaternion.Euler( 0, -45f, 0 ) * _movementeVelocity;
        _movementeVelocity *= MoveSpeed * Time.deltaTime;
    }
}
