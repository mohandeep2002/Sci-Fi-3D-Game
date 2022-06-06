using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{
    Player player;
    UIManager _uimanager;
    [SerializeField]
    private AudioClip _clip;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (player.hasCoin)
                {
                    player.hasCoin = false;
                    _uimanager.GiveCoin();
                    AudioSource.PlayClipAtPoint(_clip, transform.position, 1f);
                    player.EnableWeapons();
                    player.UpdateAmmoPlayer();
                }
                else
                {
                    Debug.Log("get out @");
                }
            }
        }
    }
}
