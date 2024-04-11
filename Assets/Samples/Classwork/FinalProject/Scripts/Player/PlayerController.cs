using System;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private float _playerSpeed = 16f;
    [SerializeField] private Transform _shootPointIcon;
    [SerializeField] private Transform _target;
    [SerializeField] private AgentShooter _agentShooter;
    [SerializeField] private bool canShoot = true;
    private float _totalShootingDelay = 0.10f;
    private float _shootingDelayLeft;
    private Rigidbody _playerRb;
    private ScoreManager _scoreManager;
    
    //shooting sound related variables
    [SerializeField] private AudioSource _SFXSource;
    [SerializeField] private AudioClip _targetHitNoise;
    [SerializeField] private AudioClip _targetMissedNoise;
    
    // Start is called before the first frame update
    void Start()
    {
        _shootingDelayLeft = _totalShootingDelay;
        _playerRb = GetComponent<Rigidbody>();
        _scoreManager = GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //movement
        float xAxisMovement = Input.GetAxis("Horizontal");
        _playerRb.velocity = new Vector3(xAxisMovement, 0, 0) * _playerSpeed;

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Shoot();
        }

        if (canShoot == false)
        {
            _shootingDelayLeft -= Time.deltaTime;
            if(_shootingDelayLeft <= 0)
            {
                canShoot = true;
            }
        }
    }

    void Shoot()
    {
        if (Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.TransformDirection(Vector3.forward),
            out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.CompareTag("Target") && canShoot == true)
            {
                _agentShooter.EndEpisode();
                _scoreManager.IncrementPlayerScore();
                canShoot = false;
                _shootingDelayLeft = _totalShootingDelay;
                _SFXSource.PlayOneShot(_targetHitNoise);
            }
            else
            {
                _SFXSource.PlayOneShot(_targetMissedNoise);
            }
        }
    }
}