using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemyController : MonoBehaviour
{
    private int _currentState;
    private static readonly int Idle = Animator.StringToHash("Idle_animation");
    private static readonly int Run = Animator.StringToHash("Running_animation");
    private static readonly int Defeat = Animator.StringToHash("Defeat_animation");

    private NavMeshAgent _agent;
    [SerializeField]
    public List<Transform> targetTransforms = new List<Transform>();
    private int _targetIndex;
    private Vector3 _targetDistance;

    private Animator animator;

    private bool isIdle;
    private bool isRun;
    private bool isDefeat;

    private bool _isSeePlayer = false;
    private bool _isMaxNoise = false;

    private FieldOfView _fov;
    private GameManager _gameManager;

    public static Action OnChangedColor;
    private bool isMaterilChanged = false;


    [Inject]
    private void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private void OnEnable()
    {
        PlayerNoise.OnPlayerMaxNoise += MaxNoiseChase;
    }

    private void OnDisable()
    {
        PlayerNoise.OnPlayerMaxNoise -= MaxNoiseChase;
    }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _fov = GetComponent<FieldOfView>();
        animator = GetComponent<Animator>();
        _agent.speed = _gameManager.UnitSpeed;
        IterateWaypointIndex();
        Patrolling();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _targetDistance) < 0.1 && !_agent.isStopped && !_isSeePlayer)
        {
            IterateWaypointIndex();
            Patrolling();
        }
        else if(_isSeePlayer)
        {
            if (!isMaterilChanged && _isMaxNoise)
            {
                OnChangedColor?.Invoke();
                isMaterilChanged = true;
            }
            ChasePlayer(_fov.playerRef.transform.position);
            if (Vector3.Distance(transform.position, _targetDistance) < 0.3 && !_gameManager.isGameOver)
            {
                _gameManager.GameOver();
                isIdle = true;
                isRun = false;
                _agent.isStopped = true;
            }
        }

        var state = GetState();

        if (state == _currentState) return;
        animator.CrossFade(state, 0.13f, 0);
        _currentState = state;
    }

    private void UpdateDestination(Vector3 targetTransform)
    {
        _targetDistance = targetTransform;
        _agent.SetDestination(_targetDistance);
    }

    private void ChasePlayer(Vector3 playerPos)
    {
        UpdateDestination(playerPos);
    }

    private void Patrolling()
    {
        UpdateDestination(targetTransforms[_targetIndex].position);
        _agent.isStopped = true;
        StartCoroutine(nameof(WaitAPoint));
    }

    private void IterateWaypointIndex()
    {
        _targetIndex++;
        if (_targetIndex == targetTransforms.Count)
        {
            _targetIndex = 0;
        }
    }

    public void SeePlayer()
    {
        _isSeePlayer = true;
    }

    public void MaxNoiseChase()
    {
        SeePlayer();
        _isMaxNoise = true;
    }

    private int GetState()
    {
        if (isRun) return Run;
        if (isDefeat) return Defeat;
        return Idle;
    }

    IEnumerator WaitAPoint()
    {
        isIdle = true;
        isRun = false;
        yield return new WaitForSeconds(1.5f);
        _agent.isStopped = false;
        isRun = true;
        isIdle = false;
    }
}
