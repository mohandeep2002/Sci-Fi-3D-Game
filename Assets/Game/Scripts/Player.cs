using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _cc;
    [SerializeField]
    private float _speed = 3.5f, _gravity = 9.81f;
    [SerializeField]
    private GameObject _muzzleFlash;
    [SerializeField]
    private GameObject _hitMarkerPrefab;
    [SerializeField]
    private AudioSource _weaponAudio;
    [SerializeField]
    private int currentAmmo, maxAmmo = 50;
    private bool _isReloading = false;
    private UIManager _uimanager;
    [SerializeField]
    private int _coins = 0;
    [SerializeField]
    public bool hasCoin = false, hasWeapon = false;
    [SerializeField]
    private GameObject _weapon;
    
    // Start is called before the first frame update
    void Start()
    {
        _cc = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        currentAmmo = maxAmmo;
        _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uimanager == null)
        {
            Debug.LogError("UI manager player ");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && currentAmmo > 0)
        {
            if (hasWeapon)
            {
                Shoot();
            }
            else
            {
                Debug.Log("No weapon");
            }
        }
        else
        {
            _weaponAudio.Stop();
            _muzzleFlash.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.R) && _isReloading == false && hasWeapon)
        {
            _isReloading = true;
            StartCoroutine(LoadingAmmoTime());
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
       
        CalculateMovement();
    }
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * _speed;
        velocity.y = velocity.y - _gravity;
        velocity = transform.TransformDirection(velocity); 
        _cc.Move(velocity * Time.deltaTime);
    }
    void Shoot()
    {
        _muzzleFlash.SetActive(true);
        currentAmmo--;
        _uimanager.UpdateAmmo(currentAmmo);
        if (_weaponAudio.isPlaying == false)
        {
            _weaponAudio.Play();
        }
        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            Debug.Log(hitInfo.transform.name + " is hittted!");
            GameObject hitmarker = Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(hitmarker, 1f);
            Destructable crate = hitInfo.transform.GetComponent<Destructable>();
            if (crate != null)
            {
                crate.DestoryCrate();
            }
        }
    }
    IEnumerator LoadingAmmoTime()
    {
        yield return new WaitForSeconds(5f * Time.deltaTime);
        currentAmmo = maxAmmo;
        _uimanager.UpdateAmmo(currentAmmo);
        _isReloading = false;
    }
    public void AddCoin()
    {
        _coins++;
        _uimanager.UpdateAmmo(_coins);
    }
    public void EnableWeapons()
    {
        _weapon.SetActive(true);
        hasWeapon = true;
    }
    public void UpdateAmmoPlayer()
    {
        currentAmmo = maxAmmo;
        _uimanager.UpdateAmmo(_coins);
    }
}
