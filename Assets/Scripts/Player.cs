using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _jumpHeight = 20f;
    [SerializeField]
    private float _gravity = 1f;
    private Vector3 _direction;

    private CharacterController _charController;
    private Animator _animator;
    private bool _idleJumping;
    private bool _jumping;
    private bool _onLedge;
    private bool _ladderClimbing;
    private bool _rolling;

    private LedgeGrabCheck _activeLedge;


    // Start is called before the first frame update
    void Start()
    {
        _charController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        if (_animator == null)
            Debug.Log("Animator is null");
    }

    // Update is called once per frame
    void Update()
    {
        if(_charController.enabled)
            CharacterMovement();

        if(_onLedge)
        {
            if(Input.GetKeyDown(KeyCode.E))
                _animator.SetBool("ClimbingLedge", true);
        }

    }

    void CharacterMovement()
    {
        if (_charController.isGrounded)
        {
            if (_jumping)
            {
                _jumping = false;
                _animator.SetBool("Jumping", _jumping);
            }

            float h = Input.GetAxisRaw("Horizontal");
            if(!_idleJumping)
                _direction = new Vector3(0, 0, h) * _speed;

            _animator.SetFloat("Speed", Mathf.Abs(h));

            if (h != 0)
            {
                Vector3 facing = transform.localEulerAngles;
                facing.y = _direction.z > 0 ? 0 : 180;
                transform.localEulerAngles = facing;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(h != 0)
                    _direction.y += _jumpHeight;
                if (h == 0)
                    _idleJumping = true;
                _jumping = true;
                _animator.SetBool("Jumping", _jumping);
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _rolling = true;
                _animator.SetFloat("Speed", Mathf.Abs(h));
                _animator.SetBool("Rolling", _rolling);
                _speed = _speed * 1.5f;
            }
        }

        if(_ladderClimbing)
        {
            float v = Input.GetAxisRaw("Vertical");

            if (v != 0)
            {
                _animator.enabled = true;
                float climbingSpeed = 5f;
                _direction = new Vector3(0, v, 0) * climbingSpeed;
                _gravity = 0;

                if (v>0)
                {
                    _animator.SetBool("OnGround", false);
                    _animator.SetFloat("ClimbingLadder", Mathf.Abs(v));
                }
                else if (v<0)
                    _animator.SetFloat("ClimbingLadder", -Mathf.Abs(v));
            }
            else if (v == 0)
            {
                _direction = new Vector3(0, v, 0);
                _animator.enabled = false;
            }

            if (_charController.isGrounded)
                LadderExit();
        }



        _direction.y -= _gravity * Time.deltaTime;

        _charController.Move(_direction * Time.deltaTime);
    }

    public void LedgeGrab(Vector3 handPos, LedgeGrabCheck currentLedge)
    {
        _charController.enabled = false;
        _animator.SetBool("LedgeGrab", true);

        _animator.SetBool("Jumping", false);
        _animator.SetFloat("Speed", 0);
        _onLedge = true;

        transform.position = handPos;

        _activeLedge = currentLedge;
    }

    public void ClimbUpComplete()
    {
        transform.position = _activeLedge.GetStandPos();
        _animator.SetBool("LedgeGrab", false);
        _charController.enabled = true;
    }

    public void LadderClimb()
    {
        float v = Input.GetAxisRaw("Vertical");

        if (v != 0)
            _ladderClimbing = true;
    }

    public void LadderExit()
    {
        _ladderClimbing = false;
        _animator.SetBool("OnGround", true);
        _animator.SetBool("ClimbingLadder", false);
        _gravity = 1f;
    }

    public void RollingComplete()
    {
        _rolling = false;
        _animator.SetBool("Rolling", _rolling);
        _speed = _speed /1.5f;
    }

    public void IdleJumpingComplete()
    {
        _idleJumping = false;
    }

}