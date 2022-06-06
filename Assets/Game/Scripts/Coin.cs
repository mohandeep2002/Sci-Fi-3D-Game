using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private UIManager _uimanager;
    Player _player;
    [SerializeField]
    private AudioClip _audioCLip;
    private void Start()
    {
        
        _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uimanager == null)
        {
            Debug.LogError("UI in coins is null");
        }
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player in coins is null");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            _uimanager.UpdateMessage("Enter E to collect coin"); 
            if (Input.GetKeyDown(KeyCode.E))
            {
                _player.hasCoin = true;
                AudioSource.PlayClipAtPoint(_audioCLip, transform.position, 1f);
                _uimanager.CollectedCoin();
                Destroy(this.gameObject); 
                //Debug.Log("E clicked");
                ////Destroy(this.gameObject);
                //_player.AddCoin();
                //Debug.Log("Added Coin");
                //_uimanager.UpdateMessage("Coin Collected");
            }
        }    
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _uimanager.UpdateMessage("");
        }
    }
}
