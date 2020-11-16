using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrabCheck : MonoBehaviour
{
    private Vector3 _handPos, _standPos;

    private void Start()
    {
        _handPos = transform.Find("HandPos").position;
        _standPos = transform.Find("StandPos").position;

        if (_handPos == null || _standPos == null)
            Debug.Log("HandPos or StandPos null");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("LedgeGrabbingChecker"))
        {
            Player _player = other.transform.parent.GetComponent<Player>();
            if(_player != null)
                _player.LedgeGrab(_handPos, this);
        }
    }

    public Vector3 GetStandPos()
    {
        return _standPos;
    }

}