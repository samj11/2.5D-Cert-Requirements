using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Transform _platformPos1, _platformPos2;
    private float _duration = 2f;
    private float _currentTime;

    private bool _reachedPos;

    // Start is called before the first frame update
    void Start()
    {
        _platformPos1 = transform.parent.Find("PlatformPos1");
        _platformPos2 = transform.parent.Find("PlatformPos2");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position == _platformPos2.position)
        {
            _currentTime = 0;
            _reachedPos = true;
        }
        else if (transform.position == _platformPos1.position)
        {
            _currentTime = 0;
            _reachedPos = false;
        }

        if (!_reachedPos)
            MoveToPosition(_platformPos1, _platformPos2);
        else if (_reachedPos)
            MoveToPosition(_platformPos2, _platformPos1);
    }

    private void MoveToPosition(Transform startPos, Transform endPos)
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > _duration)
            _currentTime = _duration;

        float t = _currentTime / _duration;
        t = t * t * (3f - 2f * t);

        transform.position = Vector3.Lerp(startPos.position, endPos.position, t);
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
