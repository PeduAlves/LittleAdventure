using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private CharacterController _cc;
    private Vector3 _movementeVelocity;
    private PlayerInput _playerInput;
    private float _verticalVelocity;
    private Animator _animator;

    public float MoveSpeed = 5f;
    public float Gravity = -9.8f;

    //Enemy

    public bool IsPlayer = true;
    private UnityEngine.AI.NavMeshAgent _navMeshAgent;
    private Transform _targetPlayer;

    private void Awake() {
        
        _cc = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        if( !IsPlayer ){

            _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            _targetPlayer = GameObject.FindWithTag( "Player" ).transform;
            _navMeshAgent.speed = MoveSpeed;
        }
        else{

            _playerInput = GetComponent<PlayerInput>();
        }
    }

    private void FixedUpdate() {
        
        if( IsPlayer ){
            
            CalculatePlayerMovement();
            IsGround();
        }
        else{
            CalculateEnemyMovement();
        }

    }

    #region MyMetods

    private void CalculatePlayerMovement(){

        _movementeVelocity.Set( _playerInput.HorizontalInput, 0f, _playerInput.VerticalInput );
        _movementeVelocity.Normalize();
        _movementeVelocity = Quaternion.Euler( 0, -45f, 0 ) * _movementeVelocity;

        _animator.SetFloat( "Speed", _movementeVelocity.magnitude );
        _movementeVelocity *= MoveSpeed * Time.deltaTime;


        if( _movementeVelocity != Vector3.zero ){

            transform.rotation = Quaternion.LookRotation( _movementeVelocity );
        }

        _animator.SetBool("AirBorne", !_cc.isGrounded);
    }

    private void CalculateEnemyMovement(){

        if( Vector3.Distance( _targetPlayer.position, transform.position ) >= _navMeshAgent.stoppingDistance){

            _navMeshAgent.SetDestination( _targetPlayer.position);
            _animator.SetFloat( "Speed", 0.2f );
        }
        else{

            _navMeshAgent.SetDestination( transform.position);
            _animator.SetFloat( "Speed", 0f );
        }
    }

    private void IsGround(){

        if( _cc.isGrounded == false){

            _verticalVelocity = Gravity;
        }
        else{

           _verticalVelocity = Gravity * 0.3f;
        }

        _movementeVelocity += _verticalVelocity * Vector3.up * Time.deltaTime;

        _cc.Move( _movementeVelocity );
    }

    #endregion
}
