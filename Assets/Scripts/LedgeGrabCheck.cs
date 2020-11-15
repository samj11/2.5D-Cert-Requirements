using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrabCheck : MonoBehaviour
{
    [SerializeField]
    private Vector3 _handPos, _standPos;

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