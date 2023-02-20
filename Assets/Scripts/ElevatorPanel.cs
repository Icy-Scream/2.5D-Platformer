using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    [SerializeField] Transform _callButton;
    private MeshRenderer meshRendererCallButton;

    public void Start()
    {
        meshRendererCallButton = _callButton.GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            meshRendererCallButton.material.color = Color.green;
        }
        else
            meshRendererCallButton.material.color = Color.red;
    }
}
