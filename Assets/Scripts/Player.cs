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
    private float _yVelocity;

    private void Awake()
    {
        _gameInput = GetComponent<GameInput>();
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 _direction = _gameInput.GetMovementVectorNormalized();
        Vector3 _velocity = _direction * _speed;
        
        Debug.Log(_yVelocity);

        if (_controller.isGrounded == true && _gameInput.IsJumping()) 
        {
            _yVelocity = _jumpStrength;
        }  
        else
        {
            _yVelocity -= _gravity;
        }
       
        _velocity.y = _yVelocity;
        _controller.Move(_velocity * Time.deltaTime);

    }

}
