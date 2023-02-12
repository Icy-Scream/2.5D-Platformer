using JetBrains.Annotations;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameInput _gameInput;
    private CharacterController _controller;
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private float _jumpStrength = 15.0f;
    [SerializeField]private float _gravity = 1.0f;
    private bool _doubleJump;
    private float _jumpDelay;
    private float _yVelocity;
    [SerializeField] private bool _groundPlayer;
    private void Awake()
    {
        _gameInput = GetComponent<GameInput>();
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {

        _groundPlayer = _controller.isGrounded;


        if (_groundPlayer == true) { 
            _controller.Move(Vector3.zero);
             _yVelocity = -_gravity;
        }

        if (_groundPlayer == true && _gameInput.IsJumping()) {
            _yVelocity += _jumpStrength;
            _doubleJump = false;
            _jumpDelay = Time.time + 0.3f;
            
        }
        else if(_groundPlayer == false) 
        {
            if (!_doubleJump && _gameInput.IsJumping() && Time.time > _jumpDelay) 
            {
                _doubleJump = true;
                if(_yVelocity < 0) { 
                    _yVelocity = 0;
                    _yVelocity += 5f;
                }
                else
                    _yVelocity += 5f;

            }

        _yVelocity -= _gravity * Time.deltaTime;

        }

        var _yMaxVelocity = Mathf.Clamp(_yVelocity, -20, 100f);
        
        Vector3 _direction = _gameInput.GetMovementVectorNormalized();
        Vector3 _velocity = _direction * _speed;
        
        Vector3 movement = new Vector3(_velocity.x, _yMaxVelocity, 0);
        _controller.Move(movement * Time.deltaTime);

    }

}
