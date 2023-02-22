using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private GameInput gameInput;
    [SerializeField] private ElevatorPanel elevatorPanel;
    [SerializeField] private Transform[] _wayPoints;
    [SerializeField] private float _speed;
    private bool _returnPosition = false;
    private bool _interactEnabled = false;

    void Start()
    {
        gameInput = FindObjectOfType<Player>().GetComponent<GameInput>();
        gameInput.OnInteract += ActivateElevator_OnInteract;
    }

    private void ActivateElevator_OnInteract(object sender, System.EventArgs e)
    {
        _interactEnabled = true;

        if (_interactEnabled)
            gameInput.OnInteract -= ActivateElevator_OnInteract;

        StartCoroutine(ElevatorDownRoutine());
    }


   
    IEnumerator ElevatorDownRoutine()
    {
        while (true)
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
                {
                    transform.position = _wayPoints[1].position;
                    yield return new WaitForSeconds(1f);
                    _returnPosition = false;
                }

            }
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
            other.transform.position = this.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;

        }
    }

}
