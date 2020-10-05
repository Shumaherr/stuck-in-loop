using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 4f;
    [SerializeField] public float landingSpeed = 2f;
    private Vector3 _forward, _right;
    private bool _isLanding = false;
    private bool _isTakingOff = false;
    private float _altitude = 5.0f;
    private Vector3 _heading;

    void Start()
    {
        _forward = Camera.main.transform.forward;
        _forward.y = 0;
        _forward = Vector3.Normalize(_forward);
        _right = Quaternion.Euler(new Vector3(0, 90, 0)) * _forward;
    }

    // Update is called once per frame
    void Update()
    {
        HandleLanding();
        if (Input.anyKey)
        {
            HandleMove();
        }
    }

    private void HandleLanding() //TODO Refactor with Corutine
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isLanding = true;
        }

        if (_isLanding)
        {
            Landing();
            return;
        }

        if (_isTakingOff)
        {
            TakeOff();
        }
    }

    private void HandleMove()
    {
        if(_isLanding || _isTakingOff)
            return;
        float dT = Time.deltaTime;
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 rightMovement = _right * moveSpeed * dT * Input.GetAxis("Horizontal");
        Vector3 upMovement = _forward * moveSpeed * dT * Input.GetAxis("Vertical");
        
        _heading = Vector3.Normalize(rightMovement + upMovement);

        transform.forward = _heading;
        transform.position += rightMovement;
        transform.position += upMovement;

    }

    private void TakeOff()
    {
         Vector3 tempPos = transform.position;
         tempPos.y += transform.position.y * landingSpeed * Time.deltaTime;
         if (tempPos.y >= _altitude)
         {
             _isTakingOff = false;
             return;
         }
         transform.position = tempPos;
    }

    private void Landing()
    {
        Vector3 tempPos = transform.position;
        tempPos.y -= transform.position.y * landingSpeed * Time.deltaTime;
        transform.position = tempPos;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.GetComponent<TerrainCollider>())
        {
            _isLanding = false;
            _isTakingOff = true;
        }
    }
}
