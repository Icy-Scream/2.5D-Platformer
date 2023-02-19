
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance;
    [SerializeField] TMP_Text _coinText,_liveText;

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
    }

    public void UpdateCoinText(int value) => _coinText.text = ($"Coins: {value}");

    public void UpdateLivesText(int lives) => _liveText.text = ($"Lives: {lives}");
}
