using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private UIManager _UIManager;
    private float _rotateSpeed = 1.2f;
    private float _targetAngle = 160f;

    // Start is called before the first frame update
    void Start()
    {
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (!_UIManager)
            Debug.Log("UI Manager is null");
    }

    private void Update()
    {
        IdleMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _UIManager.UpdateCollectableCount();
            Destroy(this.gameObject);
        }
    }

    private void IdleMovement()
    {
        Vector3 rotationSpeed = new Vector3(2f, 2f, 2f);
        transform.Rotate(rotationSpeed);

        /*
        Quaternion target = Quaternion.Euler(_targetAngle, _targetAngle, _targetAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * _rotateSpeed);
        */
    }
}