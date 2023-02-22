using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    public event EventHandler OnCallElevator;
    [SerializeField] Transform _callButton;
    private MeshRenderer meshRendererCallButton;
    private GameInput _gameInput;
    private Player player;
    [SerializeField] private int _requiredCoins = 8;
    public void Start()
    {
        meshRendererCallButton = _callButton.GetComponent<MeshRenderer>();
        if (_gameInput == null)
        {
            _gameInput = FindObjectOfType<Player>().GetComponent<GameInput>();
        }

    }

    private void OnTriggerStay(Collider other)
    {
           other.TryGetComponent<Player>(out player);
        
        if (other.CompareTag("Player"))
        {
          _gameInput.OnInteract += ActivateElevator;
        }
    }


    private void ActivateElevator(object sender,System.EventArgs e)
    {
        try
        {
            if (player._coinCollected >= _requiredCoins)
            {
              meshRendererCallButton.material.color = Color.green;
                OnCallElevator?.Invoke(this,EventArgs.Empty);

            }
            else
            {
                Debug.Log("Need 8 Coins");
            }
        }
        catch (Exception) { Debug.Log("Missing Player Component"); }
       
    }
}
