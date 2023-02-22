using JetBrains.Annotations;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private GameInput _gameInput;
    private CharacterController _controller;
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private float _jumpStrength = 15.0f;
    [SerializeField]private float _gravity = 1.0f;
    [SerializeField] private bool _groundPlayer;
    private int _lives = 3;
    private float _yVelocity;
    private  Vector3 _direction;
    private Vector3 _xVelocity;
    public int _coinCollected { get; private set; } = 0;
    private bool _doubleJump;
    private float _jumpDelay;
    [SerializeField] private bool _canWallJump;
   [SerializeField] private Vector3 _wallJumpVelocity;
    private void Awake()
    {
        _gameInput = GetComponent<GameInput>();
        _controller = GetComponent<CharacterController>();
        if (_gameInput == null) Debug.LogError("Missing Game Input");
        if (_controller == null) Debug.LogError("Missing Character Controller");
    }

    private void Start()
    {
        UIManager._instance.UpdateLivesText(_lives);
        UIManager._instance.UpdateCoinText(_coinCollected);
    }

    private void Update()
    {
        Movement();
        ResetSpawn();
        Death();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
        
        if(_controller.isGrounded == false && hit.transform.CompareTag("Wall"))
        {
            Debug.DrawRay(hit.point, hit.normal, Color.blue);
            _canWallJump = true;
            _wallJumpVelocity = hit.normal * 2f;
        }    
    }


    private void Movement()
    {
        _groundPlayer = _controller.isGrounded;


        if (_groundPlayer == true)
        {
            _canWallJump = false;
            _controller.Move(Vector3.zero);
            _yVelocity = -_gravity;
        }

        if (_groundPlayer == true && _gameInput.IsJumping() && !_canWallJump)
        {
            _yVelocity += _jumpStrength;
            _doubleJump = false;
            _jumpDelay = Time.time + 0.3f;

        }
        else if (_groundPlayer == false)
        {
            if ((_canWallJump && _gameInput.IsJumping() && Time.time > _jumpDelay))
            {
                _doubleJump = true;
                _canWallJump = false;
                _xVelocity = _wallJumpVelocity;

                if (_yVelocity < 0)
                {
                    _yVelocity = 0;
                    _yVelocity += 8f;
                }
                else
                    _yVelocity += 8f;
            }

            if ((!_doubleJump && _gameInput.IsJumping() && Time.time > _jumpDelay))
            {
                _doubleJump = true;
              
                if (_yVelocity < 0)
                {
                    _yVelocity = 0;
                    _yVelocity += 6f;
                }
                else
                    _yVelocity += 6f;

            }

            _yVelocity -= _gravity * Time.deltaTime;

        }

        var _yMaxVelocity = Mathf.Clamp(_yVelocity, -20, 100f);
        
        if (_groundPlayer == true)
        {
            _direction = _gameInput.GetMovementVectorNormalized();
            _xVelocity = _direction * _speed;
        }

   
            Vector3 _movement = new Vector3(_xVelocity.x, _yMaxVelocity, 0);
            _controller.Move(_movement * Time.deltaTime);
    }

    private void ResetSpawn() 
    {
        Vector3 _currentPosition = transform.position;
        Vector3 _spawnLocation = new Vector3(0, 1, 0);
        bool uiZeroLivesText = _lives > 0;

        if (_currentPosition.y < -5) 
        {
            transform.position = _spawnLocation;
            _lives--;
            if(uiZeroLivesText)
            UIManager._instance.UpdateLivesText(_lives);
        }
    }


    private void Death() 
    {
        if(_lives < 0) 
        {
            SceneManager.LoadScene(0);
        }
    }

    public void AddCoins() 
    {
        _coinCollected++;
        UIManager._instance.UpdateCoinText(_coinCollected);
    }

}
