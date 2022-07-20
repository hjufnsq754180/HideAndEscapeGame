using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private int _currentState;
    private static readonly int Idle = Animator.StringToHash("Idle_animation");
    private static readonly int Run = Animator.StringToHash("Running_animation");
    private static readonly int Defeat = Animator.StringToHash("Defeat_animation");

    private CharacterController _characterController;

    private float _speed;

    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private bool isIdle;
    private bool isRun;
    private bool isDefeat;
    private Animator _animator;
    private GameManager _gameManager;

    private Vector3 _dir;
    public Vector3 dir => _dir;

    [Inject]
    private void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _speed = _gameManager.UnitSpeed;
    }

    private void Update()
    {
        if (_gameManager.IsGameStarted)
        {
            isIdle = true;
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            _dir = new Vector3(horizontal, 0, vertical).normalized;

            if (_dir.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0, angle, 0);

                _characterController.Move(dir * _speed * Time.deltaTime);
                isRun = true;
            }
            else { isRun = false; }
        }
        

        var state = GetPlayerState();

        if (state == _currentState) return;
        _animator.CrossFade(state, 0.05f, 0);
        _currentState = state;
    }

    private int GetPlayerState()
    {
        if (isRun) return Run;
        if (isDefeat) return Defeat;
        return Idle;
    }
}
