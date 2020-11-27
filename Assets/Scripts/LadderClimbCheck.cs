using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimbCheck : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Player _player = other.GetComponent<Player>();
            if (_player != null)
                _player.LadderClimb();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Player _player = other.GetComponent<Player>();
            if (_player != null)
                _player.LadderExit();

        }
    }
}
