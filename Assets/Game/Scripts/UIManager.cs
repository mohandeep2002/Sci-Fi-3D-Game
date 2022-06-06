using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI _ammoText, _coinsText, _messageBox;
    [SerializeField]
    private GameObject _coin;
    private void Start()
    {
        EmptyMessageBox();
    }
    public void UpdateAmmo(int ammocount)
    {
        _ammoText.text = "Ammo: " + ammocount.ToString();
    }
    public void UpdateCoing(int coins)
    {
        _coinsText.text = "Coins: " + coins.ToString();
    }
    public void UpdateMessage(string choice)
    {
        _messageBox.text = choice.ToString();
    }
    public void EmptyMessageBox()
    {
        _messageBox.text = "";
    }
    public void CollectedCoin()
    {
        _coin.SetActive(true);
    }
    public void GiveCoin()
    {
        _coin.SetActive(false);
    }
}
