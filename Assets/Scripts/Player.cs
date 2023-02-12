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
        Debug.Log(_yVelocity);

        if (_groundPlayer == true) { 
            _controller.Move(Vector3.zero);
             _yVelocity = -_gravity;
        }

        if (_groundPlayer == true && _gameInput.IsJumping()) {
            _yVelocity += _jumpStrength;
            _doubleJump = false;
        }
        else if(_groundPlayer == false)
        _yVelocity -= _gravity * Time.deltaTime;
       
          

        Vector3 _direction = _gameInput.GetMovementVectorNormalized();
        Vector3 _velocity = _direction * _speed;
        Vector3 movement = new Vector3(_velocity.x, _yVelocity, 0);
        _controller.Move(movement * Time.deltaTime);

    }

}
