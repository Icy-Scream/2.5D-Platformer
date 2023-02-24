using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Moving_Box")) 
        {
            float _boxPositionX = other.transform.position.x;
            if (_boxPositionX <= this.transform.position.x + 0.01f) 
            {
                other.transform.GetComponent<Rigidbody>().isKinematic = true;
                other.transform.GetComponent<MeshRenderer>().material.color = Color.green;
            }
        }
    }
}
