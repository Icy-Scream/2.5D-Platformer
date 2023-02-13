
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance;
    [SerializeField] TMP_Text _coinText;

    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
        _coinText.text = ($"Coins: {0}");
    }

    public void UpdateCoinText(int value) => _coinText.text = ($"Coins: {value}");
}
