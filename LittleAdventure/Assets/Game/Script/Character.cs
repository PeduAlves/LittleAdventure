using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private CharacterController _cc;
    private Vector3 _movementVelocity;
    private PlayerInput _playerInput;
    private float _verticalVelocity;
    private Animator _animator;

    public float MoveSpeed = 5f;
    public float Gravity = -9.8f;

    //Enemy

    public bool IsPlayer = true;
    private UnityEngine.AI.NavMeshAgent _navMeshAgent;
    private Transform _targetPlayer;

    //State Machine

    public enum CharacterState{
        Normal, Attacking
    }
    public CharacterState CurrentState;

    //Player Slides
    private float attackStartTime;
    public float AttackSlideDuration = 0.1f;
    public float AttackSlideSpeed = 0.5f;

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
        
        State();

    }

    #region MyMetods

    private void AttackAnimationEnds(){

        SwtichStateTo( CharacterState.Normal );
    }

    private void CalculatePlayerMovement(){

        IsGround();

        // IsAtack
        if( _playerInput.MouseButtonDown && _cc.isGrounded ){

            SwtichStateTo( CharacterState.Attacking );
        }
        // ----------------------------

        _movementVelocity.Set( _playerInput.HorizontalInput, 0f, _playerInput.VerticalInput );
        _movementVelocity.Normalize();
        _movementVelocity = Quaternion.Euler( 0, -45f, 0 ) * _movementVelocity;

        _animator.SetFloat( "Speed", _movementVelocity.magnitude );
        _movementVelocity *= MoveSpeed * Time.deltaTime;


        if( _movementVelocity != Vector3.zero ){

            transform.rotation = Quaternion.LookRotation( _movementVelocity );
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

        _movementVelocity += _verticalVelocity * Vector3.up * Time.deltaTime;

        _cc.Move( _movementVelocity );
    }

    private void SwtichStateTo( CharacterState newState){

        //Clear input cache
        _playerInput.MouseButtonDown = false;

        //Exiting state

        switch( CurrentState ){

            case CharacterState.Normal:


                break;
            case CharacterState.Attacking:


                break;
        }
    

        //Entering state
        switch( newState ){

            case CharacterState.Normal:

                
                break;
            case CharacterState.Attacking:

                _animator.SetTrigger("Attack");

                if( IsPlayer ){

                    attackStartTime = Time.time;
                    _movementVelocity = Vector3.zero;
                }
                break;
        }

        CurrentState = newState;
        State();
    }

    private void State(){

        switch( CurrentState ){

            case CharacterState.Normal:

                if( IsPlayer ){
                
                    CalculatePlayerMovement();
                }
                else{

                    CalculateEnemyMovement();
                }
                break;

            case CharacterState.Attacking:

                if( IsPlayer ){
                                        
                    CalculatePlayerMovement();
                                        
                    if( Time.time < attackStartTime + AttackSlideDuration){
        
                        float timePassed = Time.time -  attackStartTime;
                        float lerpTime = timePassed / AttackSlideDuration;
                        _movementVelocity = Vector3.Lerp( transform.forward * AttackSlideSpeed, Vector3.zero, lerpTime );
                    }
                }
                break;
        }
    }

    #endregion
}
