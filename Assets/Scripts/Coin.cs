using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            other.gameObject.TryGetComponent<Player>(out Player _playerScript);
            _playerScript.AddCoins();
            Destroy(this.gameObject);
        }
    }
}
