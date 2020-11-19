using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private bool _isElevatorActivated;
    private bool _elevatorDown = true;

    [SerializeField]
    private Transform[] _liftPos;
    private int _targetLiftPos;

    private float _speed = 15f;

    private bool _isWaiting;

    private void Start()
    {
        for(int i = 0; i < _liftPos.Length; i++)
            _liftPos[i] = transform.parent.Find("LiftPos"+ i);

        _targetLiftPos = 1;
    }

    private void FixedUpdate()
    {
        ElevatorMoving();
    }

    private void ElevatorMoving()
    {
        if (_isElevatorActivated)
        {
            if (!_isWaiting)
            {

                if (transform.position != _liftPos[_targetLiftPos].position)
                    transform.position = Vector3.MoveTowards(transform.position, _liftPos[_targetLiftPos].position, _speed * Time.deltaTime);
                else
                {
                    StartCoroutine(WaitFloor());

                    if (transform.position == _liftPos[2].position)
                    {
                        _elevatorDown = false;
                        _isElevatorActivated = false;
                    }
                    else if (transform.position == _liftPos[0].position)
                    {
                        _elevatorDown = true;
                        _isElevatorActivated = false;
                    }

                    if (_elevatorDown)
                        _targetLiftPos = _targetLiftPos + 1;
                    else
                        _targetLiftPos = _targetLiftPos - 1;
                }
            }
        }
    }

    private IEnumerator WaitFloor()
    {
        _isWaiting = true;
        yield return new WaitForSeconds(5);
        _isWaiting = false;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            _isElevatorActivated = !_isElevatorActivated;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            other.transform.parent = this.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            other.transform.parent = null;
    }
}