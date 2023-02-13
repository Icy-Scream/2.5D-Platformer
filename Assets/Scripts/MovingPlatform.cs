using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _wayPoints;
    private bool _returnPosition;

    // Update is called once per frame
    void Update()
    {
        if (!_returnPosition) 
        { 
            transform.position = Vector3.MoveTowards(transform.position, _wayPoints[0].position, _speed * Time.deltaTime);
            if (transform.position == _wayPoints[0].position)
                _returnPosition = true;
            
        }
       else if (_returnPosition) 
        { 
           transform.position = Vector3.MoveTowards(transform.position, _wayPoints[1].position, _speed * Time.deltaTime);
            if (transform.position == _wayPoints[1].position)
                _returnPosition = false;

        }
    }

    private void StaticPositionMovement()
    {
        if (transform.position.x < 32f && !_returnPosition)
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
            if (transform.position.x > 30f)
                _returnPosition = true;
        }

        else if (transform.position.x > 19.7f && _returnPosition)
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
            if (transform.position.x < 19.8f)
                _returnPosition = false;

        }
    }

}
