using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            other.gameObject.GetComponent<Player>().AddCoins();
            Destroy(this.gameObject);
        }
    }
}
