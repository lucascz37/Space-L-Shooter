using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int _lives = 5;
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private int _speedBoost = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShootPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _tripleShootActive;
    [SerializeField]
    private bool _shieldUp;
    [SerializeField]
    private GameObject _shieldChild;
    [SerializeField]
    private int _score;
    private UiManager _uiManager;
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        //_shieldChild = gameObject.transform.Find("Shield").gameObject;
        if(_spawnManager == null)
        {
            Debug.Log("Null Spawn Manager");
        }
        if(_uiManager == null)
        {
            Debug.Log("Null UiManager");
        }
    }

    void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * _speed);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        if (_tripleShootActive == true)
        {
            Vector3 laserOffset = new Vector3(transform.position.x, transform.position.y + 1.05f, 0);
            Instantiate(_tripleShootPrefab, laserOffset, Quaternion.identity);
        }
        else
        {
            Vector3 laserOffset = new Vector3(transform.position.x, transform.position.y + 1.05f, 0);
            Instantiate(_laserPrefab, laserOffset, Quaternion.identity);
        }
    }

    public void Damage()
    {
        if (_shieldUp)
        {
            _shieldUp = false;
            _shieldChild.SetActive(_shieldUp);
            return;
        }
        _lives--;
        _uiManager.UpdateLives(_lives);
        if (_lives <= 0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void ActivateTripleShoot()
    {
        _tripleShootActive = true;
        StartCoroutine(TripleShootRoutine());
    }

    IEnumerator TripleShootRoutine()
    {
        yield return new WaitForSeconds(5);
        _tripleShootActive = false;
    }

    public void ActivateSpeedPowerUp()
    {
        _speed *= _speedBoost;
        StartCoroutine(SpeedPowerUpRoutine());
    }

    IEnumerator SpeedPowerUpRoutine()
    {
        yield return new WaitForSeconds(5);
        _speed /= _speedBoost;
    }

    public void ActivateShieldPowerUp()
    {
        _shieldUp = true;
        _shieldChild.SetActive(_shieldUp);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    public int GetScore()
    {
        return _score;
    }

}
